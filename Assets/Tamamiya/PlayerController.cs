using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] public int hp = 3;
    [SerializeField] private float[] pos = new float[3] { -5, 0, 5 }; //移動できる場所
    [SerializeField] private int index = 1;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private bool isMove = true;
    [SerializeField] private bool isGround = true;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isMove = true;
        this.transform.position = new Vector3(pos[index], this.transform.position.y);
    }

    void Update()
    {
        if(hp <= 0)
        {
            GameManager.Instance.GameOver();
        }
        if (isMove)
        {
            Move();
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
    }

    /// <summary>
    /// プレイヤーの移動
    /// </summary>
    private void Move()
    {
        //var horizontal = Input.GetAxisRaw("Horizontal");
        //左入力 → horizontal < 0f
        //右入力 → horizontal > 0f

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AudioManager.Instance.PlaySE(SEType.HorizontalMove);
            index = (index - 1 < 0) ? index : index - 1;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            AudioManager.Instance.PlaySE(SEType.HorizontalMove);
            index = (index + 1 > pos.Length - 1) ? index : index + 1;
        }
        this.transform.position = new Vector3(pos[index], this.transform.position.y);
    }

    /// <summary>
    /// プレイヤーのジャンプ
    /// </summary>
    public void Jump()
    {
        if(isGround)
        {
            AudioManager.Instance.PlaySE(SEType.Jump);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if(hp > 0)
            {
                hp--;
            }
            if(hp == 0)
            {
                isMove = false;
            }
            Destroy(collision.gameObject);
            GameManager.Instance.Monday.ObstacleApproaching();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}