using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "YSChar Data", menuName = "Scriptable Object/YSChar Data", order = int.MaxValue)]
public class YSChar : MonoBehaviour
{
    [SerializeField]
    private YSCharData yscharData;
    public YSCharData YSCharData { set { yscharData = value; } }

    public void WatchCharInfo()
    {
        Debug.Log("ĳ���� �̸� :: " + yscharData.ZombieName);
        Debug.Log("ĳ���� ü�� :: " + yscharData.Hp);
        Debug.Log("ĳ���� ���ݷ� :: " + yscharData.Damage);
        Debug.Log("ĳ���� �þ� :: " + yscharData.SightRange);
        Debug.Log("ĳ���� �̵��ӵ� :: " + yscharData.MoveSpeed);


    }
}
