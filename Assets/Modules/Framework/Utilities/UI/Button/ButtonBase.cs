
using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
    [RequireComponent(typeof(Button))]

    public class ButtonBase : CacheMonoBehaviour
    {
        Button _button;

        public Button Button
        {
            get
            {
                if (_button == null)
                    _button = GetComponent<Button>();

                return _button;
            }
        }

        protected virtual void Awake()
        {
            Button.onClick.AddListener(Button_OnClicked);
        }

        protected virtual void Button_OnClicked()
        {
        }
        private void OnDestroy()
        {
            Button.onClick.RemoveListener(Button_OnClicked);

        }
    }
}