using UnityEngine;

namespace Framework
{
    [System.Serializable]
    public class ObservableData<T>
    {
        [SerializeField] T _data;

        public T Data
        {
            get { return _data; }
            set
            {
                if (!_data.Equals(value))
                {
                    _data = value;
                    OnDataChanged?.Invoke(value);
                }
            }
        }

        public Callback<T> OnDataChanged;

        public ObservableData(T defaultValue)
        {
            _data = defaultValue;
        }
        public ObservableData(T defaultValue, Callback<T> OnDataChanged)
        {
            _data = defaultValue;
            this.OnDataChanged += OnDataChanged;
            OnDataChanged?.Invoke(defaultValue);
        }
    }
}