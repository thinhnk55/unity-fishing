using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public enum TransactionType
    {

    }
    public class TransactionInfo : IDataUnit<TransactionInfo>
    {
        public int Index { get; set; }
        public TransactionType TransactionType;
        public List<ResourceInfo> Payments;
        public List<ResourceInfo> Payoffs;

        public void Transact()
        {
            for (int i = 0; i < Payments.Count; i++)
            {
                var payment = Payments[i];
                payment.Type.ReduceResource(payment.Value);
            }
            for (int i = 0; i < Payoffs.Count; i++)
            {
                var payoff = Payoffs[i];
                payoff.Type.AddResource(payoff.Value);
            }
        }
        public bool IsAffordble()
        {
            for (int i = 0; i < Payments.Count; i++)
            {
                var payment = Payments[i];
                if (!payment.Type.IsAffordable(payment.Value))
                {
                    return false;
                }
            }
            return true;
        }
    }


    public class TransactionCard : ButtonCardBase<TransactionInfo>
    {
        [SerializeField] protected ResourceCard paymentCard;
        [SerializeField] protected ResourceCard payoffCard;
        [SerializeField] protected ResourceCollectionView paymentView;
        [SerializeField] protected ResourceCollectionView payoffView;

        public override void BuildView(TransactionInfo info)
        {
            base.BuildView(info);
            if (paymentView) paymentView.BuildView(info.Payments);
            if (payoffView) payoffView.BuildView(info.Payoffs);
            if (paymentCard) paymentCard.BuildView(info.Payments.First());
            if (payoffCard) payoffCard.BuildView(info.Payoffs.First());
        }

        protected override void Card_OnClicked()
        {
            base.Card_OnClicked();
        }
    }

}
