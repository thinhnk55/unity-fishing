using Framework;
using System.Collections.Generic;
using UnityEngine;

namespace IAP
{
    public enum TransactionIAPType
    {

    }
    public class TransactionIAPInfo : IDataUnit<TransactionIAPInfo>
    {
        public int Index { get; set; }
        public TransactionIAPType TransactionType;
        public List<ResourceInfo> Payoffs;
        public void Transact()
        {
            for (int i = 0; i < Payoffs.Count; i++)
            {
                var payoff = Payoffs[i];
                payoff.Type.AddResource(payoff.Value);
            }
        }
    }
    public class TransactionIAPCard : ButtonCardBase<TransactionIAPInfo>
    {
        [SerializeField] protected ResourceCard paymentCard;
        [SerializeField] protected ResourceCard payoffCard;
        [SerializeField] protected ResourceCollectionView payoffView;
        public override void BuildView(TransactionIAPInfo info)
        {
            base.BuildView(info);
            if (payoffView) payoffView.BuildView(info.Payoffs);
            if (payoffCard) payoffCard.BuildView(info.Payoffs.First());
            paymentCard.BuildView(new ResourceInfo() { Index = info.Index, Type = 0, Value = 0 });
        }
        protected override void Card_OnClicked()
        {
            base.Card_OnClicked();
            IAPBase.PurchaseProduct("", (success, product) => { });
        }
    }
}
