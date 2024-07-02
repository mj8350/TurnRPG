using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public int id;
    public string CharName; // 캐릭터 이름
    public string CharJob; // 캐릭터 직업
    public int Accuracy; // 명중률
    public int P_Defense; // 물리방어력
    public int M_Defense; // 마법방어력
    public int Strength; // 공격력
    public int Magic; // 마법력
    public int Critical; // 치명타 
    public int Speed; // 속도
    public int Helth; // 체력
    public string SpecialAB; // 고유 능력
    public string Skill01;
    public string Skill02;

    public int MaxHP;
    public int CurHP;

    //public int Level;
    //public int EXP;

    public bool onPlayerDead;
}

[System.Serializable]
public class Monsters
{
    public string MonsterName; // 캐릭터 이름
    public int Accuracy; // 명중률
    public int P_Defense; // 물리방어력
    public int M_Defense; // 마법방어력
    public int Strength; // 공격력
    public int Magic; // 마법력
    public int Critical; // 치명타 
    public int Speed; // 속도
    public int Helth; // 체력

    public int MaxHP;
    public int CurHP;

    public int Give_EXP;

    public bool onMonsterDead;
}

public enum SceneState
{
    TitleScene,
    SelectScene,
    MoveScene,
    BattleScene
}

public class GameManager : Singleton<GameManager>
{
    public Player[] player = new Player[3];
    public PHM_CharStat charStat;
    //public Transform[] transforms;
    private SelectPlayerPos PlayerPos;
    public SceneState sceneState;
    //public Transform movePlayer;//선택->이동시 처음 지정, 이동>배틀시마다 지정

    private Dictionary<GameObject, int> playerObjectToId = new Dictionary<GameObject, int>();

    public GameObject[] Prefeb;

    public int[] MaxEXP = { 10, 20, 30, 40, 50, 60, 70, 80, 90 };

    public int PlayerLevel;
    public int PlayerExp;

    public int MonsterLevel;
    public int ClearEXP;

    public int Dice;
    public int movePoint=0;
    public Vector3 PlayerMovePos;
    public Vector3 PlayerMovePosClear;
    public bool[] MonsterLife;

    public int MonsterStage;
    public int MonsterValue;
    public int WhatMonster;


    private void Awake()
    {
        base.Awake();

        //for (int i = 0; i < 3; i++)
        //{
        //    int ran = Random.Range(0, Prefeb.Length);
        //    CreateUserData(i, ran);
        //}

        MonsterLevel = 1;
        PlayerLevel = 1;
        PlayerExp = 0;

        //CreateUserData(0, 0);
        //CreateUserData(1, 3);
        //CreateUserData(2, 4);
        //sceneState = SceneState.BattleScene;
    }

    public void CreateUserData(int id, int charactor)
    {
        //if (transforms[id].GetChild(0).TryGetComponent<PHM_CharStat>(out charStat))
        //Debug.Log($"charStat성공{charStat.CharName}");

        Prefeb[charactor].TryGetComponent<PHM_CharStat>(out charStat);

        player[id].id = charactor;
        player[id].CharName = charStat.CharName;
        player[id].CharJob = charStat.CharJob;
        player[id].Accuracy = charStat.Accuracy;
        player[id].P_Defense = charStat.P_Defense;
        player[id].M_Defense = charStat.M_Defense;
        player[id].Strength = charStat.Strength;
        player[id].Magic = charStat.Magic;
        player[id].Critical = charStat.Critical;
        player[id].Speed = charStat.Speed;
        player[id].Helth = charStat.Helth;

        player[id].SpecialAB = charStat.SpecialAB;
        player[id].Skill01 = charStat.Skill01;
        player[id].Skill02 = charStat.Skill02;

        player[id].MaxHP = 10 + (player[id].Helth * 2);
        player[id].CurHP = player[id].MaxHP;

        //player[id].Level = 1;
        //player[id].EXP = 0;

        player[id].onPlayerDead = charStat.onPlayerDead = false;

        //playerObjectToId.Add(Prefeb[charactor], id);
    }

    public void Player_Select(int pos, int charactor)
    {
        PlayerPos = GameObject.FindFirstObjectByType<SelectPlayerPos>();
        if (PlayerPos.transforms[pos].childCount > 0)
            Destroy(PlayerPos.transforms[pos].GetChild(0).gameObject);
        GameObject obj = Instantiate(Prefeb[charactor], PlayerPos.transforms[pos]);
        obj.transform.SetParent(PlayerPos.transforms[pos]);
    }

    //public void RegisterPlayerGameObject(GameObject playerObject, int playerId)
    //{
    //    if (!playerObjectToId.ContainsKey(playerObject))
    //    {
    //        playerObjectToId.Add(playerObject, playerId);
    //    }
    //    else
    //    {
    //        playerObjectToId[playerObject] = playerId;
    //    }
    //}

    public int GetPlayerIdByGameObject(GameObject playerObject)
    {
        if (playerObjectToId.TryGetValue(playerObject, out int playerId))
        {
            return playerId;
        }
        else
        {
            Debug.LogError("Player ID not found for GameObject: " + playerObject.name);
            return -1; // 예외 처리 또는 기본 값 설정
        }
    }


    public void ResurrectPlayer(int playerId)
    {
        // 특정 playerId에 해당하는 플레이어를 부활시키는 로직을 구현
        // 예를 들어:
        player[playerId].onPlayerDead = false;
        player[playerId].CurHP = player[playerId].MaxHP; // 체력 회복 등의 처리 추가 가능
    }

    private MoveUIManager M_UI;

    public void RoundEnd()
    {
        M_UI = GameObject.FindFirstObjectByType<MoveUIManager>();
        StartCoroutine("RoundChange");
        M_UI.RoundEND();
    }

    private IEnumerator RoundChange()
    {
        MonsterLevel++;
        yield return new WaitForSeconds(6f);
        Dice = 5;
        M_UI.D_TextChange();
    }

    public void MoveUIText()
    {
        M_UI = GameObject.FindFirstObjectByType<MoveUIManager>();
        M_UI.D_TextChange();
        M_UI.P_TextChange();
    }

    public void PlayerLevelUp()
    {
        if (PlayerExp >= MaxEXP[PlayerLevel - 1])
        {
            PlayerLevel++;

            for (int i = 0; i < player.Length; i++)
            {
                Prefeb[player[i].id].TryGetComponent<PHM_CharStat>(out charStat);
                player[i].Accuracy = charStat.Accuracy + (int)((PlayerLevel - 1) / 2);
                player[i].P_Defense = charStat.P_Defense + (PlayerLevel - 1);
                player[i].M_Defense = charStat.M_Defense + (PlayerLevel - 1);
                player[i].Strength = charStat.Strength + (PlayerLevel - 1);
                player[i].Magic = charStat.Magic + (PlayerLevel - 1);
                player[i].Critical = charStat.Critical + (int)((PlayerLevel - 1) / 2);
                player[i].Speed = charStat.Speed + (int)((PlayerLevel - 1) / 2);
                player[i].Helth = charStat.Helth + (PlayerLevel - 1);

                player[i].MaxHP = 10 + (player[i].Helth * 2);
                player[i].CurHP += 4;
                if(player[i].CurHP>player[i].MaxHP)
                    player[i].CurHP = player[i].MaxHP;
            }
            if (PlayerExp >= MaxEXP[PlayerLevel - 1])
                PlayerLevelUp();
        }
    }

}
