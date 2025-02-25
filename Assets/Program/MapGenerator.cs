using System;
using UnityEngine;

//[Serializable]
public class MapGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("生成するマップを入れる")]
    private GameObject[] _maps = null;
    [SerializeField, Tooltip("マップを動かす方向")]
    private Vector3 _mapMoveDirection = new();
    [SerializeField, Tooltip("マップが動く速さ")]
    private float _mapMoveSpeed = 1.0f;

    /// <summary>このクラスのインスタンスを持っているゲームオブジェクト</summary>
    private GameObject _parent = null;

    private float _timer = 0.0f;

    public void Start()
    {
        NullCheck();
    }

    public void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > 3.0f)
        {
            var map = Instantiate(_maps[UnityEngine.Random.Range(0, _maps.Length)]);
            var mapData = map.AddComponent<StageMove>();
            mapData.MoveDirection = _mapMoveDirection;
            mapData.MoveSpeed = _mapMoveSpeed;
            _timer = 0.0f;
        }
    }

    private void NullCheck()
    {
        if (_maps == null) throw new NullReferenceException();
    }
}
