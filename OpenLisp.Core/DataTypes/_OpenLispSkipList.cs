///**
// * OpenLisp.NET is intended to provide a fast, portable LISP implementation
// * targeting the .NET Framework.  As we have grown in our experience as a Sr.
// * Engineer, we have needed the ultimate data type for our distributed system
// * needs.  We have found such a type in the Skip List: all the beauty and 
// * elegance of a list with the backing of a tree and an average space-time
// * complexity for all operations clocking in at log(n).
// * 
// * The following implementation is based on the code found at:
// * 
// *     * https://jonlabelle.com/snippets/view/csharp/skip-list-in-c
// * 
// * Our intention is to make this into an implementation of an OpenLispVal.
// * 
// * TODO: After we add the skip list, we'll need to provide a radix sort...
// * TODO: After testing this collection, make it the core abstraction behind other collections...
// */

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using DeepEqual.Syntax;
//using OpenLisp.Core.AbstractClasses;
//using OpenLisp.Core.StaticClasses;

//namespace OpenLisp.Core.DataTypes
//{
//    /// <summary>
//    /// The basic data block of a Skip List composed of OpenLispVal.
//    /// 
//    /// And, of course, every skip list node is also an OpenLispVal!
//    /// </summary>
//    public class OpenLispSkipListNode<OpenLispVal> : IDisposable where OpenLispVal : IComparable
//    {

//        #region IDisposable Support
//        private bool disposedValue = false; // To detect redundant calls

//        /// <summary>
//        /// Dispose the object.
//        /// </summary>
//        /// <param name="disposing">If set to <c>true</c> disposing.</param>
//        protected virtual void Dispose(bool disposing)
//        {
//            if (!disposedValue)
//            {
//                if (disposing)
//                {
//                    // TODO: dispose managed state (managed objects).
//                    value = default(OpenLispVal);
//                }

//                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.

//                // TODO: set large fields to null.
//                next = null;
//                previous = null;
//                above = null;
//                previous = null;

//                disposedValue = true;
//            }
//        }

//        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
//        // ~OpenLispSkipListNode() {
//        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
//        //   Dispose(false);
//        // }

//        // This code added to correctly implement the disposable pattern.
//        /// <summary>
//        /// Releases all resource used by the <see cref="T:OpenLisp.Core.DataTypes.OpenLispSkipListNode`1"/> object.
//        /// </summary>
//        public void Dispose()
//        {
//            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
//            Dispose(true);
//            // TODO: uncomment the following line if the finalizer is overridden above.
//            // GC.SuppressFinalize(this);
//        }
//        #endregion


//        private OpenLispVal value = default(OpenLispVal);
//        private OpenLispSkipListNode<OpenLispVal> next;
//        private OpenLispSkipListNode<OpenLispVal> previous;
//        private OpenLispSkipListNode<OpenLispVal> above;
//        private OpenLispSkipListNode<OpenLispVal> below;

//        /// <summary>
//        /// Gets or sets the value.
//        /// </summary>
//        /// <value>The value.</value>
//        public virtual OpenLispVal Value
//        {
//            get
//            {
//                return value;
//            }
//            set
//            {
//                this.value = value;
//            }
//        }

//        /// <summary>
//        /// Gets or sets the next value.
//        /// </summary>
//        /// <value>The next.</value>
//        public virtual OpenLispSkipListNode<OpenLispVal> Next
//        {
//            get
//            {
//                return next;
//            }
//            set
//            {
//                next = value;
//            }
//        }

//        /// <summary>
//        /// Gets or sets the previous value.
//        /// </summary>
//        /// <value>The previous.</value>
//        public virtual OpenLispSkipListNode<OpenLispVal> Previous
//        {
//            get
//            {
//                return previous;
//            }
//            set
//            {
//                previous = value;
//            }
//        }

