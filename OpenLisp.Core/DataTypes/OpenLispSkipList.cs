//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text;
//using OpenLisp.Core.AbstractClasses;
//using OpenLisp.Core.StaticClasses;
//using static OpenLisp.Core.StaticClasses.StaticOpenLispTypes;

//namespace OpenLisp.Core.DataTypes
//{
//    /// <summary>
//    /// The basic data block of a Skip List
//    /// </summary>
//    public class SkipListNode<OpenLispVal> : IDisposable
//        where OpenLispVal : IComparable
//    {
//        private OpenLispVal value;
//        private SkipListNode<OpenLispVal> next;
//        private SkipListNode<OpenLispVal> previous;
//        private SkipListNode<OpenLispVal> above;
//        private SkipListNode<OpenLispVal> below;

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

//        public virtual SkipListNode<OpenLispVal> Next
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

//        public virtual SkipListNode<OpenLispVal> Previous
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

//        public virtual SkipListNode<OpenLispVal> Above
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

//        public virtual SkipListNode<OpenLispVal> Below
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

//        public SkipListNode(OpenLispVal value)
//        {
//            this.Value = value;
//        }

//        public void Dispose()
//        {
//            value = default(OpenLispVal);
//            next = null;
//            previous = null;
//            above = null;
//            previous = null;
//        }

//        public virtual bool IsHeader()
//        {
//            return this.GetType() == typeof(SkipListNodeHeader<OpenLispVal>);
//        }

//        public virtual bool IsFooter()
//        {
//            return this.GetType() == typeof(SkipListNodeFooter<OpenLispVal>);
//        }
//    }

//    /// <summary>
//    /// Represents a Skip List node that is the header of a level
//    /// </summary>
//    public class SkipListNodeHeader<OpenLispVal> : SkipListNode<OpenLispVal>
//        where OpenLispVal : IComparable
//    {
//        /// <summary>
//        /// Initializes a new instance of the <see cref="T:OpenLisp.Core.DataTypes.SkipListNodeHeader`1"/> class.
//        /// </summary>
//        public SkipListNodeHeader()
//        : base(default(OpenLispVal))
//        {
//        }
//    }

//    /// <summary>
//    /// Represents a Skip List node that is the footer of a level
//    /// </summary>
//    public class SkipListNodeFooter<OpenLispVal> : SkipListNode<OpenLispVal>
//        where OpenLispVal : IComparable
//    {
//        /// <summary>
//        /// Initializes a new instance of the <see cref="T:OpenLisp.Core.DataTypes.SkipListNodeFooter`1"/> class.
//        /// </summary>
//        public SkipListNodeFooter()
//        : base(default(OpenLispVal))
//        {
//        }
//    }

//    /// <summary>
//    /// Open lisp skip list.
//    /// </summary>
//    public class OpenLispSkipList<OpenLispVal> : ICollection<OpenLispVal>
//        where OpenLispVal : IComparable
//    {
//        internal SkipListNode<OpenLispVal> topLeft;
//        internal SkipListNode<OpenLispVal> bottomLeft;
//        internal Random random;
//        private int levels;
//        private int size;
//        private int maxLevels = int.MaxValue;

//        /// <summary>
//        /// Gets the <see cref="T:OpenLisp.Core.DataTypes.OpenLispSkipList`1"/> at the specified index.
//        /// </summary>
//        /// <param name="index">Index.</param>
//        public OpenLispVal this[int index]
//        {
//            get
//            {
//                var enumerator = this.GetEnumerator();

//                for (int i = 0; i < index; i++)
//                {
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
//        public virtual SkipListNode<OpenLispVal> Head
//        {
//            get
//            {
//                return bottomLeft;
//            }
//        }

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
//        protected SkipListNode<OpenLispVal> getEmptyLevel()
//        {
//            SkipListNode<OpenLispVal> negativeInfinity = new SkipListNodeHeader<OpenLispVal>();
//            SkipListNode<OpenLispVal> positiveInfinity = new SkipListNodeFooter<OpenLispVal>();

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
//                SkipListNode<OpenLispVal> currentNode = this.topLeft;

//                while (currentNode != this.bottomLeft) //do not remove the bottom level
//                {
//                    if (currentNode.IsHeader() && currentNode.Next.IsFooter())
//                    {
//                        SkipListNode<OpenLispVal> belowNode = currentNode.Below;

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
//                SkipListNode<OpenLispVal> newLevel = getEmptyLevel();

//                //Link down
//                newLevel.Below = this.topLeft;
//                this.topLeft.Above = newLevel;
//                this.topLeft = newLevel; //update reference to most top-left node

//                //Update counters
//                newLevelCount--;
//                this.levels++;
//            }

