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
                if (_lambda == null) throw new OpenLispException("Lambda is null.");
                return _lambda;
            }
            set { _lambda = value; }
        }

        public OpenLispVal Ast
        {
            get
            {
                if (_ast == null) throw new OpenLispException("Ast is null.");
                return _ast;
            }
            set { _ast = value; }
        }

        public Env Env
        {
            get
            {
                if (_env == null) throw new OpenLispException("Env is null.");
                return _env;
            }
            set { _env = value; }
        }

        public Env GenEnv(OpenLispList args)
        {
            return new Env(Env, FParams, args);
        }

        public OpenLispList FParams
        {
            get
            {
                if (_fparams == null) throw new OpenLispException("FParams is null.");
                return _fparams;
            }
            set { _fparams = value; }
        }

        public bool Macro
        {
            get { return _macro; }
            set { _macro = value; }
        }

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