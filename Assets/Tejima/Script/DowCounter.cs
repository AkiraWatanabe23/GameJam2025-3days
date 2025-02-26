using UnityEngine;
using UnityEngine.UI;

public enum Week
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday,
}

public class DowCounter : MonoBehaviour
{
    [SerializeField]
    private Animator _animator = default;
    [SerializeField]
    private Sprite[] _sprites = new Sprite[7];

    [Header("Debug")]
    [SerializeField]
    private float _checkValue = 48f;

    private Week _week = Week.Monday;
    private Image _image = default;
    private bool _isStop = false;
    private int _spriteNum = -1;
    private float _time = 0.0f;
    private float _changeTime = 0.0f;

    protected int Index
    {
        get => _spriteNum;
        private set
        {
            _spriteNum = value;
#if UNITY_EDITOR
            Debug.Log($"{_week}“Ë“üI");
#endif
            _image.sprite = _sprites[value];
        }
    }

    private void Start()
    {
        _image = GetComponent<Image>();
        _week = Week.Monday;

        Index++;
    }

    private void Update()
    {
        //_sprites.GetValue(_spriteNum);
        //_image.sprite = _sprites[_spriteNum];
        if (_isStop) { _changeTime += Time.deltaTime; }
        else { _time += Time.deltaTime; }

        if (_time >= _checkValue)
        {
            NextDay();
            _time = 0;
        }

        if (_changeTime >= 1)
        {
            _animator.SetBool("Change", false);
            _isStop = false;
            _changeTime = 0;
        }
    }

    private void NextDay()
    {
        if (_week == Week.Sunday)
        {
            _week = Week.Monday;
            Index = 0;
        }
        else
        {
            _week++;
            Index++;
        }
        _animator.SetBool("Change", true);
        _isStop = true;
    }

    public Week GetResultWeek() => _week;
}
