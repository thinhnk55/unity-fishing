using UnityEngine;

namespace Framework
{
    public class PopupRootSetter : MonoBehaviour
    {
        void Awake()
        {
            PopupHelper.PopupRoot = transform;
        }
    }
}