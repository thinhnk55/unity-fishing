using System;
using System.Collections.Generic;

namespace Framework
{
    public class DataResource : PDataBlock<DataResource>
    {
        public static Dictionary<ResourceType, ResourceUnit> resources;

        protected override void Init()
        {
            base.Init();
            foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
            {
                if (type < ResourceType.Nonconsumable && type > ResourceType.Consumable)
                {
                    resources.Add(type, new Consumable());
                }
                else if (type < ResourceType.Count && type > ResourceType.Nonconsumable)
                {
                    resources.Add(type, new Nonconsumable());
                }
            }
        }
    }
}
