#region *   License     *
/*
    SimpleHelpers - MemoryCache   

    Copyright © 2013 Khalid Salomão

    Permission is hereby granted, free of charge, to any person
    obtaining a copy of this software and associated documentation
    files (the “Software”), to deal in the Software without
    restriction, including without limitation the rights to use,
    copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the
    Software is furnished to do so, subject to the following
    conditions:

    The above copyright notice and this permission notice shall be
    included in all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND,
    EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
    OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
    HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
    WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
    FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
    OTHER DEALINGS IN THE SOFTWARE. 

    License: http://www.opensource.org/licenses/mit-license.php
    Website: https://github.com/khalidsalomao/SimpleHelpers.Net
 */
#endregion

using System;
using System.Linq;

namespace OpenLisp.Core.SimpleHelpers
{
    /// <summary>
    /// Simple lightweight object in-memory cache, with a background timer to remove expired objects.
    /// Fast in-memory cache for data that are expensive to create and can be used in a thread-safe manner.
    /// All stored items are kept in concurrent data structures (ConcurrentDictionary) to allow multi-thread usage of the MemoryCache static methods.
    /// Note that the stored objects must be **thread-safe**, since the same instace of an object can and will be returned by multiple calls of *Get* methods. 
    /// If you wish to use non-thread safe object instances you must use the *Remove* method to atomically (safelly) get and remove the object instance from the cache.
    /// Note: this nuget package contains c# source code and depends on System.Collections.Concurrent introduced in .Net 4.0.
    /// </summary>
    /// <example>
    /// // setup:
    /// // set a duration of an item in the cache to 1 second
    /// MemoryCache<string>.Expiration = TimeSpan.FromSeconds (1);
    /// // set the internal maintenance timed task to run each 500 ms removing expired items
    /// MemoryCache<string>.MaintenanceStep = TimeSpan.FromMilliseconds (500);
    /// // Our event, if we want to treat the removed expired items
    /// MemoryCache<string>.OnExpiration += (string key, string item) => {};
    /// 
    /// // Simple usage:
    /// // store an item:
    /// MemoryCache<string>.Set ("k1", "test");
    /// 
    /// // get it back (if it is still valid, otherwise we get null):
    /// string value = MemoryCache<string>.Get ("k1");
    /// </example>    
    /// <remarks>
    /// Note that the stored objects must be thread-safe, since the same instace of an object can and will be returned
    /// by multiple calls of Get methods. To avoid this you must use the Remove method, to atomically (safelly) 
    /// get and remove the object instance of the cache.
    /// </remarks>
    public class MemoryCache<T> where T : class
    {
        private static readonly System.Collections.Concurrent.ConcurrentDictionary <string, CachedItem> MCacheMap = new System.Collections.Concurrent.ConcurrentDictionary<string, CachedItem> (StringComparer.Ordinal);

        private static TimeSpan _mTimeout = TimeSpan.FromMinutes (5);
        
        private static TimeSpan _mMaintenanceStep = TimeSpan.FromMinutes (5);
        
        private static bool _mIgnoreNullValues = true;

        /// <summary>
        /// Expiration TimeSpan of stored items.
        /// Default value is 5 minutes.
        /// </summary>
        public static TimeSpan Expiration
        {
            get => _mTimeout;
            set => _mTimeout = value;
        }

        /// <summary>
        /// Interval duration between checks for expired cached items by the internal timer thread.
        /// Default value is 5 minutes.
        /// </summary>
        public static TimeSpan MaintenanceStep
        {
            get => _mMaintenanceStep;
            set
            {
                if (_mMaintenanceStep != value)
                {
                    _mMaintenanceStep = value;
                    StopMaintenance ();
                    StartMaintenance ();
                }
            }
        }
        
        /// <summary>
        /// If the Set method should silently ignore any null value.
        /// Default value is true.
        /// </summary>
        public static bool IgnoreNullValues 
        { 
            get => _mIgnoreNullValues;
            set => _mIgnoreNullValues = value;
        }

        #region *   Events and Event Handlers   *

