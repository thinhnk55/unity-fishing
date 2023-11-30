using Framework.SimpleJSON;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
    public class ResourceInfo : IDataUnit<ResourceInfo>
    {
        public int Value;
        public ResourceType Type;

        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ResourceInfo FromJson(JSONNode json)
        {
            throw new NotImplementedException();
        }
    }
    public class ResourceCard : CardBase<ResourceInfo>
    {
        [SerializeField] protected Image resourceIcon;
        [SerializeField] protected TextMeshProUGUI resourceValue;

        public override void BuildUI(ResourceInfo info)
        {
            base.BuildUI(info);
        }
    }
}