//            //Insert the value in the proper position, creating as many levels as was randomly determined
//            SkipListNode<OpenLispVal> currentNode = this.topLeft;
//            SkipListNode<OpenLispVal> lastNodeAbove = null; //keeps track of the upper-level nodes in a tower
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
//                SkipListNode<OpenLispVal> newNode = new SkipListNode<OpenLispVal>(value);
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
//        public virtual SkipListNode<OpenLispVal> Find(OpenLispVal value)
//        {
//            SkipListNode<OpenLispVal> foundNode = this.topLeft;

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
//        public virtual SkipListNode<OpenLispVal> FindLowest(OpenLispVal value)
//        {
//            SkipListNode<OpenLispVal> valueNode = this.Find(value);
//            return this.FindLowest(valueNode);
//        }

//        /// <summary>
//        /// Returns the lowest node on the first tower to match the input value
//        /// </summary>
//        public virtual SkipListNode<OpenLispVal> FindLowest(SkipListNode<OpenLispVal> valueNode)
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
//        public virtual SkipListNode<OpenLispVal> FindHighest(OpenLispVal value)
//        {
//            SkipListNode<OpenLispVal> valueNode = this.Find(value);
//            return this.FindHighest(valueNode);
//        }

//        /// <summary>
//        /// Returns the highest node on the first tower to match the input value
//        /// </summary>
//        public virtual SkipListNode<OpenLispVal> FindHighest(SkipListNode<OpenLispVal> valueNode)
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
//            SkipListNode<OpenLispVal> valueNode = this.FindHighest(value);
//            return this.Remove(valueNode);
//        }

//        /// <summary>
//        /// Removes a value or node from the Skip List
//        /// </summary>
//        public virtual bool Remove(SkipListNode<OpenLispVal> valueNode)
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
//                SkipListNode<OpenLispVal> currentNodeDown = valueNode;
//                while (currentNodeDown != null)
//                {
//                    //Remove right-left links
//                    SkipListNode<OpenLispVal> previousNode = currentNodeDown.Previous;
//                    SkipListNode<OpenLispVal> nextNode = currentNodeDown.Next;

//                    //Link the previous and next nodes to each other
//                    previousNode.Next = nextNode;
//                    nextNode.Previous = previousNode;

//                    SkipListNode<OpenLispVal> belowNode = currentNodeDown.Below; //scan down
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
//            SkipListNode<OpenLispVal> currentNode = this.Head;

//            while (currentNode != null)
//            {
//                SkipListNode<OpenLispVal> nextNode = currentNode.Next; //save reference to next node

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
//            SkipListNode<OpenLispVal> valueNode = this.FindLowest(value);
//            return this.GetHeight(valueNode);
//        }

//        /// <summary>
//        /// Gets the number of levels of a value in the Skip List
//        /// </summary>
//        public virtual int GetHeight(SkipListNode<OpenLispVal> valueNode)
//        {
//            int height = 0;
//            SkipListNode<OpenLispVal> currentNode = valueNode;

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
//            return new SkipListEnumerator(this);
//        }

//        /// <summary>
//        /// Gets the enumerator for the Skip List
//        /// </summary>
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return this.GetEnumerator();
//        }

//        internal void AddRange(List<OpenLispVal> values)
//        {
//            //throw new NotImplementedException();
//            foreach (var l in values)
//            {
//                Add(l);
//            }
//        }

//        internal OpenLispSkipList<OpenLispVal> GetRange(int start, int end)
//        {
//            var result = new OpenLispSkipList<OpenLispVal>();

//            //if (this.Count )

//            // 1. get enumerator
//            var enumerator = this.GetEnumerator();

//            // 2. set enumerator to start
//            if (start > 0)
//            {
//                //for (int i = 0; i < this.Count - end; i++) {
//                //    result.Add(enumerator.Current);
//                //}

//                for (int i = 0; i < start; i++)
//                {
//                    enumerator.MoveNext();
//                }
//            }

//            // 3. enumerate until end and grab collection
//            for (int i = 0; i < this.Count - end; i++)
//            {
//                result.Add(enumerator.Current);
//                enumerator.MoveNext();
//            }

//            // 4. return collection
//            return result;
//        }

//        /// <summary>
//        /// Enumerator for a Skip List. Scans across the lowest level of a Skip List.
//        /// </summary>
//        internal class SkipListEnumerator : IEnumerator<OpenLispVal>
//        {
//            private SkipListNode<OpenLispVal> current;
//            private OpenLispSkipList<OpenLispVal> skipList;

//            public SkipListEnumerator(OpenLispSkipList<OpenLispVal> skipList)
//            {
//                this.skipList = skipList;
//            }

//            public OpenLispVal Current
//            {
//                get
//                {
//                    //return current.Value.Equals(null) ? default(T) : current.Value;

//                    //return current.Value.Equals(null)
//                        //? current.Value : Nil.ToOpenLispVal();
//;
//                    //: current.Value.IsDeepEqual(default(T)) ? current.Value : default(T);

//                    return current.Value;

//                    //return current.Value != default(null) ? current.Value : Nil;

//                    //if (current.Value == (null)) {
//                    //    return default(OpenLispVal);
//                    //} else {
//                    //    return current.Value;
//                    //}

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
