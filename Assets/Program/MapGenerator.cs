using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//[Serializable]
public class MapGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("生成するマップを入れる")]
    private GameObject[] _maps = null;
    [SerializeField, Tooltip("マップを動かす方向")]
    private Vector3 _mapMoveDirection = new();
    [SerializeField, Tooltip("マップが動く速さ")]
    private float _mapMoveSpeed = 1.0f;

    private Queue<GameObject> _generatedMaps = new();
    private MapMove _lastMap = null;
    private MapMove _nextMap = null;

    public void Start()
    {
        InitNullCheck();
        _nextMap = RandomGenerate(Vector3.down * 100.0f, Quaternion.identity);
        _nextMap.gameObject.SetActive(false);
        _lastMap = RandomGenerate(transform.position, Quaternion.identity, true);
    }

    public void Update()
    {
        float distance = Vector3.Distance(_lastMap.transform.position, transform.position);

        if (distance >= _nextMap.transform.localScale.z * 10.0f)
        {
            _nextMap.gameObject.SetActive(true);
            _nextMap.transform.position = transform.position;
            _nextMap.IsMovable = true;
            _lastMap = _nextMap;

            _nextMap = RandomGenerate(Vector3.down * 100.0f, Quaternion.identity);
            _nextMap.gameObject.SetActive(false);
        }
    }

    private void InitNullCheck()
    {
        if (_maps == null) throw new NullReferenceException();
    }

    private MapMove RandomGenerate(Vector3 pos, Quaternion rot, bool isMovable = false, Transform parent = null)
    {
        if (_maps == null) throw new NullReferenceException();
        if (_maps.Length == 0) throw new ArgumentOutOfRangeException();

        int randomIndex = Random.Range(0, _maps.Length);
        MapMove mapData = Instantiate(_maps[randomIndex], pos, rot, parent).AddComponent<MapMove>();
        mapData.MoveDirection = _mapMoveDirection;
        mapData.MoveSpeed = _mapMoveSpeed;
        mapData.IsMovable = isMovable;
        return mapData;
    }
}