//        /// <summary>
//        /// Gets or sets the value above.
//        /// </summary>
//        /// <value>The above.</value>
//        public virtual OpenLispSkipListNode<OpenLispVal> Above
//        {
//            get
//            {
//                return above;
//            }
//            set
//            {
//                above = value;
//            }
//        }

//        /// <summary>
//        /// Gets or sets the value below.
//        /// </summary>
//        /// <value>The below.</value>
//        public virtual OpenLispSkipListNode<OpenLispVal> Below
//        {
//            get
//            {
//                return below;
//            }
//            set
//            {
//                below = value;
//            }
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="T:OpenLisp.Core.DataTypes.OpenLispSkipListNode`1"/> class.
//        /// </summary>
//        /// <param name="value">Value.</param>
//        public OpenLispSkipListNode(OpenLispVal value) => Value = value;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="T:OpenLisp.Core.DataTypes.OpenLispSkipListNode`1"/> class.
//        /// </summary>
//        public OpenLispSkipListNode() => Value = default(OpenLispVal);

//        /// <summary>
//        /// Is this the header?
//        /// </summary>
//        /// <returns><c>true</c>, if header was ised, <c>false</c> otherwise.</returns>
//        public virtual bool IsHeader()
//        {
//            return this.GetType() == typeof(OpenLispSkipListNodeHeader<OpenLispVal>);
//        }

//        /// <summary>
//        /// Is this the footer?
//        /// </summary>
//        /// <returns><c>true</c>, if footer was ised, <c>false</c> otherwise.</returns>
//        public virtual bool IsFooter()
//        {
//            return this.GetType() == typeof(OpenLispSkipListNodeFooter<OpenLispVal>);
//        }
//    }

//    /// <summary>
//    /// Represents a Skip List node that is the header of a level
//    /// </summary>
//    class OpenLispSkipListNodeHeader<OpenLispVal> : OpenLispSkipListNode<OpenLispVal>
//        where OpenLispVal : IComparable
//    {
//        public OpenLispSkipListNodeHeader()
//        : base(default(OpenLispVal))
//        {
//        }
//    }

//    /// <summary>
//    /// Represents a Skip List node that is the footer of a level
//    /// </summary>
//    class OpenLispSkipListNodeFooter<OpenLispVal> : OpenLispSkipListNode<OpenLispVal>
//        where OpenLispVal : IComparable
//    {
//        public OpenLispSkipListNodeFooter()
//        : base(default(OpenLispVal))
//        {
//        }
//    }

//    /// <summary>
//    /// OpenLisp Skip List.
//    /// </summary>
//    public class OpenLispSkipList<OpenLispVal> : ICollection<OpenLispVal> where OpenLispVal : IComparable
//    {
//        internal OpenLispSkipListNode<OpenLispVal> topLeft;
//        internal OpenLispSkipListNode<OpenLispVal> bottomLeft;
//        internal Random random;
//        private int levels;
//        private int size;
//        private int maxLevels = int.MaxValue;

//        /// <summary>
//        /// Gets or sets the <see cref="T:OpenLisp.Core.DataTypes.OpenLispSkipList`1"/> at the specified index.
//        /// </summary>
//        /// <param name="index">Index.</param>
//        public OpenLispVal this[OpenLispVal index]
//        {
//            get => this.Find(index).Value;
//            set => this.Add(index);
//        }

//        /// <summary>
//        /// Gets the <see cref="T:OpenLisp.Core.DataTypes.OpenLispSkipList`1"/> at the specified index.
//        /// </summary>
//        /// <param name="index">Index.</param>
//        public OpenLispVal this[int index]
//        {
//            get 
//            {
//                var enumerator = this.GetEnumerator();

//                for (int i = 0; i < index; i++) {
//                    enumerator.MoveNext();
//                }

//                return enumerator.Current;
//            }
//        }

//        /// <summary>
//        /// Gets the levels.
//        /// </summary>
//        /// <value>The levels.</value>
//        public virtual int Levels
//        {
//            get
//            {
//                return levels;
//            }
//        }

