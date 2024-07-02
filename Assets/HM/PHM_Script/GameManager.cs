using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public int id;
    public string CharName; // ĳ���� �̸�
    public string CharJob; // ĳ���� ����
    public int Accuracy; // ���߷�
    public int P_Defense; // ��������
    public int M_Defense; // ��������
    public int Strength; // ���ݷ�
    public int Magic; // ������
    public int Critical; // ġ��Ÿ 
    public int Speed; // �ӵ�
    public int Helth; // ü��
    public string SpecialAB; // ���� �ɷ�
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
    public string MonsterName; // ĳ���� �̸�
    public int Accuracy; // ���߷�
    public int P_Defense; // ��������
    public int M_Defense; // ��������
    public int Strength; // ���ݷ�
    public int Magic; // ������
    public int Critical; // ġ��Ÿ 
    public int Speed; // �ӵ�
    public int Helth; // ü��

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
    //public Transform movePlayer;//����->�̵��� ó�� ����, �̵�>��Ʋ�ø��� ����

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
        //Debug.Log($"charStat����{charStat.CharName}");

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
            return -1; // ���� ó�� �Ǵ� �⺻ �� ����
        }
    }


    public void ResurrectPlayer(int playerId)
    {
        // Ư�� playerId�� �ش��ϴ� �÷��̾ ��Ȱ��Ű�� ������ ����
        // ���� ���:
        player[playerId].onPlayerDead = false;
        player[playerId].CurHP = player[playerId].MaxHP; // ü�� ȸ�� ���� ó�� �߰� ����
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
