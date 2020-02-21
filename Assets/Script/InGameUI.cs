using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUI : MonoBehaviour
{
    static public InGameUI instance;

    public TextMeshProUGUI coinText;
    public Animation coinAnimation;
    private int curCoin;
    private GameObject startUI;
    private GameObject gameOverUI;

    private void Awake()
    {
        instance = this;
        startUI = transform.Find("StartUI").gameObject;
        gameOverUI = transform.Find("GameOverUI").gameObject;
    }

    private void Start()
    {
        coinText.text = "0";

        startUI.SetActive(true);
        gameOverUI.SetActive(false);
    }

    public void AddCoin()
    {
        curCoin++;
        coinText.text = (curCoin * 100).ToString();
        coinAnimation.Play("Get");
    }

    public void OnClickStartBtn()
    {
        print("OnClickStartBtn");

        // StartUI 없앰
        startUI.SetActive(false);

        // 게임시작
        GameManager.instance.Init();
    }

    public void OnClickRestartBtn()
    {
        print("OnClickRestartBtn");

        gameOverUI.SetActive(false);

        // 모든 공 없애기
        GameManager.instance.DestroyAllBall();

        // 플레이어 살아나게하기
        Player.instance.isLive = true;

        // 공 다시 생성시키기
        GameManager.instance.Init();
    }

    public void OpenGameOverUI()
    {
        print("OpenGameOverUI");
        gameOverUI.SetActive(true);
    }

}
