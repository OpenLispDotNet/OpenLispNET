using System;

namespace OpenLisp.Core.DataTypes.Errors.Throwable
{
    /// <summary>
    /// An exception that can be thrown by OpenLisp.NET
    /// </summary>
    public class OpenLispException : OpenLispThrowable
    {
        private DateTime _dateTimeStamp;
        private object _value;

        /// <summary>
        /// Private constructor to initialize the privately
        /// mutable _dateTimeStamp and assign <see cref="DateTime.Now"/>.
        /// 
        /// <seealso cref="OpenLispException.DateTimeStamp"/>
        /// </summary>
        private OpenLispException()
        {
            _dateTimeStamp = DateTime.Now;
        }

        /// <summary>
        /// Get the <see cref="DateTime"/> Stamp at
        /// the time of Exception.  This field should never
        /// be mutable by an external client.  Precise
        /// DateTime values are useful for tracing and auditing
        /// execution paths.
        /// </summary>
        public DateTime DateTimeStamp => _dateTimeStamp;

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