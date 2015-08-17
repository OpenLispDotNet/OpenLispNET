using System;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.StaticClasses;

namespace OpenLisp.Core.DataTypes
{
    public class OpenLispInt : OpenLispVal
    {
        private readonly Int64 _value;

        public Int64 Value => _value;

        public OpenLispInt(Int64 value)
        {
            _value = value;
        }

        public new OpenLispInt Copy()
        {
            return this;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public override string ToString(bool printReadably)
        {
            return _value.ToString();
        }

        public static OpenLispConstant operator <(OpenLispInt a, OpenLispInt b)
        {
            return a.Value < b.Value ? StaticOpenLispTypes.True : StaticOpenLispTypes.False;
        }

        public static OpenLispConstant operator <=(OpenLispInt a, OpenLispInt b)
        {
            return a.Value < b.Value ? StaticOpenLispTypes.True : StaticOpenLispTypes.False;
        }

        public static OpenLispConstant operator >(OpenLispInt a, OpenLispInt b)
        {
            return a.Value < b.Value ? StaticOpenLispTypes.True : StaticOpenLispTypes.False;
        }

        public static OpenLispConstant operator >=(OpenLispInt a, OpenLispInt b)
        {
            return a.Value < b.Value ? StaticOpenLispTypes.True : StaticOpenLispTypes.False;
        }

        public static OpenLispInt operator +(OpenLispInt a, OpenLispInt b)
        {
            return new OpenLispInt(a.Value + b.Value);
        }

        public static OpenLispInt operator -(OpenLispInt a, OpenLispInt b)
        {
            return new OpenLispInt(a.Value - b.Value);
        }

        public static OpenLispInt operator *(OpenLispInt a, OpenLispInt b)
        {
            return new OpenLispInt(a.Value * b.Value);
        }

        public static OpenLispInt operator /(OpenLispInt a, OpenLispInt b)
        {
            return new OpenLispInt(a.Value / b.Value);
        }
    }
}