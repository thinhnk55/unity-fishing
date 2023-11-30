using Sirenix.Utilities;
using System.Collections.Generic;

namespace Framework
{
    public class Consumable : ResourceUnit
    {
        public ObservableDataFull<int> Value { get; set; }

        public override void Add(int value)
        {
            Value.Data += value;
        }
        public override void Minus(int value)
        {
            Value.Data -= value;
        }
        public override bool IsAffordable(int price)
        {
            return Value.Data > price;
        }

        public override bool IsAffordable(IEnumerable<int> costs)
        {
            int sumCost = 0;
            costs.ForEach(x => sumCost += x);
            if (sumCost < Value.Data)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
