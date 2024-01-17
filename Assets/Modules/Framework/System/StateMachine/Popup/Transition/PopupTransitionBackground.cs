using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
    public class PopupTransitionBackground : PopupTransition
    {
        [Header("Behaviour")]
        [SerializeField] bool _closeOnClick = true;
        [SerializeField] Color _color = new Color(0f, 0f, 0f, 0.6f);
        [Range(0f, 1f)]
        [SerializeField] float _fadeDurationRatio = 0.5f;

        PopupBehaviour _popup;
        GameObject _objBG;

        public override Tween ConstructTransition(PopupBehaviour popup)
        {
            // Assign popup reference
            _popup = popup;

            //Listen to popup event
            popup.OnClose += Popup_OnClose;
            popup.OnShow += Popup_OnShow;

            // Spawn background
            SpawnBackground(popup);

            // Return background fade tween
            Image imgBG = _objBG.GetComponent<Image>();
            imgBG.color = _color;
            imgBG.SetAlpha(0f);

            if (_fadeDurationRatio <= 0)
                return null;

            return imgBG.DOFade(_color.a, popup.OpenDuration * _fadeDurationRatio).SetEase(Ease.Linear);
        }

        void SpawnBackground(PopupBehaviour popup)
        {
            _objBG = new GameObject("BG");

            RectTransform bgRect = _objBG.AddComponent<RectTransform>();
            bgRect.SetParent(popup.transform.parent);
            bgRect.SetSiblingIndex(popup.transform.GetSiblingIndex());
            bgRect.anchoredPosition3D = Vector3.zero;
            bgRect.SetWidth(Screen.width * 2f);
            bgRect.SetHeight(Screen.height * 2f);
            bgRect.localScale = Vector3.one;

            _objBG.AddComponent<Image>();

            Button bgButton = _objBG.AddComponent<Button>();
            bgButton.transition = Selectable.Transition.None;
            bgButton.onClick.AddListener(Background_OnClick);
        }

        void Background_OnClick()
        {
            if (_closeOnClick)
                _popup.Close();
        }

        void Popup_OnShow()
        {
            _objBG.GetComponent<Button>().interactable = true;
        }

        void Popup_OnClose()
        {
            _objBG.GetComponent<Button>().interactable = false;
        }

        void OnDestroy()
        {
            Destroy(_objBG.gameObject);
        }

        public override Tween ConstructTransition(float animTime)
        {
            // Return background fade tween
            Image imgBG = _objBG.GetComponent<Image>();
            imgBG.color = _color;
            imgBG.SetAlpha(0f);

            if (_fadeDurationRatio <= 0)
                return null;

            return imgBG.DOFade(_color.a, animTime * _fadeDurationRatio).SetEase(Ease.Linear);
        }
    }
}