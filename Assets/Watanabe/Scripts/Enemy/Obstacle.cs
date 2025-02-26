using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Tooltip("上下移動する速度")]
    [Min(1f)]
    [SerializeField]
    private float _moveSpeed = 1f;
    [Tooltip("移動幅")]
    [Range(0.1f, 1f)]
    [SerializeField]
    private float _moveRange = 0.1f;

    [SerializeField]
    private float[] _approachingRanges = default;

    private Vector3 _startPos = Vector3.zero;
    private Vector3 _lowPos = Vector3.zero;
    private Rigidbody2D _rb2d = default;

    private bool _isApproaching = false;
    private int _currentIndex = -1;

    protected bool IsApproaching
    {
        get => _isApproaching;
        private set
        {
            _isApproaching = value;
            if (value) { StopCoroutine(MoveVertical()); }
        }
    }

    private void Start()
    {
        Initialize();
        StartCoroutine(MoveVertical());
    }

    private void Initialize()
    {
        _startPos = transform.position;
        _lowPos = transform.position;

        if (!TryGetComponent(out _rb2d)) { _rb2d = gameObject.AddComponent<Rigidbody2D>(); }

        _rb2d.gravityScale = 0f;
    }

    private IEnumerator MoveVertical()
    {
        var position = transform.position;
        while (Application.isPlaying)
        {
            //上昇
            while (position.y < _lowPos.y + _moveRange)
            {
                position.y += Time.deltaTime * _moveSpeed;

                if (position.y >= _lowPos.y + _moveRange) { position.y = _lowPos.y + _moveRange; }

                transform.position = position;
                yield return null;
            }
            yield return null;

            //下降
            while (_lowPos.y < position.y)
            {
                position.y -= Time.deltaTime * _moveSpeed;

                if (position.y <= _lowPos.y) { position.y = _lowPos.y; }

                transform.position = position;
                yield return null;
            }
            yield return null;
        }
    }

    private IEnumerator Approaching()
    {
        _currentIndex++;
        var position = transform.position;
        while (position.y < _startPos.y + _approachingRanges[_currentIndex])
        {
            position.y += Time.deltaTime * _moveSpeed;

            if (position.y >= _startPos.y + _approachingRanges[_currentIndex]) { position.y = _startPos.y + _approachingRanges[_currentIndex]; }

            transform.position = position;
            yield return null;
        }

        IsApproaching = false;
        if (_currentIndex + 1 != _approachingRanges.Length) { StartCoroutine(MoveVertical()); }

        yield return null;
    }

    /// <summary> プレイヤーが障害物に衝突したときに呼び出される </summary>
    public void ObstacleApproaching()
    {
        IsApproaching = true;
        StartCoroutine(Approaching());
    }
}