//        /// <summary>
//        /// Gets or sets the max levels.
//        /// </summary>
//        /// <value>The max levels.</value>
//        public virtual int MaxLevels
//        {
//            get
//            {
//                return maxLevels;
//            }
//            set
//            {
//                maxLevels = value;
//            }
//        }

//        /// <summary>
//        /// Gets the count.
//        /// </summary>
//        /// <value>The count.</value>
//        public virtual int Count
//        {
//            get
//            {
//                return size;
//            }
//        }

//        /// <summary>
//        /// Gets a value indicating whether this <see cref="T:OpenLisp.Core.DataTypes.OpenLispSkipList`1"/> is read only.
//        /// </summary>
//        /// <value><c>true</c> if is read only; otherwise, <c>false</c>.</value>
//        public virtual bool IsReadOnly
//        {
//            get
//            {
//                return false;
//            }
//        }

//        /// <summary>
//        /// Gets the head.
//        /// </summary>
//        /// <value>The head.</value>
//        public virtual OpenLispSkipListNode<OpenLispVal> Head => bottomLeft;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="T:OpenLisp.Core.DataTypes.OpenLispSkipList`1"/> class.
//        /// </summary>
//        public OpenLispSkipList()
//        {
//            topLeft = getEmptyLevel(); //create an empty level
//            bottomLeft = topLeft;
//            levels = 1; //update the level count
//            size = 0; //no elements added
//            random = new Random(); //used for adding new values
//        }

//        /// <summary>
//        /// Creates an empty level with a header and footer node
//        /// </summary>
//        protected OpenLispSkipListNode<OpenLispVal> getEmptyLevel()
//        {
//            OpenLispSkipListNode<OpenLispVal> negativeInfinity = new OpenLispSkipListNodeHeader<OpenLispVal>();
//            OpenLispSkipListNode<OpenLispVal> positiveInfinity = new OpenLispSkipListNodeFooter<OpenLispVal>();

//            negativeInfinity.Next = positiveInfinity;
//            positiveInfinity.Previous = negativeInfinity;

//            return negativeInfinity;
//        }

//        /// <summary>
//        /// Randomly determines how many levels to add
//        /// </summary>
//        protected int getRandomLevels()
//        {
//            int newLevels = 0;
//            while (random.Next(0, 2) == 1 && newLevels < maxLevels) //1 is heads, 0 is tails
//            {
//                newLevels++;
//            }
//            return newLevels;
//        }

//        /// <summary>
//        /// Removes all the empty levels leftover in the Skip List
//        /// </summary>
//        protected void clearEmptyLevels()
//        {
//            if (this.levels > 1) //more than one level, don't want to remove bottom level
//            {
//                OpenLispSkipListNode<OpenLispVal> currentNode = this.topLeft;

//                while (currentNode != this.bottomLeft) //do not remove the bottom level
//                {
//                    if (currentNode.IsHeader() && currentNode.Next.IsFooter())
//                    {
//                        OpenLispSkipListNode<OpenLispVal> belowNode = currentNode.Below;

//                        //Remove the empty level

//                        //Update pointers
//                        topLeft = currentNode.Below;

//                        //Remove links
//                        currentNode.Next.Dispose();
//                        currentNode.Dispose();

//                        //Update counters
//                        this.levels--;

//                        currentNode = belowNode; //scan down
//                    }
//                    else
//                    {
//                        break;    //a single non-emtpy level means the rest of the levels are not empty
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// Add a value to the Skip List
//        /// </summary>
//        public virtual void Add(OpenLispVal value)
//        {
//            int valueLevels = getRandomLevels(); //determine height of value's tower

//            //Add levels to entire list if necessary
//            int newLevelCount = valueLevels - this.levels; //number of levels missing
//            while (newLevelCount > 0)
//            {
//                //Create new level
//                OpenLispSkipListNode<OpenLispVal> newLevel = getEmptyLevel();

