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
        Debug.Log("ĳ���� �̸� :: " + yscharData.CharName);
        Debug.Log("ĳ���� ü�� :: " + yscharData.Hp);
        Debug.Log("ĳ���� ���ݷ� :: " + yscharData.Damage);
        Debug.Log("ĳ���� �þ� :: " + yscharData.SightRange);
        Debug.Log("ĳ���� �̵��ӵ� :: " + yscharData.MoveSpeed);


    }
}
