using System;
using OpenLisp.Core.AbstractClasses;
using UnityEngine;

namespace OpenLisp.Games.Unity.DataTypes
{
    /// <summary>
    /// Unity GameObject boxed as an OpenLispVal.
    /// </summary>
    public class OpenLispGameObject : OpenLispVal
    {
        private GameObject _value;

        public new GameObject Value
        {
            get => _value;
            private set => _value = value;
        }

        public OpenLispGameObject()
        {
            Value = new GameObject();
        }

        public OpenLispGameObject(string name)
        {
            Value = new GameObject(name);
        }

        public OpenLispGameObject(string name, params Type[] components)
        {
            Value = new GameObject(name, components);
        }

        public OpenLispGameObject(GameObject value)
        {
            Value = value;
        }

        public OpenLispGameObject(OpenLispVal value)
        {
            Value = ((OpenLispGameObject)value).Value;
        }

        public new OpenLispGameObject Copy()
        {
            return this;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
