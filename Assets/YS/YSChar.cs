using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class YSChar : MonoBehaviour
{
    [SerializeField]
    private YSCharData yscharData;
    public YSCharData YSCharData { set { yscharData = value; } }

    public void WatchCharInfo()
    {
        Debug.Log("캐릭터 이름 :: " + yscharData.CharName);
        Debug.Log("캐릭터 체력 :: " + yscharData.Hp);
        Debug.Log("캐릭터 공격력 :: " + yscharData.Damage);
        Debug.Log("캐릭터 시야 :: " + yscharData.SightRange);
        Debug.Log("캐릭터 이동속도 :: " + yscharData.MoveSpeed);


    }
}
