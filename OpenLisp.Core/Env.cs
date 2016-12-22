using System.Collections.Generic;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;
using OpenLisp.Core.DataTypes.Errors.Throwable;

namespace OpenLisp.Core
{
    /// <summary>
    /// Manages the OpenLisp.NET environment containing OpenLispSymbol and OpenLispVal objects.
    /// </summary>
    public class Env
    {
        private readonly Env _outer = null;

        private readonly Dictionary<string, OpenLispVal> _data = new Dictionary<string, OpenLispVal>();

        /// <summary>
        /// Constructor accepting an outer <see cref="Env"/>.
        /// </summary>
        /// <param name="outer"><see cref="Env"/></param>
        public Env(Env outer)
        {
            _outer = outer;
        }

        /// <summary>
        /// Constructor accepting an outer <see cref="Env"/> with binds and expressions.
        /// </summary>
        /// <param name="outer"><see cref="Env"/></param>
        /// <param name="binds"><see cref="OpenLispList"/></param>
        /// <param name="expressions"><see cref="OpenLispList"/></param>
        public Env(Env outer, OpenLispList binds, OpenLispList expressions)
        {
            _outer = outer;

            for (int i = 0; i < binds.Size; i++)
            {
                string symbol = ((OpenLispSymbol) binds.Nth(i)).Value;

                if (symbol == "&")
                {
                    _data[((OpenLispSymbol) binds.Nth(i + 1)).Value] = expressions.Slice(i);
                    break;
                }

                _data[symbol] = expressions.Nth(i);
            }
        }

        /// <summary>
        /// Finds an <see cref="OpenLispSymbol"/> in <see cref="Env._data"/>.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Env Find(OpenLispSymbol key)
        {
            return _data.ContainsKey(key.Value) ? this : _outer?.Find(key);
        }

        /// <summary>
        /// Gets an <see cref="OpenLispSymbol"/> from <see cref="Env"/>.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public OpenLispVal Get(OpenLispSymbol key)
        {
            Env e = Find(key);

            if (e == null)
            {
                throw new OpenLispException("'" + key.Value + "' not found");
            }
            return e._data[key.Value];
        }

        /// <summary>
        /// Sets an <see cref="OpenLispSymbol"/> key's value to an instance of <see cref="OpenLispVal"/>.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Env Set(OpenLispSymbol key, OpenLispVal value)
        {
            _data[key.Value] = value;

            return this;
        }

        /// <summary>
        /// Return the name of the Type when <see cref="Env.ToString"/> is invoked.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{GetType().Name}";
        }
    }
}