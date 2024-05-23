using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterStats", menuName = "Character Stats")]
public class PHM_CreateStats : ScriptableObject
{
    public string CharName; // 캐릭터 이름
    public int Accuracy; // 명중률
    public int Defense; // 방어력
    public int Strength; // 공격력
    public int Magic; // 마법력
    public int Range; // 사정거리
    public int Critical; // 치명타 
    public int Speed; // 속도
    public int HP;
    public int MP;
}
