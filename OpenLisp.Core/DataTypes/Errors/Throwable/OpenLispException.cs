namespace OpenLisp.Core.DataTypes.Errors.Throwable
{
    /// <summary>
    /// An exception that can be thrown by OpenLisp.NET
    /// </summary>
    public class OpenLispException : OpenLispThrowable
    {
        private object _value;

        /// <summary>
        /// The value of the <see cref="OpenLispException"/>
        /// </summary>
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// Constructor accepting an <see cref="object"/> parameter.
        /// </summary>
        /// <param name="value"></param>
        public OpenLispException(object value)
        {
            _value = value;
        }

        /// <summary>
        /// Constructor accepting a <see cref="string"/> parameter which is passed to :base(value).
        /// </summary>
        /// <param name="value"></param>
        public OpenLispException(string value) : base(value)
        {
            _value = value;
        }
    }
}