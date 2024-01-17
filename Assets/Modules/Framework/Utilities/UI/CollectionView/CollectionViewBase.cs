using System;
using System.Collections.Generic;
using UnityEngine;
namespace Framework
{
    public interface ICollectionView<T> where T : IDataUnit<T>
    {
        void BuildView();
        void BuildView(List<T> infos);
        void AddCard(T info);
        void ModifyCardAt(int i, T info);
    }
    public abstract class CollectionViewBase<T> : MonoBehaviour, ICollectionView<T> where T : IDataUnit<T>
    {
        protected List<CardBase<T>> cards = new List<CardBase<T>>(); public List<CardBase<T>> Cards { get { return cards; } }

        [SerializeField] protected GameObject cardPrefab;
        [SerializeField] protected Transform contentRoot; public Transform ContentRoot { get { return contentRoot; } }
        public abstract void BuildView();
        public virtual void BuildView(List<T> infos)
        {
            cards.ForEach((card) => card.gameObject.SetActive(false));
            cards = new List<CardBase<T>>();
            for (int i = 0; i < infos.Count; i++)
            {
                infos[i].Index = i;
                AddCard(infos[i]);
            }
        }
        public virtual void AddCard(T info)
        {
            CardBase<T> card = ObjectPoolManager.SpawnObject<CardBase<T>>(cardPrefab, Vector3.zero, contentRoot.transform);
            card.BuildView(info, this);
            cards.Add(card);
        }
        public virtual void ModifyCardAt(int i, T info)
        {
            info.Index = i;
            cards[i].BuildView(info);
        }

        public virtual U FindFirstCard<U>(Predicate<U> predicate) where U : CardBase<T>
        {
            foreach (var item in cards)
            {
                if (predicate.Invoke((U)item))
                {
                    return (U)item;
                }
            }
            return null;
        }
    }
}