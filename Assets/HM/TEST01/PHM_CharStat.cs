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

    public string CharName; // ĳ���� �̸�
    public int Accuracy; // ���߷�
    public int P_Defense; // ��������
    public int M_Defense; // ��������
    public int Strength; // ���ݷ�
    public int Magic; // ������
    public int Critical; // ġ��Ÿ 
    public int Speed; // �ӵ�

    //public PHM_Stat STR; // �� ����
    //public PHM_Stat DEX; // ��ø ����
    //public PHM_Stat LUK; // �� ���� (���߷� ����?)
    //public PHM_Stat INT; // ���� ����


    public delegate void ChangeHPDelegate(int changeHP);
    ChangeHPDelegate HPDelegate;
    public static event ChangeHPDelegate changeHP;

    private void Start()
    {
        maxHP = curHP = 100;
        maxMP = curMP = 100;

        //HPDelegate += ModifyHP; // ��������Ʈ �̺�Ʈ ����
        //HPDelegate(30);
    }


    private void ModifyHP(int newHP)
    {
        curHP = newHP;
        // ü�� ���� �̺�Ʈ
        changeHP?.Invoke(newHP);
    }
        

}
