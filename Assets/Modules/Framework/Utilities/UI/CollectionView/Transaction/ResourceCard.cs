using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
    public class ResourceInfo : IDataUnit<ResourceInfo>
    {
        public int Value;
        public ResourceType Type;

        public int Index { get; set; }
    }
    public class ResourceCard : CardBase<ResourceInfo>
    {
        [SerializeField] protected Image resourceIcon;
        [SerializeField] protected TextMeshProUGUI resourceValue;

        public override void BuildView(ResourceInfo info)
        {
            base.BuildView(info);
        }
    }
}
