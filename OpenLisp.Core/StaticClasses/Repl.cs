using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;
using OpenLisp.Core.DataTypes.Errors;
using OpenLisp.Core.DataTypes.Errors.Throwable;
using OpenLisp.Core.Events.Args;

namespace OpenLisp.Core.StaticClasses
{
    public static class Repl
    {
        static void OnPrintEvent(object sender, PrintEventArgs e) => PrintEvent?.Invoke(sender, e);
        static void OnInputEvent(object sender, PrintEventArgs e) => InputEvent?.Invoke(sender, e);

        public static event EventHandler<PrintEventArgs> PrintEvent;
        public static event EventHandler<PrintEventArgs> InputEvent; 

        /// <summary>
        /// Use the Reader to read a string and return an OpenLispVal.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static OpenLispVal Read(string str)
        {
            return Reader.ReadStr(str);
        }

        public static bool IsPair(OpenLispVal x)
        {
            return x is OpenLispList && ((OpenLispList) x).Size > 0;
        }

        /// <summary>
        /// Implement quasi-quotes.
        /// 
        /// TODO: refactor to move to <see cref="OpenLisp.Core.StaticClasses.CoreNameSpace"/>.
        /// </summary>
        /// <param name="ast"></param>
        /// <returns></returns>
        public static OpenLispVal QuasiQuote(OpenLispVal ast)
        {
            if (!IsPair(ast))
            {
                return new OpenLispList(new OpenLispSymbol("quote"), ast);
            }

            OpenLispVal a0 = ((OpenLispList)ast)[0];

            var symbol = a0 as OpenLispSymbol;

            if ((symbol != null) && (symbol.ToString() == "unquote"))
            {
                return ((OpenLispList) ast)[1];
            }

            if (!IsPair(a0))
                return new OpenLispList(new OpenLispSymbol("cons"),
                    QuasiQuote(a0),
                    QuasiQuote(((OpenLispList) ast).Rest()));

            OpenLispVal a00 = ((OpenLispList)a0)[0];

            var lispSymbol = a00 as OpenLispSymbol;

            return (lispSymbol != null) && (lispSymbol.ToString() == "splice-unquote")
                ? new OpenLispList(new OpenLispSymbol("concat"),
                    ((OpenLispList) a0)[1],
                    QuasiQuote(((OpenLispList) ast).Rest()))
                : new OpenLispList(new OpenLispSymbol("cons"),
                    QuasiQuote(a0),
                    QuasiQuote(((OpenLispList) ast).Rest()));
        }

        public static bool IsMacroCall(OpenLispVal ast, Env env)
        {
            var list = ast as OpenLispList;

            OpenLispVal a0 = list?[0];

            if (!(a0 is OpenLispSymbol) || env.Find((OpenLispSymbol) a0) == null) return false;

            OpenLispVal mac = env.Get((OpenLispSymbol)a0);

            var func = mac as OpenLispFunc;

            return func != null &&
                   func.Macro;
        }

        /// <summary>
        /// Expands a macro.
        ///
        /// TODO: refactor to move to <see cref="OpenLisp.Core.StaticClasses.CoreNameSpace"/>.
        /// </summary>
        /// <param name="ast"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static OpenLispVal MacroExpand(OpenLispVal ast, Env env)
        {
            while (IsMacroCall(ast, env))
            {
                OpenLispSymbol a0 = (OpenLispSymbol)((OpenLispList)ast)[0];

                OpenLispFunc mac = (OpenLispFunc)env.Get(a0);

                ast = mac.Apply(((OpenLispList)ast).Rest());
            }
            return ast;
        }

        /// <summary>
        /// Evalues an AST in the environment.
        /// 
        /// TODO: refactor to move to <see cref="OpenLisp.Core.StaticClasses.CoreNameSpace"/>.
        /// </summary>
        /// <param name="ast"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static OpenLispVal EvalAst(OpenLispVal ast, Env env)
        {
            var key = ast as OpenLispSymbol;

            if (key != null)
            {
                return env.Get(key);
            }

            var list = ast as OpenLispList;

            if (list == null)
            {
                var map = ast as OpenLispHashMap;
                if (map == null) return ast;
                var newDict =
                    map.Value.ToDictionary<KeyValuePair<string, OpenLispVal>, string, OpenLispVal>(
                        entry => entry.Key, entry => Eval((OpenLispVal) entry.Value, env));
                return new OpenLispHashMap(newDict);
            }

            OpenLispList oldList = list;
            OpenLispList newList = ast.ListQ() ? new OpenLispList()
                : (OpenLispList)new OpenLispVector();

            foreach (OpenLispVal mv in oldList.Value)
            {
                newList.Conj(Eval(mv, env));
            }
            return newList;
        }

