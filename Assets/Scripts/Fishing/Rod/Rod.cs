using Framework;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Rod : MonoBehaviour
{
    [SerializeField] RectTransform canvas;
    [SerializeField] Image RodImg;
    [SerializeField] Hook hook;
    [SerializeField] LineRenderer line;
    [SerializeField] DiggerState diggerState = DiggerState.NONE;
    public DiggerState DiggerState => diggerState;
    [SerializeField] float hookInitYPos;

    [Header("Speed Rod")]
    [SerializeField] float pullSpeed;
    [SerializeField] float digSpeed;
    // Start is called before the first frame update
    void Start()
    {
        SetHeightSizeOfRod(canvas.rect.width/2);
        hookInitYPos = hook.transform.position.y;
        FishingManager.Instance.OnStartFishing += OnStartFishing;
        FishingManager.Instance.OnStopFishing += OnStopFishing;
    }

    private void OnDestroy()
    {
        try
        {
            FishingManager.Instance.OnStartFishing -= OnStartFishing;
            FishingManager.Instance.OnStopFishing -= OnStopFishing;
        }

        catch (Exception e) { Debug.Log("Error: " + e); }
    }

    private void SetHeightSizeOfRod(float height)
    {
        RodImg.rectTransform.SetHeight(height);
    }

    private void OnStartFishing()
    {
        diggerState = DiggerState.SWINGING;
    }

    private void OnStopFishing()
    {
        diggerState = DiggerState.NONE;
    }

    // Update is called once per frame
    void Update()
    {

        if (diggerState == DiggerState.SWINGING)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if (Input.GetMouseButton(0) && mousePos.y < 0)
            {
                RodImg.rectTransform.rotation = Quaternion.Euler(0, 0, GetRotation(Camera.main.ScreenToWorldPoint(Input.mousePosition).x));
            }
            else if (Input.GetMouseButtonUp(0) && mousePos.y < 0) 
            {
                StartDigging();
            }
        }
    }

    private void FixedUpdate()
    {
        if (diggerState == DiggerState.DIGGING)
        {
            hook.transform.Translate(new Vector2(0, -1 * digSpeed * Time.fixedDeltaTime));
        }
        else if (diggerState == DiggerState.PULLING)
        {
            hook.transform.Translate(new Vector2(0, 1 * pullSpeed * Time.fixedDeltaTime));
            if (hook.transform.position.y >= hookInitYPos)
            {
                StartSwinging();
                hook.CollectObject();
            }
        }
    }

    public void StartDigging()
    {
        hook.canCatch = true;
        diggerState = DiggerState.DIGGING;
    }

    public void StartPulling()
    {
        hook.canCatch = false;
        diggerState = DiggerState.PULLING;
    }

    public void StartSwinging()
    {
        diggerState = DiggerState.SWINGING;
        hook.transform.position = new Vector3(hook.transform.position.x, hookInitYPos, hook.transform.position.z);
    }

    private float GetRotation(float posX)
    {
        float aspect = posX / FishingManager.Instance.halfWidthOfCamera;
        float rotation = 90f * -aspect;
        return rotation;
    }
}

public enum DiggerState
{
    NONE,
    SWINGING,
    DIGGING,
    PULLING,
}
