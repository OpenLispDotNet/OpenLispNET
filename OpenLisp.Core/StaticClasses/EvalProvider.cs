using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;

namespace OpenLisp.Core.StaticClasses
{
    /// <summary>
    /// Evaluation provider suitable for most REPLs wanting to compile assemblies.
    /// </summary>
    public static class EvalProvider
    {
        /// <summary>
        /// Creates an eval method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="code"></param>
        /// <param name="usingStatements"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Func<T, TResult> CreateEvalMethod<T, TResult>(string code,
            string[] usingStatements = null,
            string[] assemblies = null)
        {
            var returnType = typeof (TResult);
            var inputType = typeof (T);

            var includeUsings = new HashSet<string>(new[] {"System"}) {returnType.Namespace, inputType.Namespace};

            if (usingStatements != null)
            {
                foreach (var usingStatement in usingStatements)
                {
                    includeUsings.Add(usingStatement);
                }
            }

            using (var compiler = new CSharpCodeProvider())
            {
                var name = "F" + Guid.NewGuid().ToString().Replace("-", string.Empty);
                var includeAssemblies = new HashSet<string>(new[] {"system.dll"});

                if (assemblies != null)
                {
                    foreach (var assembly in assemblies)
                    {
                        includeAssemblies.Add(assembly);
                    }
                }

                var parameters = new CompilerParameters(includeAssemblies.ToArray())
                {
                    GenerateInMemory = true
                };

                var source = string.Format(@"
{0}
namespace {1},
{{
    public static class EvalClass
    {{
        public static {2} Eval({3} arg)
        {{
            {4}
        }}
    }}
}}", GetUsing(includeUsings), name, returnType.Name, inputType.Name, code);

                var compilerResult = compiler.CompileAssemblyFromSource(parameters, source);
                var compiledAssembly = compilerResult.CompiledAssembly;
                var type = compiledAssembly.GetType(string.Format("{0}.EvalClass", name));
                var method = type.GetMethod("Eval");
                return (Func<T, TResult>) Delegate.CreateDelegate(typeof (Func<T, TResult>), method);
            }
        }

        /// <summary>
        /// Gets the using statments needed to compile an OpenLisp.NET assembly
        /// targeting a .NET platform.
        /// </summary>
        /// <param name="usingStatements"></param>
        /// <returns></returns>
        private static string GetUsing(HashSet<string> usingStatements)
        {
            var result = new StringBuilder();
            foreach (var usingStatement in usingStatements)
            {
                result.AppendLine(string.Format("using {0};", usingStatement));
            }
            return result.ToString();
        }
    }
}
