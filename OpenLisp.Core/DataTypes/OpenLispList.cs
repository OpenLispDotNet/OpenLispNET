using System;
using System.Collections.Generic;
using System.Linq;
using OpenLisp.Core.AbstractClasses;
using OpenLisp.Core.DataTypes.Errors.Throwable;
using OpenLisp.Core.StaticClasses;

namespace OpenLisp.Core.DataTypes
{
    /// <summary>
    /// Default List implementation for OpenLisp.NET
    ///
    /// TODO: replace List{OpenLispVal} with OpenLispSkipList{OpenLispVal}.
    /// TODO: aftere replacing backing type, perform benchmarks and tests on large collections: 100k+
    /// </summary>
    public class OpenLispList : OpenLispVal
    {
        /// <summary>
        /// Gets the <see cref="T:OpenLisp.Core.DataTypes.OpenLispList"/> at the specified index.
        /// </summary>
        /// <param name="index">Index.</param>
        public OpenLispVal this[int index]
        {
            get
            {
                var enumerator = Value.GetEnumerator();

                for (int i = 0; i < index; i++)
                {
                    enumerator.MoveNext();
                }

                return enumerator.Current;
            }
        }


        /// <summary>
        /// Opening token of an <see cref="OpenLispList"/>.
        /// </summary>
        public string Start = "(";

        /// <summary>
        /// Ending token of an <see cref="OpenLispList"/>.
        /// </summary>
        public string End = ")";

        //private List<OpenLispVal> _value;
        private OpenLispSkipList<OpenLispVal> _value;

        /// <summary>
        /// Get or Set the value of an <see cref="OpenLispList"/> instance.
        /// </summary>
        public OpenLispSkipList<OpenLispVal> Value
        {
            get
            {
                if (_value == null) throw new OpenLispException("Value is null.");
                return _value;
            }
            set { _value = value; }
        }

        /// <summary>
        /// Get the size of an <see cref="OpenLispList"/> collection.
        /// </summary>
        public int Size => Value.Count;

