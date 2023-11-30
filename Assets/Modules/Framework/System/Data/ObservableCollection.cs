using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    [System.Serializable]
    public class ObservableCollection<T, U> where U : ICollection<T>
    {
        [SerializeField] ICollection<T> _data;

        public ICollection<T> Data
        {
            get { return _data; }
            set { _data = value; OnCollectionChanged?.Invoke(value); }
        }

        public Callback<IEnumerable<T>> OnCollectionChanged;
        public Callback<List<T>> OnDataAdded;
        public Callback<List<T>> OnDataRemoved;

        public ObservableCollection(ICollection<T> defaultValue)
        {
            _data = defaultValue;
        }

        public void Add(T data)
        {
            _data.Add(data);
            OnDataAdded?.Invoke(new List<T>() { data });
        }
        public void AddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                _data.Add(item);
            }
            OnDataAdded?.Invoke((List<T>)collection);
        }
        public void RemoveAll(Predicate<T> predicate)
        {
            List<T> listRemove = new List<T>();
            foreach (var item in _data)
            {
                if (predicate(item) && _data.Remove(item))
                    listRemove.Add(item);
            }
            OnDataRemoved?.Invoke(listRemove);
        }
        public void Remove(T data)
        {
            _data.Remove(data);
            OnDataRemoved?.Invoke(new List<T>() { data });
        }

        public void Clear()
        {
            _data.Clear();
            OnCollectionChanged?.Invoke(_data);
        }
    }
}