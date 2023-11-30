using DG.Tweening;
using UnityEngine;

namespace Framework
{
    public class SpriteRendererAnimated : CacheMonoBehaviour
    {
        [Header("Config")]
        [SerializeField] Sprite[] _sprFrames;
        [Min(1)]
        [SerializeField] int _fps = 30;
        [SerializeField] bool _autoPlay = true;

        SpriteRenderer _spriteRenderer;
        Sequence _sequence;

        public Sequence Sequence
        {
            get
            {
                if (_sequence == null)
                    InitSequence();
                return _sequence;
            }
        }

        #region MonoBehaviour

        void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            if (_autoPlay)
                Sequence.Play();
        }

        void OnDestroy()
        {
            _sequence?.Kill();
        }

        void OnEnable()
        {
            _sequence?.Play();
        }

        void OnDisable()
        {
            _sequence?.Pause();
        }

        #endregion

        #region Public

        public void Destroy()
        {
            Object.Destroy(gameObject);
        }

        #endregion

        void InitSequence()
        {
            if (_sequence != null)
                return;

            float delayBetween = 1f / _fps;

            _sequence = DOTween.Sequence();

            for (int i = 0; i < _sprFrames.Length; i++)
            {
                int frameIndex = i;

                _sequence.AppendCallback(() => { SetFrame(frameIndex); });
                _sequence.AppendInterval(delayBetween);
            }

            _sequence.SetLoops(-1, LoopType.Restart);
            _sequence.SetAutoKill(false);
        }

        void SetFrame(int frameIndex)
        {
            _spriteRenderer.sprite = _sprFrames[frameIndex];
        }
    }
}