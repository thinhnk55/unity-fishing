using System;
using UnityEngine;
using UnityEngine.UI;

public class Rod : MonoBehaviour
{
    [SerializeField] Image RodImg;
    [SerializeField] Hook hook;
    [SerializeField] LineRenderer line;
    [SerializeField] DiggerState diggerState;
    [SerializeField] float hookInitYPos;

    [Header("Speed Rod")]
    [SerializeField] float pullSpeed;
    [SerializeField] float digSpeed;
    // Start is called before the first frame update
    void Start()
    {
        FishingManager.instance.StartFishing += OnStartFishing;
        hookInitYPos = hook.transform.position.y;
    }

    private void OnDestroy()
    {
        FishingManager.instance.StartFishing -= OnStartFishing;
    }

    private void OnStartFishing()
    {
        diggerState = DiggerState.SWINGING;
    }

    // Update is called once per frame
    void Update()
    {

        if (diggerState == DiggerState.SWINGING)
        {
            if (Input.GetMouseButton(0))
            {
                RodImg.rectTransform.rotation = Quaternion.Euler(0, 0, GetRotation(Camera.main.ScreenToWorldPoint(Input.mousePosition).x));
            }
            else if (Input.GetMouseButtonUp(0)) 
            {
                diggerState = DiggerState.DIGGING;

            }
        }
    }

    private void FixedUpdate()
    {
        if (diggerState == DiggerState.DIGGING)
        {
            hook?.transform.Translate(new Vector2(0, -1 * digSpeed * Time.fixedDeltaTime));
        }
        else if (diggerState == DiggerState.PULLING)
        {
            hook.transform.Translate(new Vector3(0, 1 * pullSpeed * Time.fixedDeltaTime));
            if (hook.transform.position.y >= hookInitYPos)
            {
                hook.CollectObject();
                StartSwinging();
            }
        }
        else if (diggerState == DiggerState.SWINGING)
        {
            //Swing();
        }
    }

    public void StartDigging()
    {
        diggerState = DiggerState.DIGGING;
    }

    public void StartPulling()
    {
        diggerState = DiggerState.PULLING;
    }

    public void StartSwinging()
    {
        diggerState = DiggerState.SWINGING;
        hook.transform.position = new Vector3(hook.transform.position.x, hookInitYPos, hook.transform.position.z);
    }


    private float GetRotation(float posX)
    {
        float aspect = posX / FishingManager.instance.halfWidthOfCamera;
        float rotation = 90f * -aspect;
        return rotation;
    }

}

enum DiggerState
{
    NONE,
    SWINGING,
    DIGGING,
    PULLING,
}
