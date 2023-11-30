using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    protected virtual void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}
