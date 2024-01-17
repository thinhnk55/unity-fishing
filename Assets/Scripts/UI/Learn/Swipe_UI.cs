using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Swipe_UI : MonoBehaviour
{
    [SerializeField]
    private Scrollbar scrollBar;
    // [SerializeField] private Transform[] circleContents;
    [SerializeField]
    private float swipeTime = 0.2f;
    [SerializeField]
    private float swipeDistance = 0.1f;
    [SerializeField] HorizontalLayoutGroup horizontalLayoutGroup;
    [SerializeField] Vector3 upScale;
    [SerializeField] Vector3 downScale;

    float[] scrollPageValues;
    LearnModeCard[] modeCards;
    float valueDistance = 0;
    int currentPage = 0;
    int maxPage = 0;
    float startTouchX;
    float endTouchX;
    bool isSwipeMode = false;
    //float circleContentScale = 1.6f;

    private void Awake()
    {
        scrollPageValues = new float[transform.childCount];
        modeCards = new LearnModeCard[transform.childCount];

        valueDistance = 1f / (scrollPageValues.Length - 1f);

        for (int i = 0; i < scrollPageValues.Length; ++i)
        {
            scrollPageValues[i] = valueDistance * i;
            modeCards[i] = transform.GetChild(i).GetComponent<LearnModeCard>(); 
        }
        maxPage = transform.childCount;
    }

    private void Start()
    {
        horizontalLayoutGroup.padding.bottom = (int)(1920f/Screen.height * horizontalLayoutGroup.padding.bottom);
        SetScrollBarValue(0);
        UpdateLearnCardMode(0,1);
    }

    #region private 
    private void SetScrollBarValue(int index)
    {
        currentPage = index;
        scrollBar.value = scrollPageValues[index];
    }

    private void Update()
    {
        UpdateInput();
        //UpdateCircleContent();
    }

    private void UpdateInput()
    {
        if (isSwipeMode == true) return;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            startTouchX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endTouchX = Input.mousePosition.x;
            UpdateSwipe();
        }
#endif

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchX = touch.position.x;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchX = touch.position.x;
                UpdateSwipe();
            }
        }
#endif
    }
    private void UpdateSwipe()
    {
        if ((Mathf.Abs(startTouchX - endTouchX) / Screen.width) < swipeDistance)
        {
            StartCoroutine(OnSwipeOneStep(currentPage));
            return;
        }
        bool isLeft = startTouchX < endTouchX ? true : false;

        if (isLeft == true)
        {
            if (currentPage == 0) return;
            currentPage--;
            UpdateLearnCardMode(currentPage, currentPage + 1);
        }
        else
        {
            if (currentPage == maxPage - 1) return;
            currentPage++;
            UpdateLearnCardMode(currentPage, currentPage - 1);
        }
        StartCoroutine(OnSwipeOneStep(currentPage));
    }
    private IEnumerator OnSwipeOneStep(int index)
    {
        float start = scrollBar.value;
        float current = 0;
        float percent = 0;
        isSwipeMode = true;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / swipeTime;

            scrollBar.value = Mathf.Lerp(start, scrollPageValues[index], percent);
            yield return null;
        }
        isSwipeMode = false;
    }

    //private void UpdateCircleContent()
    //{
    //    if (circleContents.Length == 0) return;
    //    for (int i = 0; i < scrollPageValues.Length; ++i)
    //    {
    //        circleContents[i].localScale = Vector2.one;
    //        circleContents[i].GetComponent<Image>().color = Color.white;
    //        if (scrollBar.value < scrollPageValues[i] + (valueDistance / 2) && scrollBar.value > scrollPageValues[i] - (valueDistance / 2))
    //        {
    //            circleContents[i].localScale = Vector2.one * circleContentScale;
    //            circleContents[i].GetComponent<Image>().color = Color.black;
    //        }
    //    }
    //}

    private void UpdateLearnCardMode(int curPage, int prePage)
    {
        transform.GetChild(curPage).DOComplete();
        transform.GetChild(curPage).DOScale(upScale, swipeTime).OnComplete(() =>
        {
            modeCards[curPage].OnCardSelected(true);
        });

        transform.GetChild(prePage).DOComplete();
        modeCards[prePage].OnCardSelected(false);
        transform.GetChild(prePage).DOScale(downScale, swipeTime);
    }
    #endregion
}


