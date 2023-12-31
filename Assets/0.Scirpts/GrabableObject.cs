using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrabableObject : MonoBehaviour, IGrabable
{
    protected Hook attachedHook;

    public bool attachable;

    protected virtual void OnEnable()
    {
        attachable = true;
    }

    public virtual void OnHookInteracted(Hook hook)
    {
        if (attachable)
        {
            attachedHook = hook;
            transform.SetParent(hook.transform);
            transform.localPosition = Vector2.zero;
            transform.localEulerAngles = Vector3.zero;
            var moveObj = GetComponent<MovingObject>();
            var anim = GetComponent<Anim>();

            if (moveObj != null)
            {
                moveObj.IsMoving = false;
            }

            if (anim != null)
            {
                anim.StopAnim();
            }

            hook.AttachObject(this);
        }

    }

    public abstract void OnCollectObject(Hook collector);
}
