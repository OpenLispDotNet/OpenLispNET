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
    }
}