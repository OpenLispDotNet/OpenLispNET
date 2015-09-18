using System;
using OpenLisp.Core.DataTypes;

namespace OpenLisp.Core.AbstractClasses
{
    public abstract class OpenLispVal
    {
        private OpenLispVal _meta;

        public OpenLispVal Meta
        {
            get { return _meta; }
            set { _meta = value; }
        }

        public virtual OpenLispVal Copy()
        {
            return (OpenLispVal)MemberwiseClone();
        }

        public virtual string ToString(bool printReadably)
        {
            return ToString();
        }

        public virtual bool ListQ()
        {
            return false;
        }

        /// <summary>
        /// An explicit conversion from OpenLispVal to an OpenLispString.
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator OpenLispVal(string v)
        {
            //throw new NotImplementedException();
            return new OpenLispString(v);
        }
    }
}