        public delegate void SimpleMemoryCacheItemExpirationEventHandler (string key, T item);

        public static event SimpleMemoryCacheItemExpirationEventHandler OnExpiration;
        
        private static bool HasEventListeners ()
        {
            if (OnExpiration != null)
            {
            	return OnExpiration.GetInvocationList ().Length != 0;
            }
            return false;
        }

        #endregion

        class CachedItem
        {
            public DateTime Updated;
            public T Data;
        }

        /// <summary>
        /// Gets the current number of item stored in the cache.
        /// </summary>
        public static int Count => MCacheMap.Count;

        /// <summary>
        /// Gets the stored value associated with the specified key.
        /// Return the default value if not found.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Stored value for the key or default value if not found</returns>
        protected static T Get (string key)
        {
            if (key != null)
            {
                if (MCacheMap.TryGetValue (key, out var item))
                    return item.Data;
            }
            return null;
        }

        /// <summary>
        /// Stores or updates the value associated with the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">Stored value.</param>
        private static void Set (string key, T data)
        {
            if (string.IsNullOrEmpty (key))
            {
                if (_mIgnoreNullValues)
                    return;
                throw new System.ArgumentNullException (nameof(key));
            }
            if (data == null)
            {
                // remove data if data is null
                Remove (key);
            }
            else
            {
                // add or update item
                MCacheMap[key] = new CachedItem
                {
                    Updated = DateTime.UtcNow,
                    Data = data
                };
                // check if the timer is active
                StartMaintenance ();
            }
        }

        /// <summary>
        /// Renews the expiration due time for the specified cached item.
        /// </summary>
        /// <param name="key">The cached item key.</param>
        public static void Renew (string key)
        {
            if (string.IsNullOrEmpty (key))
                return;
            if (MCacheMap.TryGetValue (key, out var item))
                item.Updated = DateTime.UtcNow;
        }

        /// <summary>
        /// Removes and returns the value associated with the specified key.
        /// Return the default value if not found.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Stored value for the key or default value if not found</returns>
        protected static T Remove (string key)
        {
            if (!string.IsNullOrEmpty (key))
            {
                if (MCacheMap.TryRemove (key, out var item))                
                    return item.Data;
            }
            return default (T);
        }

        /// <summary>
        /// Remove all cached items with the matching prefix.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="comparison">The comparison method.</param>
        public static void ClearByPrefix (string prefix, StringComparison comparison = StringComparison.Ordinal)
        {
            if (prefix == null)
                return;
            foreach (var i in MCacheMap.Keys)
                if (i.StartsWith (prefix, comparison))
                    MCacheMap.TryRemove (i, out var item);
        }

        /// <summary>
        /// Remove all cached items.
        /// </summary>
        public static void Clear ()
        {
            MCacheMap.Clear ();
        }

        /// <summary>
        /// Gets the stored value associated with the specified key or store a new value
        /// generated by the provided factory function and return it.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="valueFactory">The value factory function.</param>
        /// <returns>Stored value for the key or default value if not found</returns>
        public static T GetOrAdd (string key, Func<string, T> valueFactory)
        {
            if (string.IsNullOrEmpty (key))
                return default (T);
            if (!MCacheMap.TryGetValue (key, out var item))
            {
                if (valueFactory == null)
                    throw new System.ArgumentNullException (nameof(valueFactory));                    
                // create the new value
                var data = valueFactory (key);
                // add or update cache
                Set (key, data);
                // get again to ensure we have the correct item
                return Get (key);
            }
            else
            {
                return item.Data;
            }
        }

        /// <summary>
        /// Gets the stored value associated with the specified key or store a new value
        /// generated by the provided factory function and return it.
        /// If the value factory function is called to create a new item, a lock is aquired to supress
        /// multiple call to the factory function for the specified key (calls to others keys are not blocked). 
        /// If the lock times out (i.e. the factory takes more waitTimeout to create then new instance), the default value for the type is returned.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="valueFactory">The value factory function.</param>
        /// <param name="waitTimeout">The wait timeout to sync.</param>
        /// <returns>Stored value for the key or default value if not found</returns>
        public static T GetOrSyncAdd (string key, Func<string, T> valueFactory, TimeSpan waitTimeout)
        {
            return GetOrSyncAdd (key, valueFactory, (int)waitTimeout.TotalMilliseconds);
        }

