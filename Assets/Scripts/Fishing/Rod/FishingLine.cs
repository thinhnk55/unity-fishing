using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLine : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] GameObject head;
    [SerializeField] GameObject tail;

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, head.transform.position);
        lineRenderer.SetPosition(1, tail.transform.position);
    }
}
