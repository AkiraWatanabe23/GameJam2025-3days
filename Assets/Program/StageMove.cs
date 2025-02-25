using UnityEngine;

public class StageMove : MonoBehaviour
{
    public Vector3 MoveDirection { get => _moveDirection; set => _moveDirection = value.normalized; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    [SerializeField]
    private Vector3 _moveDirection = new();
    [SerializeField]
    private float _moveSpeed = 0.0f;

    private Rigidbody _rigidbody = null;

    private void Start()
    {
        if (TryGetComponent(out _rigidbody)) { }
        else _rigidbody = gameObject.AddComponent<Rigidbody>();

        _rigidbody.useGravity = false;
        _rigidbody.velocity = _moveDirection * _moveSpeed;
    }
}
