using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float[] pos = new float[3] { -5, 0, 5 }; //移動できる場所
    [SerializeField] private int index = 1;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _camera;
    [SerializeField] private Vector3 _offset;

    void Start()
    {
        this.transform.position = new Vector3(pos[index], this.transform.position.y);
        _offset = _camera.position - this.transform.position;
    }

    void Update()
    {
        Move();
        _camera.transform.position = new Vector3(
            0, this.transform.position.y + _offset.y, this.transform.position.z + _offset.z);
    }

    /// <summary>
    /// プレイヤーの移動
    /// </summary>
    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            index = (index - 1 < 0) ? index : index - 1;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            index = (index + 1 > pos.Length - 1) ? index : index + 1;
        }

        if (index > pos.Length)
        {
            index = pos.Length - 1;
        }
        if (index < 0)
        {
            index = 0;
        }

        this.transform.position = new Vector3(pos[index], this.transform.position.y,
            this.transform.position.z + _speed * Time.deltaTime);
        GameManager.Instance.AddDistance(_speed * Time.deltaTime);
    }
}