using System;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes.Errors.Throwable;
using OpenLisp.Core.StaticClasses;

namespace OpenLisp.Core.DataTypes
{
    public class OpenLispFunc : OpenLispVal
    {
        private Func<OpenLispList, OpenLispVal> _lambda = null;

        private OpenLispVal _ast = null;

        private Env _env = null;

        private OpenLispList _fparams;

        private bool _macro = false;

        public OpenLispFunc(Func<OpenLispList, OpenLispVal> lambda)
        {
            Lambda = lambda;
        }

        public OpenLispFunc(OpenLispVal ast, Env env, OpenLispList fparams, Func<OpenLispList, OpenLispVal> lambda)
        {
            Ast = ast;
            Env = env;
            FParams = fparams;
            Lambda = lambda;
        }

        public Func<OpenLispList, OpenLispVal> Lambda
        {
            get
            {
                return _lambda;
            }
            private set { _lambda = value; }
        }

        public OpenLispVal Ast
        {
            get
            {
                return _ast;
            }
            private set { _ast = value; }
        }

        public Env Env
        {
            get
            {
                return _env;
            }
            private set { _env = value; }
        }

        public Env GenEnv(OpenLispList args)
        {
            return new Env(Env, FParams, args);
        }

        public OpenLispList FParams
        {
            get
            {
                return _fparams;
            }
            private set { _fparams = value; }
        }

        public bool Macro
        {
            get { return _macro; }
            //private set { _macro = value; }
            set { _macro = value; }
        }

        /// <summary>
        /// Returns a string representation of an OpenLispFunc.
        /// 
        /// TODO: create a parameterized version that can override "builtin_function" per package.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Ast != null
            ? "<fn* " + Printer.PrStr(FParams, true) +
              " " + Printer.PrStr(Ast, true) + ">"
            : "<builtin_function " + Lambda + ">";

        public OpenLispVal Apply(OpenLispList args)
        {
            return Lambda(args);
        }
    }
}