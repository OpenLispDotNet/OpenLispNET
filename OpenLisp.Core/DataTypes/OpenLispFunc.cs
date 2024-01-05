using System;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes.Errors.Throwable;
using OpenLisp.Core.StaticClasses;

namespace OpenLisp.Core.DataTypes
{
    /// <summary>
    /// Implementation of a func in OpenLisp.NET
    /// </summary>
    public class OpenLispFunc : OpenLispVal
    {
        private OpenLispList _fparams;

        // TODO: prove our macros work =)
        private bool _macro = false;

        /// <summary>
        /// Constructor accepting a <see cref="Func{T, TResult}"/>.
        /// </summary>
        /// <param name="lambda"></param>
        public OpenLispFunc(Func<OpenLispList, OpenLispVal> lambda)
        {
            Lambda = lambda;
        }

        /// <summary>
        /// Constructor accepting an <see cref="OpenLispVal"/>, an <see cref="Env"/>, an <see cref="OpenLispList"/>,
        /// and a <see cref="Func{T, TResult}"/>.
        /// </summary>
        /// <param name="ast"></param>
        /// <param name="env"></param>
        /// <param name="fparams"></param>
        /// <param name="lambda"></param>
        public OpenLispFunc(OpenLispVal ast, Env env, OpenLispList fparams, Func<OpenLispList, OpenLispVal> lambda)
        {
            Ast = ast;
            Env = env;
            FParams = fparams;
            Lambda = lambda;
        }

        /// <summary>
        /// Publicly Get and privately Set the <see cref="Func{T, TResult}"/>
        /// </summary>
        private Func<OpenLispList, OpenLispVal> Lambda { get; set; } = null;

        /// <summary>
        /// Publicly Get and privately Set the <see cref="OpenLispVal"/> representing the AST.
        /// </summary>
        public OpenLispVal Ast { get; private set; } = null;

        /// <summary>
        /// Publicly Get and privately Set the <see cref="Env"/>.
        /// </summary>
        private Env Env { get; set; } = null;

        /// <summary>
        /// Generates a new <see cref="Env"/> from an <see cref="OpenLispList"/> parameter.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Env GenEnv(OpenLispList args)
        {
            return new Env(Env, FParams, args);
        }

        /// <summary>
        /// Publicly Get and privately Set the <see cref="OpenLispList"/> parameters
        /// of an <see cref="OpenLispFunc"/>.
        /// </summary>
        private OpenLispList FParams
        {
            get => _fparams;
            set => _fparams = value;
        }

        /// <summary>
        /// Get or Set whether this <see cref="OpenLispList"/> is a macro.
        /// </summary>
        public bool Macro
        {
            get => _macro;
            //private set { _macro = value; }
            set => _macro = value;
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

        /// <summary>
        /// Apply the <see cref="OpenLispFunc"/> with an <see cref="OpenLispList"/> of parameters.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public OpenLispVal Apply(OpenLispList args)
        {
            return Lambda(args);
        }
    }
}
