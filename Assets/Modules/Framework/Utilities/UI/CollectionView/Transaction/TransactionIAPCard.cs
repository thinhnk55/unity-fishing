using Framework.SimpleJSON;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public enum TransactionIAPType
    {

    }
    public class TransactionIAPInfo : IDataUnit<TransactionIAPInfo>
    {
        public int Id { get; set; }
        public TransactionIAPType TransactionType;
        public List<ResourceInfo> Payoffs;

        public TransactionIAPInfo FromJson(JSONNode data)
        {
            TransactionIAPInfo transactionInfo = new TransactionIAPInfo
            {
            };
            return transactionInfo;
        }
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
        public override void BuildUI(TransactionIAPInfo info)
        {
            base.BuildUI(info);
            if (payoffView) payoffView.BuildView(info.Payoffs);
            if (payoffCard) payoffCard.BuildUI(info.Payoffs.First());
            paymentCard.BuildUI(new ResourceInfo() { Id = info.Id, Type = 0, Value = 0 });
        }
        protected override void Card_OnClicked()
        {
            base.Card_OnClicked();
            IAPBase.PurchaseProduct("", (success, product) => { });
        }
    }
}
