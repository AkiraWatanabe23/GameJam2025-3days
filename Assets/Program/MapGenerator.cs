using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public float MapMoveSpeed { get => _mapMoveSpeed; set => _mapMoveSpeed = value; }

    [SerializeField]
    private int _initialGenerateCount = 4;
    [SerializeField, Tooltip("")]
    private GameObject _initialMap = null;
    [SerializeField, Tooltip("生成するマップを入れる")]
    private GameObject[] _maps = null;
    [SerializeField, Tooltip("マップを動かす方向")]
    private Vector3 _mapMoveDirection = new();
    [SerializeField, Tooltip("マップが動く速さ")]
    private float _mapMoveSpeed = 1.0f;
    [SerializeField]
    private int _mapLifeTime = 10;
    [SerializeField]
    private Vector3 _generatePos = new();
    [SerializeField]
    private float _offset = 0.0f;

    private Queue<MapMove> _generatedMaps = new();
    private MapMove _lastMap = null;
    private MapMove _nextMap = null;
    private float _mapDistance = 0.0f;

    public void Start()
    {
        InitNullCheck();
        InitGenerate();

        _nextMap = RandomGenerate(Vector3.down * 100);
        _nextMap.UpdateMapData(_mapMoveDirection, _mapMoveSpeed, false, _mapLifeTime);
        _nextMap.gameObject.SetActive(false);
        _lastMap = RandomGenerate(_generatePos);
        _lastMap.UpdateMapData(_mapMoveDirection, _mapMoveSpeed, true, _mapLifeTime);
    }

    private void InitGenerate()
    {
        Vector3 generateOffset = new(0.0f, 0.0f, _offset);

        for (int i = _initialGenerateCount; 0 < i; i--)
        {
            var map = Instantiate(_initialMap, _generatePos, _initialMap.transform.rotation).AddComponent<MapMove>();
            map.UpdateMapData(_mapMoveDirection, _mapMoveSpeed, true, _mapLifeTime);
            map.transform.position -= generateOffset * i;
        }
    }

    public void Update()
    {
        _mapDistance = Vector3.Distance(_lastMap.transform.position, _generatePos);

        if (_mapDistance >= _offset)
        {
            _nextMap.gameObject.SetActive(true);
            Vector3 pos = _generatePos;
            pos.z -= _mapDistance - _offset;
            _nextMap.transform.position = pos;
            _nextMap.IsMovable = true;
            _lastMap = _nextMap;

            _nextMap = RandomGenerate(Vector3.down * 100);
            _nextMap.UpdateMapData(_mapMoveDirection, _mapMoveSpeed, false, _mapLifeTime);
            _nextMap.gameObject.SetActive(false);
        }
    }

    private void InitNullCheck()
    {
        if (_maps == null) throw new NullReferenceException();
    }

    private MapMove RandomGenerate(Vector3 pos, Transform parent = null)
    {
        if (_maps == null) throw new NullReferenceException();
        if (_maps.Length == 0) throw new ArgumentOutOfRangeException();

        int randomIndex = Random.Range(0, _maps.Length);
        return Instantiate(
            _maps[randomIndex], pos, _maps[randomIndex].transform.rotation, parent).AddComponent<MapMove>();
    }

    private void ChangeSpeed(float newSpeed)
    {
        { if (!_generatedMaps.TryPeek(out var map)) return; }

        for (int i = 0; i < _generatedMaps.Count; i++)
        {
            var map = _generatedMaps.Dequeue();
            map.MoveSpeed = newSpeed;
            _generatedMaps.Enqueue(map);
        }
    }
}
