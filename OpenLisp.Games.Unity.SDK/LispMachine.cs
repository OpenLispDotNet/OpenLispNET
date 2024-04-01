using System;
using OpenLisp.Core;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.StaticClasses;
using UnityEngine;

namespace OpenLisp.Games.Unity.SDK
{
    public class LispMachine : MonoBehaviour
    {

        private static readonly Env LispEnv = new Env(null);
        private Func<string, OpenLispVal> ReadEval = (string str) => Repl.Eval(Repl.Read(str), LispEnv);
        public GameObject Input { get; set; }
    }
}