        /// <summary>
        /// Gets the stored value associated with the specified key or store a new value
        /// generated by the provided factory function and return it.
        /// If the value factory function is called to create a new item, a lock is aquired to supress
        /// multiple call to the factory function for the specified key (calls to others keys are not blocked).
        /// If the lock times out (i.e. the factory takes more waitTimeout to create then new instance), the default value for the type is returned.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="valueFactory">The value factory function.</param>
        /// <param name="waitTimeoutMilliseconds">The wait timeout milliseconds.</param>
        /// <returns>Stored value for the key or default value if not found</returns>
        public static T GetOrSyncAdd (string key, Func<string, T> valueFactory, int waitTimeoutMilliseconds)
        {
            if (String.IsNullOrEmpty (key))
                return default (T);
            if (!MCacheMap.TryGetValue (key, out var item))
            {
                // create a lock for this key
                using (var padlock = new NamedLock (key))
                {
                    if (padlock.Enter (waitTimeoutMilliseconds))
                    {
                        return GetOrAdd (key, valueFactory);                            
                    }
                    else
                    {
                        return default (T);
                    }
                }
            }
            return item.Data;
        }

        #region *   Cache Maintenance Task  *

        private static System.Threading.Timer m_maintenanceTask = null;
        private static readonly object m_lock = new object ();
        private static int m_executing = 0;

        private static void StartMaintenance ()
        {
            if (m_maintenanceTask == null)
            {
                lock (m_lock)
                {
                    if (m_maintenanceTask == null)
                    {
                        m_maintenanceTask = new System.Threading.Timer (ExecuteMaintenance, null, _mMaintenanceStep, _mMaintenanceStep);
                    }
                }
            }
        }

        private static void StopMaintenance ()
        {
            lock (m_lock)
            {
                if (m_maintenanceTask != null)
                    m_maintenanceTask.Dispose ();
                m_maintenanceTask = null;
            }
        }

        private static void ExecuteMaintenance (object state)
        {
            // check if a step is already executing
            if (System.Threading.Interlocked.CompareExchange (ref m_executing, 1, 0) != 0)
                return;
            // try to fire OnExpiration event
            try
            {
                // stop timed task if queue is empty
                if (MCacheMap.Count == 0)
                {
                    StopMaintenance ();
                    // check again if the queue is empty
                    if (MCacheMap.Count != 0)
                        StartMaintenance ();
                }
                else
                {
                    CachedItem item;
                    DateTime oldThreshold = DateTime.UtcNow - _mTimeout;
                    bool hasEvents = HasEventListeners ();
                    // select elegible records
                    var expiredItems = MCacheMap.Where (i => i.Value.Updated < oldThreshold).Select (i => i.Key);
                    // remove from cache and fire OnExpiration event
                    foreach (var key in expiredItems)
                    {
                        MCacheMap.TryRemove (key, out item);
                        if (hasEvents)
                        {
                            OnExpiration (key, item.Data);
                        }
                    }
                }
            }
            finally
            {
                // release lock
                System.Threading.Interlocked.Exchange (ref m_executing, 0);
            }
        }
        
        #endregion
    }

    /// <summary>
    /// MemoryCache implements some helper methods on top of the MemoryCache<object>.
    /// </summary>
    public class MemoryCache : MemoryCache<object>
    {
        /// <summary>
        /// Gets the stored value associated with the specified key and cast it to desired type.
        /// Returns null if not found or if the type cast failed.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The key value or null if not found or if the type cast failed.</returns>
        public static T GetAs<T> (string key) where T : class
        {
            return Get (key) as T;
        }

        /// <summary>
        /// Removes and returns the value associated with the specified key.
        /// Returns null if not found or if the type cast failed.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The key value or null if not found or if the type cast failed.</returns>
        public static T RemoveAs<T> (string key) where T : class
        {
            return Remove (key) as T;
        }
    }
}