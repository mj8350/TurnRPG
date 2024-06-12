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

    public int Level;
    public int EXP;
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
}

public class GameManager : Singleton<GameManager>
{
    public Player[] player = new Player[3];
    public PHM_CharStat charStat;
    public Transform[] transforms;


    public GameObject[] Prefeb;



    private void Awake()
    {
        base.Awake();

        for (int i = 0; i < 3; i++)
        {
            int ran = Random.Range(0, Prefeb.Length);
            CreateUserData(i, ran);
        }

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

        player[id].MaxHP = 10 + (player[id].Helth*2);
        player[id].CurHP = player[id].MaxHP;

        player[id].Level = 1;
        player[id].EXP = 0;

}

    public void Player_Select(int pos, int charactor)
    {
        if (transforms[pos].childCount > 0)
            Destroy(transforms[pos].GetChild(0).gameObject);
        GameObject obj = Instantiate(Prefeb[charactor], transforms[pos]);
        obj.transform.SetParent(transforms[pos]);
    }

    public void Damage(GameObject obj, int damage)
    {
        if(obj.TryGetComponent<IDamage>(out IDamage idam))
            idam.TakeDamage(damage);
    }
    

}
