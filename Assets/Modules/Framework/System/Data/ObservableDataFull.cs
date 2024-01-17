using System;
using UnityEngine;

namespace Framework
{
    [System.Serializable]
    public class ObservableDataFull<T>
    {
        [SerializeField] T _data;

        public T Data
        {
            get { return _data; }
            set
            {
                if (!_data.Equals(value))
                {
                    T oldValue = _data;
                    _data = value;
                    OnDataChanged?.Invoke(oldValue, value);
                }
            }
        }

        public Callback<T, T> OnDataChanged;

        public ObservableDataFull(T defaultValue)
        {
            _data = defaultValue;
            OnDataChanged?.Invoke(defaultValue, defaultValue);
        }

        public ObservableDataFull(T defaultValue, Callback<T, T> OnDataChanged)
        {
            _data = defaultValue;
            this.OnDataChanged += OnDataChanged;
            OnDataChanged?.Invoke(defaultValue, defaultValue);
        }

        public void Invoke(int index)
        {
            throw new NotImplementedException();
        }
    }
}