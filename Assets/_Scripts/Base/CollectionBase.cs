using System.Collections.Generic;
using UnityEngine;

namespace Game.Base
{
    public abstract class CollectionBase<T> : MonoBehaviour
    {
        private readonly List<T> _collection = new List<T>();

        public IList<T> Collection => _collection;
        public int Count => _collection.Count;
        
        public virtual bool Add(T t)
        {
            if (t == null || _collection.Contains(t)) return false;
            _collection.Add(t);
            return true;
        }

        public virtual bool Remove(T t)
        {
            if (t == null || !_collection.Contains(t)) return false;
            _collection.Remove(t);
            return true;
        }
    }
}