        /// <summary>
        /// Always return true when determining if this a list.
        /// </summary>
        /// <returns></returns>
        public override bool ListQ()
        {
            return true;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public OpenLispList()
        {
            Value = new OpenLispSkipList<OpenLispVal>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:OpenLisp.Core.DataTypes.OpenLispList"/> class.
        /// </summary>
        /// <param name="values">Values.</param>
        public OpenLispList(List<OpenLispVal> values): base() {
            Value.AddRange(values);
        }

        /// <summary>
        /// Constructor accepting a <see cref="List{OpenLispVal}"/> as a parameter.
        /// </summary>
        /// <param name="value"></param>
        public OpenLispList(OpenLispSkipList<OpenLispVal> value)
        {
            Value = value;
        }

        /// <summary>
        /// Constsructor accepting a <see cref="OpenLispVal"/> array for params. 
        /// </summary>
        /// <param name="values"></param>
        public OpenLispList(params OpenLispVal[] values)
        {
            Value = new OpenLispSkipList<OpenLispVal>();

            Conj(values);
        }

        /// <summary>
        /// Conj operation accepting a <see cref="OpenLispVal"/> array for params.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public OpenLispList Conj(params OpenLispVal[] values)
        {
            //foreach (var v in values)
            //{
            //    _value.Add(v);
            //}

            Value.AddRange(values.ToList());

            return this;
        }

        /// <summary>
        /// Pretty-prints an <see cref="OpenLispList"/> instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Start + Printer.Join(Value, " ", true) + End;
        }

        /// <summary>
        /// Prints a string representation of an <see cref="OpenLispList"/> with optional pretty-print.
        /// </summary>
        /// <param name="printReadably"></param>
        /// <returns></returns>
        public override string ToString(bool printReadably)
        {
            return Start + Printer.Join(Value, " ", printReadably) + End;
        }

        /// <summary>
        /// Retrieves the Nth member of an <see cref="OpenLispList"/> by index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        //public OpenLispVal Nth(int index)
        //{
        //    return Value.Count > index ? Value[index] : StaticOpenLispTypes.Nil;
        //}
        public OpenLispVal Nth(int index)
        {
            //return index != null ? Value[index] : StaticOpenLispTypes.Nil;
            var enumerator = Value.GetEnumerator();

            try { 
                for (int i = 1; i <= index; i++) {
                    if (i != index) { enumerator.MoveNext(); }

                    else if (i == index) { return enumerator.Current; }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return StaticOpenLispTypes.Nil;
            }

            return StaticOpenLispTypes.Nil;
        }

        /// <summary>
        /// Retrieves an indexed value from an <see cref="OpenLispList"/> or returns
        /// <see cref="StaticOpenLispTypes.Nil"/> if the index exceeds the size of the collection.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        //public OpenLispVal this[int index] => Value.Count > index ? Value[index] : StaticOpenLispTypes.Nil;
        public OpenLispVal this[OpenLispVal index] => index != null ? Value[index] : StaticOpenLispTypes.Nil;

        /// <summary>
        /// Returns either the rest of an <see cref="OpenLispList"/> or a new, empty instance.
        /// </summary>
        /// <returns></returns>
        public OpenLispList Rest()
        {
            //return Size > 0 ? new OpenLispList(Value.GetRange(1, Value.Count - 1)) : new OpenLispList();

            if (Size > 0) {

                var newSkipList = new OpenLispSkipList<OpenLispVal>();

                // Return OpenLispSkipList without the HEAD or lowest item on the tree
                var enumerator = Value.GetEnumerator();
                enumerator.MoveNext(); // skip HEAD!

                for (int i = 0; i <= Value.Count - 1; i++) {
                    newSkipList.Add(enumerator.Current);
                }

                return new OpenLispList(newSkipList);
            } else {
                return new OpenLispList();
            }
        }

        /// <summary>
        /// Retrieves a slice from the underlying array.
        /// </summary>
        /// <param name="start">The starting index.</param>
        /// <returns></returns>
        public virtual OpenLispList Slice(int start)
        {
            return new OpenLispList(Value.GetRange(start, Value.Count - 1));
            // TODO: implement Value.GetRange(start, Value.Count - 1)) via the following:

            //var newSkipList = new OpenLispSkipList<OpenLispVal>();

            //// 1. get enumerator
            //var enumerator = Value.GetEnumerator();

            //// 2. skip ahead until start
            //for (int i = 0; i < start; i++) {
            //    enumerator.MoveNext();
            //}

            //// 3. get a collecton from start to end
            //for (int i = 0; i < Value.Count - 1; i++) {
            //    newSkipList.Add(enumerator.Current);
            //    enumerator.MoveNext();
            //}

            //// 4. return that collection
            //return new OpenLispList(newSkipList);
        }

        /// <summary>
        /// Retrieves a slice from the underlying array.
        /// </summary>
        /// <param name="start">The starting index.</param>
        /// <param name="end">The ending index.</param>
        /// <returns></returns>
        public virtual OpenLispList Slice(int start, int end)
        {
            return new OpenLispList(Value.GetRange(start, end - start));
            // TODO: impelement Value.GetRange(start, end - start));

            //var newSkipList = new OpenLispSkipList<OpenLispVal>();

            //// 1. get enumerator
            //var enumerator = Value.GetEnumerator();

            //// 2. skip ahead until start
            //for (int i = 0; i < start; i++)
            //{
            //    enumerator.MoveNext();
            //}

            //// 3. get a collection until end
            //for (int i = 0; i < end - start; i++)
            //{
            //    newSkipList.Add(enumerator.Current);
            //    enumerator.MoveNext();
            //}

            //// 4. return that collection
            //return new OpenLispList(newSkipList);
        }
    }
}