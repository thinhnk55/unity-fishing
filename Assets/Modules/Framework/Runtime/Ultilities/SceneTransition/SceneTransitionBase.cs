using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Framework
{
    public enum ESceneName
    {
        Loading,
        Auth,
        Intro,
        Home,
        GameHome,
        LearnHome,
        Phonics,
        CrossWord,
        FlashCard,
        ScrambleWord,
        FlashphraseScramble,
        Matching,
        MemoryCard,
        SelectLevelMemory,
        EmbededVideo,
        Fishing,
        Coloring,
        Null
    }
    public abstract class SceneTransitionBase<T> : HardSingletonMono<T> where T : CacheMonoBehaviour
    {
        enum State
        {
            // Wait for load scene command
            Idle,
            // Playing fade in animation
            FadeIn,
            // End of fade in animation, load new scene
            Loading,
            // Playing fade out animation
            FadeOut,
        }
        // Index of scene will be loaded
        ESceneName eSceneValue = ESceneName.Null; public ESceneName ESceneValue { get=> eSceneValue; }
        ESceneName previousSceneName = ESceneName.Null;
        // Scene async
        AsyncOperation _sceneAsync;
        // State machine
        StateMachine<State> _stateMachine;
        // Tween
        Tween _tween;
        protected Callback _fadein;
        protected Callback _fadeout;
        public Callback OnLoaded;
        public IEnumerator LoadingTask;

        #region MonoBehaviour

        void Start()
        {
            CacheGameObject.SetActive(false);
            _stateMachine = new StateMachine<State>();
            _stateMachine.AddState(State.FadeIn, State_OnFadeInStart);
            _stateMachine.AddState(State.Loading, null, State_OnLoadingUpdate);
            _stateMachine.AddState(State.FadeOut, State_OnFadeOutStart);
            _stateMachine.AddState(State.Idle, EnterScene, null, ExitScene);
            _stateMachine.CurrentState = State.Idle;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _tween?.Kill();
        }

        protected virtual void Update()
        {
            _stateMachine.Update();
        }

        #endregion

        #region States
        void State_OnFadeInStart()
        {
            // Load scene async
            _sceneAsync = SceneManager.LoadSceneAsync(eSceneValue.ToString());
            _sceneAsync.allowSceneActivation = false;

            //Play fade in tween
            FadeIn();

            //Wait until animation is end
            _tween?.Kill();
            _tween = DOVirtual.DelayedCall(SceneTransitionConfigSO.FadeInDuration + SceneTransitionConfigSO.LoadDuration, () =>
            {
                if (CacheGameObject.activeSelf)
                    _stateMachine.CurrentState = State.Loading;
                else
                    _stateMachine.CurrentState = State.FadeOut;
                _sceneAsync.allowSceneActivation = true;
            }, true);
        }

        void State_OnLoadingUpdate()
        {
            if (_sceneAsync.isDone)
            {
                _stateMachine.CurrentState = State.FadeOut;
            }
        }
        void State_OnFadeOutStart()
        {
            if (gameObject.activeInHierarchy)
                StartCoroutine(State_OnFadeOutStart_Task());
            else
            {
                //Play fade out tween
                FadeOut();
                //Wait until animation is end
                _tween?.Kill();
                _tween = DOVirtual.DelayedCall(SceneTransitionConfigSO.FadeOutDuration, () =>
                {
                    _stateMachine.CurrentState = State.Idle;
                    CacheGameObject.SetActive(false);
                    OnLoaded?.Invoke();
                }, true);
            }
        }
        IEnumerator State_OnFadeOutStart_Task()
        {
            yield return LoadingTask;
            LoadingTask = null;
            //Play fade out tween
            FadeOut();
            //Wait until animation is end
            _tween?.Kill();
            _tween = DOVirtual.DelayedCall(SceneTransitionConfigSO.FadeOutDuration, () =>
            {
                _stateMachine.CurrentState = State.Idle;
                CacheGameObject.SetActive(false);
                OnLoaded?.Invoke();
            }, true);
        }

        protected abstract void EnterScene();
        protected abstract void ExitScene();
        protected abstract void FadeIn();
        protected abstract void FadeOut();
        #endregion

        #region Public
        public void Load(ESceneName eSceneValue, bool showLoadingScene)
        {
            if (eSceneValue == ESceneName.Null) return;
            if (_stateMachine.CurrentState != State.Idle)
            {
                PDebug.Log("[{0}] A scene is loading, can't execute load scene command!", typeof(SceneTransitionBase<T>));
                return;
            }

            if (showLoadingScene)
                CacheGameObject.SetActive(true);
            if(this.eSceneValue != ESceneName.Loading) previousSceneName = this.eSceneValue;
            this.eSceneValue = eSceneValue;
            _stateMachine.CurrentState = State.FadeIn;
        }
        public void Reload(bool showLoadingScene)
        {
            Load(eSceneValue, showLoadingScene);
        }

        public void LoadPreviousScene(bool showLoadingScene)
        {
            Load(previousSceneName, showLoadingScene);
        }

        public void Construct()
        {
            // Construct state machine
            //_fadein += ()=> gameObject.SetChildrenRecursively<Image>((img) => { img.DOFade(1, SceneTransitionConfigSO.FadeInDuration); });
            //_fadeout += ()=> GetComponent<Image>().DOFade(0,SceneTransitionConfigSO.FadeOutDuration);
        }
        #endregion
    }
    public static class SceneTransitionHelper
    {
        public static SceneTransitionBase<SceneTransition> _instance;
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void LazyInit()
        {
            if (_instance == null)
            {
                _instance = SceneTransitionConfigSO.ObjSceneTransition.Create().GetComponent<SceneTransition>();
                _instance.Construct();
                GameObject.DontDestroyOnLoad(_instance.CacheGameObject);
            }
        }
        public static void Load(ESceneName eSceneValue, bool showLoadingScene)
        {
            LazyInit();
            _instance.Load(eSceneValue, showLoadingScene);
        }
        public static void Reload(bool showLoadingScene)
        {
            LazyInit();
            _instance.Reload(showLoadingScene);
        }

        public static void LoadPreviousScene(bool showLoadingScene)
        {
            LazyInit();
            _instance.LoadPreviousScene(showLoadingScene);
        }
    }
}
