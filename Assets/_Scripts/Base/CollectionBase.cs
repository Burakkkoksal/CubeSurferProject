using System.Collections.Generic;
using UnityEngine;

namespace Game.Base
{
    public abstract class CollectionBase<T> : MonoBehaviour
    {
        private readonly List<T> _collection = new List<T>();

        public List<T> Collection => _collection;
        
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

        public void Remove(int amount)
        {
            if (amount > _collection.Count)
                amount = _collection.Count;

            for (int i = 0; i < amount; i++)
            {
                if (_collection.Count == 0 || !Remove(_collection[_collection.Count - 1]))
                    break;
            }
        }
    }
}
