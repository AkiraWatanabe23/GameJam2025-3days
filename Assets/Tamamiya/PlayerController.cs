using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] public int _hp = 3;
    [SerializeField] private float[] _pos = new float[3] { -5, 0, 5 }; //移動できる場所
    [SerializeField] private int _index = 1;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private bool _isMove = true;
    [SerializeField] private bool _isGround = true;

    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _isMove = true;
        transform.position = new Vector3(_pos[_index], transform.position.y, transform.position.z);
    }

    void Update()
    {
        if(_hp <= 0)
        {
            GameManager.Instance.GameOver();
        }
        if (_isMove)
        {
            Move();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
    }

    /// <summary>プレイヤーの移動</summary>
    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AudioManager.Instance.PlaySE(SEType.HorizontalMove);
            _index = (_index - 1 < 0) ? _index : _index - 1;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            AudioManager.Instance.PlaySE(SEType.HorizontalMove);
            _index = (_index + 1 > _pos.Length - 1) ? _index : _index + 1;
        }
        transform.position = new Vector3(_pos[_index], transform.position.y, transform.position.z);
    }

    /// <summary>プレイヤーのジャンプ</summary>
    public void Jump()
    {
        if(_isGround)
        {
            AudioManager.Instance.PlaySE(SEType.Jump);
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("HIT");
            AudioManager.Instance.PlaySE(SEType.Clash);
            if(_hp > 0)
            {
                _hp--;
            }
            if(_hp == 0)
            {
                _isMove = false;
            }
            Destroy(other.gameObject);
            GameManager.Instance.Monday.ObstacleApproaching();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGround = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGround = false;
        }
    }
}