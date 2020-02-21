using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public Ball[] balls;
    public Transform[] generateTrs;
    private List<Ball> ballList = new List<Ball>();
    public Coin coin;
    public bool isStart = false;

    private void Awake()
    {
        instance = this;
    }

    public void Init()
    {
        // 공 생성
        Ball ball = GenerateBall(1);
        ball.transform.position = generateTrs[0].position;
        ball.Init(Ball.Direction.Random);

        // 볼 무한 생성
        StartCoroutine(CreateBallCo());

        isStart = true;
        Time.timeScale = 1;
    }

    public float createInterval = 5;
    IEnumerator CreateBallCo()
    {
        while (Player.instance.isLive)
        {
            yield return new WaitForSeconds(createInterval);
            Ball newBall = GenerateBall(Random.Range(0, balls.Length));

            // 생성 지점을 왼쪽 모서리, 혹은 오른쪽 모서리로 한다.
            if (Random.Range(0, 2) == 0)
            {
                // 왼쪽 모서리에서 생성
                newBall.transform.position = generateTrs[1].position;
            }
            else
            {
                // 오른쪽 모서리에 생성
                newBall.transform.position = generateTrs[2].position;
            }
        }
    }

    public Ball GenerateBall(int index)
    {
        Ball newBall = Instantiate(balls[index]);
        ballList.Add(newBall);
        return newBall;
    }

    public void RemoveBall(Ball ball)
    {
        ballList.Remove(ball);
    }
  
    public void DestroyAllBall()
    {
        foreach (Ball ball in ballList)
        {
            Destroy(ball.gameObject);
        }
        ballList.Clear();
    }

}
