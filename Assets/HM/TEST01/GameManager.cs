using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Player
{
    //public int id;
    public string CharName; // 캐릭터 이름
    public string CharJob; // 캐릭터 직업
    public int Accuracy; // 명중률
    public int P_Defense; // 물리방어력
    public int M_Defense; // 마법방어력
    public int Strength; // 공격력
    public int Magic; // 마법력
    public int Critical; // 치명타 
    public int Speed; // 속도

    public int MaxHP;
    public int CurHP;

    public int Level;
    public int EXP;
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
    }

    public void CreateUserData(int id, int charactor)
    {
        //if (transforms[id].GetChild(0).TryGetComponent<PHM_CharStat>(out charStat))
        //Debug.Log($"charStat성공{charStat.CharName}");

        Prefeb[charactor].TryGetComponent<PHM_CharStat>(out charStat);

        player[id].CharName = charStat.CharName;
        player[id].CharJob = charStat.CharJob;
        player[id].Accuracy = charStat.Accuracy;
        player[id].P_Defense = charStat.P_Defense;
        player[id].M_Defense = charStat.M_Defense;
        player[id].Strength = charStat.Strength;
        player[id].Magic = charStat.Magic;
        player[id].Critical = charStat.Critical;
        player[id].Speed = charStat.Speed;
        
    }

    public void Player_Select(int pos, int charactor)
    {
        if (transforms[pos].childCount > 0)
            Destroy(transforms[pos].GetChild(0).gameObject);
        GameObject obj = Instantiate(Prefeb[charactor], transforms[pos]);
        obj.transform.SetParent(transforms[pos]);
    }

}
