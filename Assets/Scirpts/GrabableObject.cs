using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrabableObject : MonoBehaviour, ICatchable
{
    [SerializeField] private float mass;

    private Hook attachedHook;
    private bool attachable = true;

    public float Mass { get => mass; set => mass = value; }
    public bool Attachable { get => attachable; set => attachable = value; }

    public void OnHookInteracted(Hook hook)
    {
        if (Attachable)
        {
            if (attachedHook != null)
            {
                attachedHook.OnStolen();
            }

            attachedHook = hook;
            transform.SetParent(hook.transform);
            transform.localPosition = Vector2.zero;
            transform.localEulerAngles = Vector3.zero;
            Attachable = false; GetComponent<SpriteRenderer>().sortingOrder = 4;
            var moveObj = GetComponent<MovingObject>();
            if (moveObj != null)
            {
                moveObj.Moving = false;
            }

            hook.AttachObject(this);
        }

    }

    public abstract void OnCollectObject(Digger collector);
}
