using System.Collections.Generic;
using UnityEngine;

namespace DataStructures.RuntimeSet
{
    public abstract class RuntimeSet_SO<T> : ScriptableObject
    {
        [ShowInInspector, ReadOnly] private HashSet<T> items = new HashSet<T>();
        private ImmutableHashSet<T> immutableItems;

        public ImmutableHashSet<T> GetItems()
        {
            if (immutableItems == null)
            {
                immutableItems = new ImmutableHashSet<T>(items);
            }

            return immutableItems;
        }

        public void Add(T item)
        {
            items.Add(item);
        }

        public void Remove(T item)
        {
            items.Remove(item);
        }
    }
}
