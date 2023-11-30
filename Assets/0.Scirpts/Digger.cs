using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digger : MonoBehaviour
{
    private const int MAXHOOKANGLE = 70;

    [SerializeField] LineRenderer line;
    [SerializeField] Hook hook;
    [SerializeField] float digSpeed;
    [SerializeField] float pullSpeed;
    [SerializeField] float swingSpeed;
    //[SerializeField] Dynamite dynamitePrefab;
    [SerializeField] Transform aimPoint;
    [SerializeField] LayerMask aimLayerMask;

    [SerializeField] DiggerState state = DiggerState.SWINGING;
    bool swingingLeft = true;
    private float hookInitialY;
    private float defaultMaxPullSpeed;
    private int dynamitesLeft;
    //private Dynamite throwingDynamite;
    private bool isSuperStrong = false;

    public float PullSpeed { get => pullSpeed; set => pullSpeed = value; }

    private void Start()
    {
        hookInitialY = hook.transform.localPosition.y;
        defaultMaxPullSpeed = PullSpeed;
        dynamitesLeft = 2;
    }

    private void Update()
    {
        if (state == DiggerState.SWINGING)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                StartDigging();
            }
        }
        else if (state == DiggerState.PULLING)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                StartPulling();
            }
        }
    }

    private void FixedUpdate()
    {
        if (state == DiggerState.DIGGING)
        {
            hook.transform.Translate(Vector2.down * digSpeed * Time.fixedDeltaTime);
        }
        else if (state == DiggerState.PULLING)
        {
            float pullForce = isSuperStrong ? defaultMaxPullSpeed : PullSpeed;
            hook.transform.Translate(Vector2.up * pullForce * Time.fixedDeltaTime);
            if (hook.transform.localPosition.y >= hookInitialY)
            {
                hook.CollectObject();
                StartSwinging();
            }
        }
        else if (state == DiggerState.SWINGING)
        {
            Swing();
        }

        //line.SetPosition(0, Vector2.zero);
        line.SetPosition(1, hook.transform.localPosition);
    }

    public void StartDigging()
    {
        state = DiggerState.DIGGING;
    }

    public void StartPulling()
    {
        state = DiggerState.PULLING;
    }

    public void StartSwinging()
    {
        state = DiggerState.SWINGING;
        PullSpeed = defaultMaxPullSpeed;
        hook.transform.localPosition = new Vector2(0, hookInitialY);
    }

    public void SwitchSwingDirection()
    {
        swingingLeft = swingingLeft ? false : true;
    }

    public void Swing()
    {
        float rotateDegree = swingSpeed * Time.fixedDeltaTime;
        if (swingingLeft)
            rotateDegree *= -1;

        transform.Rotate(Vector3.forward, rotateDegree);
        //transform.Rotate(0, 0, rotateDegree);

        if (transform.localEulerAngles.z > MAXHOOKANGLE && transform.localEulerAngles.z < 360 - MAXHOOKANGLE)
            SwitchSwingDirection();
    }

    public void SetPullSpeedBasedOnObjectMass(float objectMass)
    {
        PullSpeed = defaultMaxPullSpeed / objectMass;
    }

    enum DiggerState
    {
        SWINGING,
        DIGGING,
        PULLING,
    }
}
