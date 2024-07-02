using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHM_MonsterStat : MonoBehaviour
{
    public int maxHP;
    public int curHP;

    public string MonsterName; // 캐릭터 이름
    public int Accuracy; // 명중률
    public int P_Defense; // 물리방어력
    public int M_Defense; // 마법방어력
    public int Strength; // 공격력
    public int Magic; // 마법력
    public int Critical; // 치명타 
    public int Speed; // 속도
    public int Helth; // 체력

    public int Level;

    public int Stage;
    public int Value;

    private void Awake()
    {
        Level = GameManager.Instance.MonsterLevel;

        LevelUpdate();

        maxHP = 10 + (Helth * 2);
        curHP = maxHP;
    }
    private void Update()
    {
        if (curHP<=0)
        {
            curHP = 0;
        }
    }

    private void LevelUpdate()//레벨에 따른 스탯조정
    {
        Accuracy += (Level-1) * 2;
        P_Defense += (Level - 1) * 2;
        M_Defense += (Level - 1) * 2;
        Strength += (Level - 1) * 2;
        Magic += (Level - 1) * 2;
        Critical += (Level - 1) * 2;
        Speed += (Level - 1) * 2;
        Helth += (Level - 1) * 2;
    }
}