//                //Link down
//                newLevel.Below = this.topLeft;
//                this.topLeft.Above = newLevel;
//                this.topLeft = newLevel; //update reference to most top-left node

//                //Update counters
//                newLevelCount--;
//                this.levels++;
//            }

//            //Insert the value in the proper position, creating as many levels as was randomly determined
//            OpenLispSkipListNode<OpenLispVal> currentNode = this.topLeft;
//            OpenLispSkipListNode<OpenLispVal> lastNodeAbove = null; //keeps track of the upper-level nodes in a tower
//            int currentLevel = this.levels - 1;

//            while (currentLevel >= 0 && currentNode != null)
//            {
//                if (currentLevel > valueLevels) //too high on the list, nothing would be added to this level
//                {
//                    currentNode = currentNode.Below; //scan down
//                    currentLevel--; //going one level lower
//                    continue; //skip adding to this level
//                }

//                //Add the value to the current level

//                //Find the biggest value on the current level that is less than the value to be added
//                while (currentNode.Next != null)
//                {
//                    if (!currentNode.Next.IsFooter() && currentNode.Next.Value.CompareTo(value) < 0) //smaller
//                    {
//                        currentNode = currentNode.Next;    //keep scanning across
//                    }
//                    else
//                    {
//                        break;    //the next node would be bigger than the value
//                    }

//                }

//                //Insert the value right after the node found
//                OpenLispSkipListNode<OpenLispVal> newNode = new OpenLispSkipListNode<OpenLispVal>(value);
//                newNode.Next = currentNode.Next;
//                newNode.Previous = currentNode;
//                newNode.Next.Previous = newNode;
//                currentNode.Next = newNode;

//                //Link down/up the tower
//                if (lastNodeAbove != null) //is this node part of a tower?
//                {
//                    lastNodeAbove.Below = newNode;
//                    newNode.Above = lastNodeAbove;
//                }
//                lastNodeAbove = newNode; //start/continue tower

//                //Scan down
//                currentNode = currentNode.Below;
//                currentLevel--;
//            }

//            this.size++; //update count
//        }

//        /// <summary>
//        /// Returns the first node whose value matches the input value
//        /// </summary>
//        public virtual OpenLispSkipListNode<OpenLispVal> Find(OpenLispVal value)
//        {
//            OpenLispSkipListNode<OpenLispVal> foundNode = this.topLeft;

//            //Look for the highest-level node with an element value matching the parameter value
//            while (foundNode != null && foundNode.Next != null)
//            {
//                if (!foundNode.Next.IsFooter() && foundNode.Next.Value.CompareTo(value) < 0) //next node's value is still smaller
//                {
//                    foundNode = foundNode.Next;    //keep scanning across
//                }
//                else
//                {
//                    if (!foundNode.Next.IsFooter() && foundNode.Next.Value.Equals(value)) //value found
//                    {
//                        foundNode = foundNode.Next;
//                        break;
//                    }
//                    else
//                    {
//                        foundNode = foundNode.Below;    //element not in this level, scan down
//                    }
//                }
//            }

//            return foundNode;
//        }

//        /// <summary>
//        /// Returns the lowest node on the first tower to match the input value
//        /// </summary>
//        public virtual OpenLispSkipListNode<OpenLispVal> FindLowest(OpenLispVal value)
//        {
//            OpenLispSkipListNode<OpenLispVal> valueNode = this.Find(value);
//            return this.FindLowest(valueNode);
//        }

//        /// <summary>
//        /// Returns the lowest node on the first tower to match the input value
//        /// </summary>
//        public virtual OpenLispSkipListNode<OpenLispVal> FindLowest(OpenLispSkipListNode<OpenLispVal> valueNode)
//        {
//            if (valueNode == null)
//            {
//                return null;
//            }
//            else
//            {
//                //Scan down to the lowest level
//                while (valueNode.Below != null)
//                {
//                    valueNode = valueNode.Below;
//                }
//                return valueNode;
//            }
//        }

