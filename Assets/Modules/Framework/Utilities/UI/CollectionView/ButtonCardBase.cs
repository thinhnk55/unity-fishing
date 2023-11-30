using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
    [RequireComponent(typeof(Button))]
    public class ButtonCardBase<T> : CardBase<T> where T : IDataUnit<T>
    {
        [SerializeField] protected Button button;

        private void Awake()
        {
            button.onClick.AddListener(Card_OnClicked);

        }
        protected virtual void Card_OnClicked()
        {

        }
        private void OnDestroy()
        {
            button.onClick.RemoveListener(Card_OnClicked);

        }
    }
}
