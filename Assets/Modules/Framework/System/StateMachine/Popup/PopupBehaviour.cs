using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Framework
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PopupBehaviour : CacheMonoBehaviour
    {
        // Global stack of popup
        static List<PopupBehaviour> PopupStack = new ();

        [Header("Config")]
        [SerializeField] protected float _openDuration = 0.4f;
        [SerializeField] protected float _closeDuration = 0.2f;
        [SerializeField] protected bool _allowClose = true;

        [Header("Events")]
        [SerializeField] UnityEvent _eventOnShow;
        [SerializeField] UnityEvent _eventOnClosed;

        // Popup transition
        protected Sequence _transitionSequence;
        protected CanvasGroup _canvasGroup;

        // Popup callback
        public event Callback OnClosed;
        public event Callback OnClose;
        public event Callback OnShow;

        protected bool _inputEnabled = true;

        public float OpenDuration => _openDuration;
        public bool AllowClose { get { return _allowClose; } set { _allowClose = value; } }
        public CanvasGroup CanvasGroup { get { return _canvasGroup; } }

        public static int PopupCount { get { return PopupStack.Count; } }

        #region MonoBehaviour

        protected virtual void Awake()
        {
            // Setup input listen to this popup
            EnableInput();

            // Get canvas group component and disable UI constrol at begin
            _canvasGroup = GetComponent<CanvasGroup>();
            //_canvasGroup.interactable = false;

            // Init transition sequence
            _transitionSequence = DOTween.Sequence()
                .SetAutoKill(false)
                .SetUpdate(true);
            _transitionSequence.OnComplete(ShowCallback);
            _transitionSequence.OnRewind(ClosedCallback);

            // Construct transition sequence from popup transition components or a blank sequence with open duration
            PopupTransition[] popupTransitions = GetComponents<PopupTransition>();
            if (popupTransitions != null)
            {
                for (int i = 0; i < popupTransitions.Length; i++)
                {
                    Tween tween = popupTransitions[i].ConstructTransition(this);
                    if (tween != null)
                        _transitionSequence.Join(tween);
                }
            }
            else
            {
                _transitionSequence.AppendInterval(_openDuration);
            }
        }

        protected virtual void OnDestroy()
        {
            // Disable input listen for this popup
            DisableInput();
            // Kill transition tween
            _transitionSequence?.Kill();
            // Call close popup callback
            OnClosed?.Invoke();
            _eventOnClosed?.Invoke();
        }

        void Update()
        {
            HandleInput();
        }

        #endregion

        #region Public

        public void Close()
        {
            if (!_allowClose)
                return;
           
            HandleClose();
            InternalClose();
        }

        public void ForceClose()
        {
            HandleClose();
            InternalClose();
        }

        public void SetEnabled(bool enabled)
        {
            _canvasGroup.interactable = enabled;
        }

        #endregion

        #region Popup events

        void ShowCallback()
        {
            _canvasGroup.interactable = true;

            HandleShow();
            OnShow?.Invoke();
            _eventOnShow?.Invoke();
        }

        void ClosedCallback()
        {
            HandleClosed();
            Destroy(gameObject);
        }

        #endregion

        #region Virtual functions

        protected virtual void HandleShow()
        {

        }

        protected virtual void HandleClose()
        {

        }

        protected virtual void HandleClosed()
        {

        }

        #endregion

        #region Protected function

        protected void InternalClose()
        {
            // Can't close when it is transiting
            if (_transitionSequence.IsPlaying())
                return;

            // Disable UI control at this moment
            _canvasGroup.interactable = false;

            // On close callback
            OnClose?.Invoke();

            // Set time scale to modify close duration
            _transitionSequence.timeScale = _openDuration / _closeDuration;

            if (_transitionSequence.timeScale > 0f)
            {
                // Play sequence backward
                _transitionSequence.PlayBackwards();
            }
            else
            {
                // Direct rewind sequence
                _transitionSequence.Rewind();
            }
        }

        #endregion

        #region Handle input

        void EnableInput()
        {
            _inputEnabled = true;

            if (PopupStack.Count > 0)
                PopupStack.Last()._inputEnabled = false;

            PopupStack.Add(this);
        }

        void DisableInput()
        {
            _inputEnabled = false;

            PopupStack.Remove(this);

            if (PopupStack.Count > 0)
                PopupStack.Last()._inputEnabled = true;
        }

        void HandleInput()
        {
#if UNITY_ANDROID || UNITY_EDITOR || UNITY_STANDALONE
            if (_inputEnabled && _canvasGroup.interactable)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                    Close();
            }
#endif
        }

        #endregion

        #region Static
        public static void CloseAll()
        {
            for (int i = 0; i < PopupStack.Count; i++)
            {
                Destroy(PopupStack[i].gameObject);
            } 
        }
        #endregion
    }
}