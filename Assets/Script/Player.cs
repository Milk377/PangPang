using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
    public PlayerInfo(string level, string maxExp, string missileCount)
    {
        int.TryParse(level, out this.level);
        int.TryParse(maxExp, out this.maxExp);
        int.TryParse(missileCount, out this.missileCount);
    }

    public int level;
    public int maxExp;
    public int missileCount;
}

public class Player : MonoBehaviour
{
    static public Player instance;
    public bool isLive = true;
    public Missile missile;
    public float missileInterval = 0.1f;
    private int curExp;
    public TextAsset csv;
    PlayerTable playerTable;
    List<PlayerInfo> playerInfoList = new List<PlayerInfo>();
    PlayerInfo playerInfo;

    public void AddExp(int addExp)
    {
        curExp += addExp;

        foreach(PlayerInfo playerInfo in playerInfoList)
        {
            // curExp 를 이용해 레벨을 결정한다.
            if (playerInfo.maxExp > curExp)
            { 
                this.playerInfo = playerInfo;
                break; 
            }
        }

        print("AddExp addexp: " + addExp + " /exp: " + curExp + " /level: " + playerInfo.level);
    }

    private void Awake()
    {
        instance = this;
        playerTable = new PlayerTable();
        playerTable.Load(csv);
        foreach (var iter in playerTable.GetRowList())
        {
            PlayerInfo info = new PlayerInfo(iter.Level, iter.MaxExp, iter.MissileCount);
            playerInfoList.Add(info);
        }
    }

    private void Start()
    {
        playerInfo = playerInfoList[0];
        StartCoroutine(GeneratMissileCo());
    }

    IEnumerator GeneratMissileCo()
    {
        while (isLive)
        {
            // 시작 하지 않으면 제외
            if (GameManager.instance.isStart == false)
            {
                yield return null;
                continue;
            }

            // 마우스를 눌렀을때 미사일 발사
            if (Input.GetButton("Shoot"))
            {
                CreateMissile(1);

            }
           
            yield return new WaitForSeconds(missileInterval);
        }
    }

    void CreateMissile(int missileCount)
    {

        switch (missileCount)
        {
            case 1:

                {

                    CreateMissile(new Vector3(transform.position.x - 0.2f, transform.position.y, 0));
                    CreateMissile(new Vector3(transform.position.x + 0.2f, transform.position.y, 0));
                    
                    break;
                }

            case 2:
                {
                    CreateMissile(new Vector3(transform.position.x - 0.3f, transform.position.y, 0));

                    CreateMissile(new Vector3(transform.position.x - 0.2f , transform.position.y, 0));
                    CreateMissile(new Vector3(transform.position.x + 0.2f , transform.position.y, 0));
                    break;


                }
            case 3:
                {
                    CreateMissile(new Vector3(transform.position.x - 0.2f, transform.position.y, 0));
                    CreateMissile(new Vector3(transform.position.x + 0.2f, transform.position.y, 0));

                    CreateMissile(new Vector3(transform.position.x - 0.3f, transform.position.y, 0));
                    CreateMissile(new Vector3(transform.position.x + 0.3f, transform.position.y, 0));

                    break;


                }
            case 4:
                {
                    CreateMissile(new Vector3(transform.position.x - 0.2f, transform.position.y + 0.1f, 0));
                    CreateMissile(new Vector3(transform.position.x + 0.2f, transform.position.y + 0.1f, 0));

                    CreateMissile(new Vector3(transform.position.x - 0.3f, transform.position.y + 0.2f, 0));
                    CreateMissile(new Vector3(transform.position.x + 0.3f, transform.position.y + 0.2f, 0));

                    CreateMissile(new Vector3(transform.position.x - 0.3f, transform.position.y, 0));
                    CreateMissile(new Vector3(transform.position.x + 0.3f, transform.position.y, 0));


                    break;


                }
            case 5:
                {
                    CreateMissile(new Vector3(transform.position.x - 0.2f, transform.position.y, 0));
                    CreateMissile(new Vector3(transform.position.x + 0.2f, transform.position.y, 0));
                    break;


                }

        }

    }

    void CreateMissile(Vector3 startPos)
    {

        Missile newMissile = Instantiate(missile);
        newMissile.transform.position = transform.position;
        newMissile.name = "Missile";

    }


    void Update()
    {
        // 시작 하지 않으면 리턴.
        if (GameManager.instance.isStart == false)
            return;

        if (isLive == false)
            return;

        // 카메라가 바라보는 마우스 X좌표
        Vector3 mousePostion = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        float playerPosX = Camera.main.ScreenToWorldPoint(mousePostion).x;

        // 마우스 좌표 보정
        if (playerPosX > 2.4)
            playerPosX = 2.4f;
        else if (playerPosX < -2.4)
            playerPosX = -2.4f;

        // 이동
        transform.position = new Vector3(playerPosX, transform.position.y, 0);
    }

    public void Die()
    {
        print("Game Over!");
        Player.instance.isLive = false;
        Time.timeScale = 0;

        // 게임 오버 UI 띄우기
        InGameUI.instance.OpenGameOverUI();
    }


}
