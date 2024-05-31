using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PHM_CharStat : MonoBehaviour, IDamage
{
    private int maxHP;
    private int curHP;
    
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


    // 전사 : 높은 방어력, 높은 공격력 위주.
    // 마법사 : 높은 마법력, 광역 딜 담당.
    // 궁수 : 높은 명중률, 밸런스 형.
    // 도적 : 높은 치명타, 빠른 속도.
    // 사제 : 가장 빠른 속도를 가져 팀 버프에 도움.


    //public delegate void ChangeHPDelegate(int changeHP);
    //ChangeHPDelegate HPDelegate;
    //public static event ChangeHPDelegate changeHP;

    private void Start()
    {
        //HPDelegate += ModifyHP; // 델리게이트 이벤트 구독
        //HPDelegate(30);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"{CharName}이(가) {damage}의 데미지를 입었습니다.");
    }


    //private void ModifyHP(int newHP)
    //{
    //    curHP = newHP;
    //    // 체력 관련 이벤트
    //    changeHP?.Invoke(newHP);
    //}


}
