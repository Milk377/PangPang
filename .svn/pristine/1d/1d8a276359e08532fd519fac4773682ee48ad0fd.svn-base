using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    private Animator animator;
    public float addForceX = 300;
    private bool isGet;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        isGet = false; 
        rigidbody.AddForce(new Vector2(Random.Range(-300, 300), 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Ground")
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            rigidbody.gravityScale = 0;
        }
        else if (collision.name == "Right")
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            rigidbody.AddForce(new Vector2(-addForceX, 0));
        }
        else if (collision.name == "Left")
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            rigidbody.AddForce(new Vector2(addForceX, 0));
        }
        else if (collision.name == "Player")
        {
            if (isGet == false)
            {
                // 플레이어가 동전을 획득하면 Get애니메이션 실행하고 파괴
                rigidbody.velocity = Vector2.zero;
                animator.Play("Get");
                InGameUI.instance.AddCoin();
                Player.instance.AddExp(1);
                isGet = true;
                Destroy(gameObject, 1);
            }
        }

    }
}