//        /// <summary>
//        /// Returns the highest node on the first tower to match the input value
//        /// </summary>
//        public virtual OpenLispSkipListNode<OpenLispVal> FindHighest(OpenLispVal value)
//        {
//            OpenLispSkipListNode<OpenLispVal> valueNode = this.Find(value);
//            return this.FindHighest(valueNode);
//        }

//        /// <summary>
//        /// Returns the highest node on the first tower to match the input value
//        /// </summary>
//        public virtual OpenLispSkipListNode<OpenLispVal> FindHighest(OpenLispSkipListNode<OpenLispVal> valueNode)
//        {
//            if (valueNode == null)
//            {
//                return null;
//            }
//            else
//            {
//                //Scan up to the highest level
//                while (valueNode.Above != null)
//                {
//                    valueNode = valueNode.Above;
//                }
//                return valueNode;
//            }
//        }

//        /// <summary>
//        /// Returns whether a value exists in the Skip List
//        /// </summary>
//        public virtual bool Contains(OpenLispVal value)
//        {
//            return (this.Find(value) != null);
//        }

//        /// <summary>
//        /// Removes a value or node from the Skip List
//        /// </summary>
//        public virtual bool Remove(OpenLispVal value)
//        {
//            OpenLispSkipListNode<OpenLispVal> valueNode = this.FindHighest(value);
//            return this.Remove(valueNode);
//        }

//        /// <summary>
//        /// Removes a value or node from the Skip List
//        /// </summary>
//        public virtual bool Remove(OpenLispSkipListNode<OpenLispVal> valueNode)
//        {
//            if (valueNode == null)
//            {
//                return false;
//            }
//            else
//            {
//                //Make sure node is top-level node in it's tower
//                if (valueNode.Above != null)
//                {
//                    valueNode = this.FindHighest(valueNode);
//                }

//                //---Delete nodes going down the tower
//                OpenLispSkipListNode<OpenLispVal> currentNodeDown = valueNode;
//                while (currentNodeDown != null)
//                {
//                    //Remove right-left links
//                    OpenLispSkipListNode<OpenLispVal> previousNode = currentNodeDown.Previous;
//                    OpenLispSkipListNode<OpenLispVal> nextNode = currentNodeDown.Next;

//                    //Link the previous and next nodes to each other
//                    previousNode.Next = nextNode;
//                    nextNode.Previous = previousNode;

//                    OpenLispSkipListNode<OpenLispVal> belowNode = currentNodeDown.Below; //scan down
//                    currentNodeDown.Dispose(); //unlink previous

//                    currentNodeDown = belowNode;
//                }

//                //update counter
//                this.size--;

//                //Clean up the Skip List by removing levels that are now empty
//                this.clearEmptyLevels();

//                return true;
//            }
//        }

//        /// <summary>
//        /// Removes all values in the Skip List
//        /// </summary>
//        public virtual void Clear()
//        {
//            OpenLispSkipListNode<OpenLispVal> currentNode = this.Head;

//            while (currentNode != null)
//            {
//                OpenLispSkipListNode<OpenLispVal> nextNode = currentNode.Next; //save reference to next node

//                if (!currentNode.IsHeader() && !currentNode.IsFooter())
//                {
//                    this.Remove(currentNode);
//                }

//                currentNode = nextNode;
//            }
//        }

//        /// <summary>
//        /// Copies the values of the Skip List to an array
//        /// </summary>
//        public virtual void CopyTo(OpenLispVal[] array)
//        {
//            CopyTo(array, 0);
//        }

//        /// <summary>
//        /// Copies the values of the Skip List to an array
//        /// </summary>
//        public virtual void CopyTo(OpenLispVal[] array, int startIndex)
//        {
//            IEnumerator<OpenLispVal> enumerator = this.GetEnumerator();

