using System.Collections.Generic;

namespace Framework
{
    public enum ResourceType
    {
        Money = 0,

        Consumable,
        Gem,
        Gold,

        Nonconsumable,
        Avatar,
        Skin,

        Count
    }
    public abstract class ResourceUnit
    {
        public static Dictionary<ResourceType, ResourceUnit> resources;

        public ResourceUnit()
        {
            resources.Add(Type, this);
        }
        public ResourceType Type { get; set; }
        public abstract void Add(int value);
        public abstract void Minus(int value);
        public abstract bool IsAffordable(int cost);
        public abstract bool IsAffordable(IEnumerable<int> cost);
    }

    interface Equipable<T, U> where T : ICollection<U>
    {
        public T InUseItems { get; set; }
        public abstract void Use(U value);
    }
}
