using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : GrabableObject
{
    public TypeFishing TypeFishing;
    public override void OnCollectObject(Digger collector)
    {
        Destroy(this.gameObject);
    }
}
