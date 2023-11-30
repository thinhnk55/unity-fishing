using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public bool isMovingRight;
    public bool IsMoving;
    [SerializeField] float speed;

    private void OnEnable()
    {
        IsMoving = true;
    }

    private void Update()
    {
        if(!IsMoving) return;

        Moving();
    }

    private void Moving()
    {
        int dir = isMovingRight ? 1 : -1;
        this.transform.position += Vector3.right * dir * speed * Time.deltaTime;
    }
}