using UnityEngine;

namespace Framework
{
    public abstract class CardBase<T> : CacheMonoBehaviour where T : IDataUnit<T>
    {
        protected T Info;
        [HideInInspector] public CollectionViewBase<T> View;
        public virtual void BuildUI(T info)
        {
            this.Info = info;
        }
        public void BuildUI(T info, CollectionViewBase<T> collectionView)
        {
            this.View = collectionView;
            BuildUI(info);
        }
    }
}
