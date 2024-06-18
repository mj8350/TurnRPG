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

    public int Level;
    public int EXP;

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

public class GameManager : Singleton<GameManager>
{
    public Player[] player = new Player[3];
    public PHM_CharStat charStat;
    public Transform[] transforms;

    private Dictionary<GameObject, int> playerObjectToId = new Dictionary<GameObject, int>();

    public GameObject[] Prefeb;

    public int[] MaxEXP = { 10, 20, 30, 40, 50, 60, 70, 80, 90 };

    private void Awake()
    {
        base.Awake();

        //for (int i = 0; i < 3; i++)
        //{
        //    int ran = Random.Range(0, Prefeb.Length);
        //    CreateUserData(i, ran);
        //}

        CreateUserData(0, 4);
        CreateUserData(1, 1);
        CreateUserData(2, 2);
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

        player[id].Level = 1;
        player[id].EXP = 0;

        player[id].onPlayerDead = charStat.onPlayerDead = false;

        //playerObjectToId.Add(Prefeb[charactor], id);
    }

    public void Player_Select(int pos, int charactor)
    {
        if (transforms[pos].childCount > 0)
            Destroy(transforms[pos].GetChild(0).gameObject);
        GameObject obj = Instantiate(Prefeb[charactor], transforms[pos]);
        obj.transform.SetParent(transforms[pos]);
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

}
