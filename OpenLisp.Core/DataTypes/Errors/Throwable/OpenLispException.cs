namespace OpenLisp.Core.DataTypes.Errors.Throwable
{
    public class OpenLispException : OpenLispThrowable
    {
        private object _value;

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public OpenLispException(object value)
        {
            _value = value;
        }

        public OpenLispException(string value) : base(value)
        {
            _value = value;
        }
    }
}