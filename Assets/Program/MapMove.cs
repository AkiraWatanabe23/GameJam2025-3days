using UnityEngine;

public class MapMove : MonoBehaviour
{
    /// <summary>このオブジェクトの移動方向</summary>
    public Vector3 MoveDirection { get => _moveDirection; set => _moveDirection = value.normalized; }
    /// <summary>このオブジェクトの移動速度</summary>
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public bool IsMovable
    {
        get => _isMovable;
        set 
        {
            if (_isMovable != value) _isMovable = value;
        }
    }

    private Vector3 _moveDirection = new();
    private float _moveSpeed = 0.0f;
    private bool _isMovable = false;

    private Rigidbody _rigidbody = null;

    private void Start()
    {
        if (!TryGetComponent(out _rigidbody))
            _rigidbody = gameObject.AddComponent<Rigidbody>();

        _rigidbody.useGravity = false;
    }

    private void Update()
    {
        if (_isMovable) _rigidbody.velocity = _moveDirection * _moveSpeed;
    }

    public void UpdateMapData(Vector3 direction, float speed, bool movable)
    {
        _moveDirection = direction;
        _moveSpeed = speed;
        _isMovable = movable;
    }
}
