using System;
using OpenLisp.Core;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;
using OpenLisp.Core.StaticClasses;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

namespace OpenLisp.Games.Unity
{

    public class LispMachine : MonoBehaviour
    {
        Func<string, Env, OpenLispVal> Re = (string str, Env env) => Repl.Eval(Repl.Read(str), env);

        public Env env = new Env(null);
        public Text Input;
        public Text Output;

        // Start is called before the first frame update
        void Start()
        {
            // Load each OpenLispSymbol in the core name space
            foreach (var entry in CoreNameSpace.Ns)
            {
                this.env.Set(new OpenLispSymbol(entry.Key), entry.Value);
            }

            // TODO: extract this from the Repl
            this.env.Set(new OpenLispSymbol("eval"),
                new OpenLispFunc(a => Repl.Eval(a[0], env)));
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void EvaluateInput()
        {
            if (this.Input == null)
            {
                Debug.LogError("Input or Output is null - can't read and eval an S-Expression.");
                return;
            }

            var inputExpression = this.Input.text;
            var outputValue = Re(inputExpression, this.env);
            if (outputValue != null)
            {
                Debug.Log($"S-Expression result: {outputValue}");
                Output.text = outputValue.ToString();
            }
        }
    }
}
