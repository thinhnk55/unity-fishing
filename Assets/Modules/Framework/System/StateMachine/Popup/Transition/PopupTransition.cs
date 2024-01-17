using DG.Tweening;
using UnityEngine;

namespace Framework
{
    public abstract class PopupTransition : MonoBehaviour
    {
        public abstract Tween ConstructTransition(PopupBehaviour popup);
        public abstract Tween ConstructTransition(float animTime);

    }
}