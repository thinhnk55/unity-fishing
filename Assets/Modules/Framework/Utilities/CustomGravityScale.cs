using UnityEngine;

namespace Framework
{
    [RequireComponent(typeof(Rigidbody))]
    public class CustomGravityScale : MonoBehaviour
    {
        [SerializeField] float _gravityScale = 1.0f;

        public float GravityScale { get { return _gravityScale; } set { _gravityScale = value; } }

        Rigidbody _rigidbody;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            if (_rigidbody.isKinematic)
                return;

            Vector3 gravity = Physics.gravity * (_gravityScale - 1f);
            _rigidbody.AddForce(gravity, ForceMode.Acceleration);
        }
    }
}