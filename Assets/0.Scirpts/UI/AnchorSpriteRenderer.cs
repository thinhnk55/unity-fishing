using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorSpriteRenderer : MonoBehaviour
{
    [SerializeField] Vertical vertical;
    [SerializeField] float VerticalOffset;
    [SerializeField] Horizontal horizontal;
    [SerializeField] float HorizontalOffset; 

    private float halfHeightOfCamera;
    private float halfWidthOfCamera;

    public enum Vertical
    {
        Middle,
        Top,
        Bottom,
    }

    public enum Horizontal
    {
        Center,
        Left,
        Right,
    }
    void Start()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            halfHeightOfCamera = mainCamera.orthographicSize;
            halfWidthOfCamera = halfHeightOfCamera * mainCamera.aspect;
            StartAnchor();  
        }
        else
        {
            Debug.Log("Don't find main camera!");
        }

    }

    private void StartAnchor()
    {
        float AnchorX = 0, AnchorY = 0;
        switch (vertical)
        {
            case Vertical.Top:
                AnchorY = halfHeightOfCamera;
                break; 
            case Vertical.Bottom:
                AnchorY = -halfHeightOfCamera;
                break;
            case Vertical.Middle:
                AnchorY = 0;
                break;

        }

        switch(horizontal)
        {
            case Horizontal.Left:
                AnchorX = -halfWidthOfCamera;
                break;
            case Horizontal.Right:
                AnchorX = halfWidthOfCamera;
                break;
            case Horizontal.Center:
                AnchorX = 0;
                break;
        }

        this.transform.localPosition = new Vector3(AnchorX + HorizontalOffset, AnchorY + VerticalOffset, this.transform.localPosition.z);
    }
}