//            for (int i = startIndex; i < array.Length; i++)
//            {
//                if (enumerator.MoveNext())
//                {
//                    array[i] = enumerator.Current;
//                }
//                else
//                {
//                    break;
//                }
//            }
//        }

//        /// <summary>
//        /// Gets the number of levels of a value in the Skip List
//        /// </summary>
//        public virtual int GetHeight(OpenLispVal value)
//        {
//            OpenLispSkipListNode<OpenLispVal> valueNode = this.FindLowest(value);
//            return this.GetHeight(valueNode);
//        }

//        /// <summary>
//        /// Gets the number of levels of a value in the Skip List
//        /// </summary>
//        public virtual int GetHeight(OpenLispSkipListNode<OpenLispVal> valueNode)
//        {
//            int height = 0;
//            OpenLispSkipListNode<OpenLispVal> currentNode = valueNode;

//            //Move all the way down to the bottom first
//            while (currentNode.Below != null)
//            {
//                currentNode = currentNode.Below;
//            }

//            //Count going back up to the top
//            while (currentNode != null)
//            {
//                height++;
//                currentNode = currentNode.Above;
//            }

//            return height;
//        }

//        /// <summary>
//        /// Gets the enumerator for the Skip List
//        /// </summary>
//        public IEnumerator<OpenLispVal> GetEnumerator()
//        {
//            return new OpenLispSkipListEnumerator(this);
//        }

//        /// <summary>
//        /// Gets the enumerator for the Skip List
//        /// </summary>
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return this.GetEnumerator();
//        }

//        internal void AddRange(List<OpenLispVal> list)
//        {
//            foreach (var l in list) {
//                Add((OpenLispVal)l);
//            }
//        }

//        internal OpenLispSkipList<OpenLispVal> GetRange(int start, int end)
//        {
//            var result = new OpenLispSkipList<OpenLispVal>();

//            //if (this.Count )

//            // 1. get enumerator
//            var enumerator = this.GetEnumerator();

//            // 2. set enumerator to start
//            if (start > 0) { 
//                //for (int i = 0; i < this.Count - end; i++) {
//                //    result.Add(enumerator.Current);
//                //}

//                for (int i = 0; i < start; i++) {
//                    enumerator.MoveNext();
//                }
//            }

//            // 3. enumerate until end and grab collection
//            for (int i = 0; i < this.Count - end; i++) {
//                result.Add(enumerator.Current);
//                enumerator.MoveNext();
//            }

//            // 4. return collection
//            return result;
//        }

//        /// <summary>
//        /// Enumerator for a Skip List. Scans across the lowest level of a Skip List.
//        /// </summary>
//        internal class OpenLispSkipListEnumerator : IEnumerator<OpenLispVal> 
//        {
//            private OpenLispSkipListNode<OpenLispVal> current;
//            private OpenLispSkipList<OpenLispVal> skipList;

//            public OpenLispSkipListEnumerator(OpenLispSkipList<OpenLispVal> skipList)
//            {
//                this.skipList = skipList;
//            }

//            public OpenLispVal Current
//            {
//                get
//                {

//                    //return current.Value.Equals(null)
//                    //? default(OpenLispVal)
//                    //: current.Value.IsDeepEqual(default(OpenLispVal)) ? current.Value : default(OpenLispVal);

//                    return current.Value;
//                }
//            }

//            object IEnumerator.Current
//            {
//                get
//                {
//                    return this.Current;
//                }
//            }

//            public void Dispose()
//            {
//                current = null;
//            }

//            public void Reset()
//            {
//                current = null;
//            }

//            public bool MoveNext()
//            {
//                if (current == null)
//                {
//                    current = this.skipList.Head.Next;    //Head is header node, start after
//                }
//                else
//                {
//                    current = current.Next;
//                }

//                if (current != null && current.IsFooter())
//                {
//                    current = null;    //end of list
//                }

//                return (current != null);
//            }
//        }
//    }
//}
