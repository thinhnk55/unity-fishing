using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public abstract class CollectionViewBase<T> : CacheMonoBehaviour where T : IDataUnit<T>
    {
        protected List<CardBase<T>> cards;

        [SerializeField] protected GameObject cardPrefab;
        [SerializeField] protected Transform contentRoot;
        public abstract void BuildView();
        public virtual void BuildView(List<T> infos)
        {
            contentRoot.DestroyChildrenImmediate();
            cards = new List<CardBase<T>>();
            for (int i = 0; i < infos.Count; i++)
            {
                CardBase<T> card = Instantiate(cardPrefab, contentRoot.transform).GetComponent<CardBase<T>>();
                card.BuildUI(infos[i], this);
                cards.Add(card);
            }
        }
        public virtual void AddCard(T info)
        {
            CardBase<T> card = Instantiate(cardPrefab, contentRoot.transform).GetComponent<CardBase<T>>();
            card.BuildUI(info, this);
            cards.Add(card);
        }
        public virtual void ModifyCardAt(int i, T info)
        {
            cards[i].BuildUI(info);
        }
    }
}