        /// <summary>
        /// Evaluate a <see cref="OpenLispVal"/> inside an <seealso cref="Env"/>.
        /// 
        /// The core namespace is defined in <seealso cref="OpenLisp.Core.StaticClasses.CoreNameSpace"/>.
        /// 
        /// TODO: refactor the switch over a0Sym.  All symbols of the core language should be defined in the same place.
        /// </summary>
        /// <param name="origAst"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static OpenLispVal Eval(OpenLispVal origAst, Env env)
        {
            while (true)
            {
                //Console.WriteLine("EVAL: " + printer._pr_str(orig_ast, true));
                if (!origAst.ListQ())
                {
                    return EvalAst(origAst, env);
                }

                // apply list
                OpenLispVal expanded = MacroExpand(origAst, env);
                if (!expanded.ListQ()) { return expanded; }
                OpenLispList ast = (OpenLispList)expanded;

                if (ast.Size == 0) { return ast; }
                var a0 = ast[0];

                var symbol = a0 as OpenLispSymbol;
                String a0Sym = symbol?.ToString() ?? "__<*fn*>__";

                OpenLispVal a1;
                OpenLispVal a2;
                OpenLispVal res;
                switch (a0Sym)
                {
                    case "def!":
                        a1 = ast[1];
                        a2 = ast[2];
                        res = Eval(a2, env);
                        env.Set((OpenLispSymbol)a1, res);
                        return res;
                    case "let*":
                        a1 = ast[1];
                        a2 = ast[2];
                        OpenLispSymbol key;
                        OpenLispVal val;
                        Env letEnv = new Env(env);
                        for (int i = 0; i < ((OpenLispList)a1).Size; i += 2)
                        {
                            key = (OpenLispSymbol)((OpenLispList)a1)[i];
                            val = ((OpenLispList)a1)[i + 1];
                            letEnv.Set(key, Eval(val, letEnv));
                        }
                        origAst = a2;
                        env = letEnv;
                        break;
                    case "quote":
                        return ast[1];
                    case "quasiquote":
                        origAst = QuasiQuote(ast[1]);
                        break;
                    case "defmacro!":
                        a1 = ast[1];
                        a2 = ast[2];
                        res = Eval(a2, env);
                        ((OpenLispFunc)res).Macro = true;
                        env.Set(((OpenLispSymbol)a1), res);
                        return res;
                    case "macroexpand":
                        a1 = ast[1];
                        return MacroExpand(a1, env);
                    case "try*":
                        try
                        {
                            return Eval(ast[1], env);
                        }
                        catch (Exception e)
                        {
                            if (ast.Size <= 2) throw e;
                            OpenLispVal exc;
                            a2 = ast[2];
                            OpenLispVal a20 = ((OpenLispList)a2)[0];
                            if (((OpenLispSymbol) a20).ToString() != "catch*") throw e;
                            var exception = e as OpenLispException;
                            exc = exception != null
                                ? (OpenLispVal) exception.Value
#if NOSTACKTRACE // <-- wat
                 /* --> wat */  : new OpenLispString(e.StackTrace); // <-- wat
#elif !NOSTACKTRACE
                                : new OpenLispString("Stack Trace not yet available in OS.");
#endif
                            return Eval(((OpenLispList)a2)[2],
                                new Env(env, ((OpenLispList)a2).Slice(1, 2),
                                    new OpenLispList(exc)));
                        }
                    case "do":
                        EvalAst(ast.Slice(1, ast.Size - 1), env);
                        origAst = ast[ast.Size - 1];
                        break;
                    case "if":
                        a1 = ast[1];
                        OpenLispVal cond = Eval(a1, env);
                        if (cond == StaticOpenLispTypes.Nil || cond == StaticOpenLispTypes.False)
                        {
                            // eval false slot form
                            if (ast.Size > 3)
                            {
                                origAst = ast[3];
                            }
                            else
                            {
                                return StaticOpenLispTypes.Nil;
                            }
                        }
                        else
                        {
                            // eval true slot form
                            origAst = ast[2];
                        }
                        break;
                    case "fn*":
                        OpenLispList a1f = (OpenLispList)ast[1];
                        OpenLispVal a2f = ast[2];
                        Env curEnv = env;
                        return new OpenLispFunc(a2f, env, a1f,
                            args => Eval(a2f, new Env(curEnv, a1f, args)));
                    default:
                        var el = (OpenLispList)EvalAst(ast, env);
                        var f = (OpenLispFunc)el[0];
                        OpenLispVal fnast = f.Ast;
                        if (fnast != null)
                        {
                            origAst = fnast;
                            env = f.GenEnv(el.Rest());
                        }
                        else
                        {
                            return f.Apply(el.Rest());
                        }
                        break;
                }

            }
        }

