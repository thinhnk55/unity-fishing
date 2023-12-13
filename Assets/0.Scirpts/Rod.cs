using UnityEngine;
using UnityEngine.UI;

public class Rod : MonoBehaviour
{
    [SerializeField] Vector2 lineSizeInit;
    [SerializeField] Image RodImg;
    [SerializeField] Hook hook;
    [SerializeField] LineRenderer line;
    [SerializeField] DiggerState diggerState = DiggerState.SWINGING;
    // Start is called before the first frame update
    void Start()
    {
         //RodImg.rectTransform.sizeDelta = new Vector2(lineSizeInit.x, lineSizeInit.y);
    }

    // Update is called once per frame
    void Update()
    {

        if (diggerState == DiggerState.SWINGING)
        {
            if (Input.GetMouseButton(0))
            {
                Debug.Log("Swing");
                RodImg.rectTransform.rotation = Quaternion.Euler(0, 0, GetRotation(Camera.main.ScreenToWorldPoint(Input.mousePosition).x));
            }
            else if (Input.GetMouseButtonUp(0)) 
            {
                Debug.Log("Dig");
               // diggerState = DiggerState.DIGGING;
            }
        }
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
