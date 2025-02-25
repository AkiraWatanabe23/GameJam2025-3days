using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float[] pos = new float[3] { -5, 0, 5 }; //移動できる場所
    [SerializeField] private int index = 1;
    [SerializeField] private bool isMove = true;
    void Start()
    {
        isMove = true;
        this.transform.position = new Vector3(pos[index], this.transform.position.y);
    }

    void Update()
    {
        if (isMove)
        {
            Move();
        }
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

        this.transform.position = new Vector3(pos[index], this.transform.position.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isMove = false;
        }

        if (collision.gameObject.CompareTag("Monday"))
        {
            GameManager.Instance.GameOver();
        }
    }
}