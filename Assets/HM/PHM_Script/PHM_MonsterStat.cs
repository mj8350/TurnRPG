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

    private void Awake()
    {
        maxHP = 10 + (Helth * 2);
        curHP = maxHP;
        Level = 1;
    }
    private void Update()
    {
        if (curHP<=0)
        {
            curHP = 0;
        }
    }
}
