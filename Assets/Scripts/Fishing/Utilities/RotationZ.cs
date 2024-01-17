using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationZ : MonoBehaviour
{
    [SerializeField] float rotSpeed;

    // Update is called once per frame
    void Update()
    {
        this.transform.eulerAngles -= rotSpeed * Vector3.forward * Time.deltaTime;
    }
}
