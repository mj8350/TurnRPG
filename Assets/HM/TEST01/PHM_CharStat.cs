using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PHM_CharStat : MonoBehaviour
{
    private int maxHP;
    private int curHP;
    private int maxMP;
    private int curMP;

    public string CharName; // 캐릭터 이름
    public int Accuracy; // 명중률
    public int P_Defense; // 물리방어력
    public int M_Defense; // 마법방어력
    public int Strength; // 공격력
    public int Magic; // 마법력
    public int Critical; // 치명타 
    public int Speed; // 속도

    //public PHM_Stat STR; // 힘 스텟
    //public PHM_Stat DEX; // 민첩 스텟
    //public PHM_Stat LUK; // 운 스텟 (명중률 관련?)
    //public PHM_Stat INT; // 마력 스텟


    public delegate void ChangeHPDelegate(int changeHP);
    ChangeHPDelegate HPDelegate;
    public static event ChangeHPDelegate changeHP;

    private void Start()
    {
        maxHP = curHP = 100;
        maxMP = curMP = 100;

        //HPDelegate += ModifyHP; // 델리게이트 이벤트 구독
        //HPDelegate(30);
    }


    private void ModifyHP(int newHP)
    {
        curHP = newHP;
        // 체력 관련 이벤트
        changeHP?.Invoke(newHP);
    }
        

}
