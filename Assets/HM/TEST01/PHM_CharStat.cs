using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PHM_CharStat : MonoBehaviour
{
    public int maxHP;
    public int curHP;
    public int maxMP;
    public int curMP;
    public int Exp;

    public PHM_Stat STR; // �� ����
    public PHM_Stat DEX; // ��ø ����
    public PHM_Stat LUK; // �� ���� (���߷� ����?)
    public PHM_Stat INT; // ��� ����


    public delegate void ChangeHPDelegate(int changeHP);
    ChangeHPDelegate HPDelegate;
    public static event ChangeHPDelegate changeHP;

    private void Start()
    {
        maxHP = curHP = 100;
        maxMP = curMP = 100;

        //HPDelegate += ModifyHP; // ��������Ʈ �̺�Ʈ ����
        //HPDelegate(30);

        Debug.Log(STR.GetStat()+5);
    }



    private void ModifyHP(int newHP)
    {
        curHP = newHP;
        // ü�� ���� �̺�Ʈ
        changeHP?.Invoke(newHP);
    }

}
