using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    public int level;
    Rigidbody2D rigidbody;
    public GameObject explosion;
    public float addForceY = 300;
    public float addForceX = 250;

    private int maxHp = 5;
    private int curHp;
    private TextMeshPro hpText;
    private Animator animator;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hpText = transform.Find("HpText").GetComponent<TextMeshPro>();
       
    }

    public enum Direction
    {
        None,
        Left,
        Right,
        Random
    }

    public void Init(Direction direction)
    {
        if (direction == Direction.Random)
        {
            if (Random.Range(0, 2) == 0)
                direction = Direction.Left;
            else
                direction = Direction.Right;
        }

        if (direction == Direction.Left)
            rigidbody.AddForce(new Vector2(-250, 0));
        else if (direction == Direction.Right)
            rigidbody.AddForce(new Vector2(250, 0));

        // 볼의 레벨에 따라 최대 체력 설정
        maxHp = level * 2 + 1;

        curHp = maxHp;
        hpText.text = curHp.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Ground")
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            rigidbody.AddForce(new Vector2(0, addForceY));
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
        if (collision.name == "Missile")
        {
            // 미사일 파괴
            Destroy(collision.gameObject);

            // 데미지를 깎는다.
            curHp -= 1;
            hpText.text = curHp.ToString();

            // 데미지 애니메이션 실행
            animator.Play("Hit");

            // Hp가 0보다 작으면 파괴
            if (curHp <= 0)
            {
                GameObject newExplosion = Instantiate(explosion);
                newExplosion.transform.position = transform.position;
                Destroy(newExplosion, 0.9f);

                // coin
                Coin newCoin = Instantiate(GameManager.instance.coin);
                newCoin.transform.position = transform.position;

                // 레벨이 0보다 크면 공 2개 생성
                if (level > 0)
                {
                    Ball leftBall = GameManager.instance.GenerateBall(level - 1);
                    leftBall.transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, 0);
                    leftBall.Init(Direction.Left);

                    Ball rightBall = GameManager.instance.GenerateBall(level - 1);
                    rightBall.transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, 0);
                    rightBall.Init(Direction.Right);
                }

                GameManager.instance.RemoveBall(this);
                Destroy(gameObject);
            }
        }
        else if (collision.name == "Player")
        {
            Player.instance.Die();
        }

    }
}
