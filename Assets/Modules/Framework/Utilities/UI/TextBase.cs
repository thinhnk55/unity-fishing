using TMPro;
using UnityEngine;

namespace Framework
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextBase : MonoBehaviour
    {
        protected TextMeshProUGUI _text;

        public TextMeshProUGUI Text { get { return _text; } }

        protected virtual void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }
    }
}
