using System;
using Codice.Client.GameUI.Explorer;
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

        public Env LispEnv = new(null);
        public Text Input;
        public Text Output;

        /**
         * Build the Lisp Environment with a standard library.
         *
         * Start is called before the first frame update
         */
        void Start()
        {
            // Load each OpenLispSymbol in the core name space
            foreach (var entry in CoreNameSpace.Ns)
            {
                LispEnv.Set(new OpenLispSymbol(entry.Key), entry.Value);
            }

            // TODO: extract this from the Repl
            LispEnv.Set(new OpenLispSymbol("eval"),
                new OpenLispFunc(a => Repl.Eval(a[0], LispEnv)));

            LispEnv.Set(new OpenLispSymbol("game-object-invoke"), new OpenLispFunc(a =>
            {
                try
                {
                    var gameObjectToCall = GameObject.Find(a[0].ToString());
                    if (gameObjectToCall != null)
                    {
                        gameObjectToCall.
                    }
                }
                catch (Exception e)
                {
                    return new OpenLispList(new OpenLispString("error"), new OpenLispString(e.Message));
                }
            }));
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void EvaluateInput()
        {
            if (Input == null || Output == null)
            {
                Debug.LogError("Input or Output is null - can't read and eval an S-Expression.");
                return;
            }

            var inputExpression = this.Input.text;
            var outputValue = Re(inputExpression, this.LispEnv);
            if (outputValue != null)
            {
                Debug.Log($"S-Expression result: {outputValue.Value}");
                Output.text = outputValue.ToString();
            }
        }
    }
}