        public static string Print(OpenLispVal exp)
        {
            return Printer.PrStr(exp, true);
        }

        public static void ReplMain(string[] args)
        {
            var replEnv = new Env(null);

            // TODO: extract this from the Repl
            Func<string, OpenLispVal> Re = (string str) => Eval(Read(str), replEnv);

            // Load each OpenLispSymbol 
            foreach (var entry in CoreNameSpace.Ns)
            {
                replEnv.Set(new OpenLispSymbol(entry.Key), entry.Value);
            }

            // TODO: extract this from the Repl
            replEnv.Set(new OpenLispSymbol("eval"), 
                        new OpenLispFunc(a => Eval(a[0], replEnv)));

            int fileIdx = 0;
            if (args.Length > 0 && args[0] == "--raw")
            {
                ReadLine.Mode = ReadLine.ModeEnum.Raw;
                fileIdx = 1;
            }

            OpenLispList argv = new OpenLispList();
            for (int i = fileIdx + 1; i < args.Length; i++)
            {
                argv.Conj(new OpenLispString(args[i]));
            }
            replEnv.Set(new OpenLispSymbol("*ARGV*"), argv);

            // core.oln: defined using the language itself
            // TODO: move this to its own class or assembly?
            Re("(def! not (fn* (a) (if a false true)))");
            Re("(def! load-file (fn* (f) (eval (read-string (str \"(do \" (slurp f) \")\")))))");
            Re("(defmacro! cond (fn* (& xs) (if (> (count xs) 0) (list 'if (first xs) (if (> (count xs) 1) (nth xs 1) (throw \"odd number of forms to cond\")) (cons 'cond (rest (rest xs)))))))");
            Re("(defmacro! or (fn* (& xs) (if (empty? xs) nil (if (= 1 (count xs)) (first xs) `(let* (or_FIXME ~(first xs)) (if or_FIXME or_FIXME (or ~@(rest xs))))))))");

            if (args.Length <= fileIdx)
            {
                while (true)
                {
                    string line;
                    try
                    {
                        // TODO: make user> reflect the current namespace.
                        line = ReadLine.LineReader("user> ");                        
                        if (line != null)
                        {
                            if (line == "")
                            {
                                continue;
                            }
                        }
                        else
                        {
                            break;
                        }

                        OnInputEvent("USER", new PrintEventArgs(line));
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("IOException: " + e.Message);
                        break;
                    }

                    try
                    {
                        //Console.WriteLine(Print(Re(line)));
                        var p = Print(Re(line));
                        OnPrintEvent("REPL", new PrintEventArgs(p));
                        Console.WriteLine(p);
                    }
                    catch (OpenLispContinue)
                    {
                        continue;
                    }
                    catch (OpenLispException e)
                    {
                        Console.WriteLine("Error: " +
                                          //Printer.PrStr((OpenLispVal) e.Value, false));             // Implicitly call ToString()
                                          Printer.PrStr((OpenLispVal)e.Value.ToString(), false));     // Explicitly call ToString()

                        continue;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e.Message);
#if TRACE
                        Console.WriteLine(e.StackTrace);
#elif !TRACE
                        Console.WriteLine("Stack Trace not yet available in OS.");
#endif
                        continue;
                    }
                }
            }
            else
            {
                Re("(load-file \"" + args[fileIdx] + "\")");
                return;
            }

            // repl loop
        }
    }
}