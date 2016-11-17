using System.Collections.Generic;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes;
using OpenLisp.Core.DataTypes.Errors.Throwable;

namespace OpenLisp.Core
{
    public class Env
    {
        private readonly Env _outer = null;

        private readonly Dictionary<string, OpenLispVal> _data = new Dictionary<string, OpenLispVal>();

        public Env(Env outer)
        {
            _outer = outer;
        }

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

        public Env Find(OpenLispSymbol key)
        {
            return _data.ContainsKey(key.Value) ? this : _outer?.Find(key);
        }

        public OpenLispVal Get(OpenLispSymbol key)
        {
            Env e = Find(key);

            if (e == null)
            {
                throw new OpenLispException("'" + key.Value + "' not found");
            }
            return e._data[key.Value];
        }

        public Env Set(OpenLispSymbol key, OpenLispVal value)
        {
            _data[key.Value] = value;

            return this;
        }

        public override string ToString()
        {
            return $"{GetType().Name}";
        }
    }
}