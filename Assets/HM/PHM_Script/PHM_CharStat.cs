using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PHM_CharStat : MonoBehaviour, IDamage
{
    private int maxHP;
    private int curHP;
    
    public string CharName; // ĳ���� �̸�
    public string CharJob; // ĳ���� ����
    public int Accuracy; // ���߷�
    public int P_Defense; // ��������
    public int M_Defense; // ��������
    public int Strength; // ���ݷ�
    public int Magic; // ������
    public int Critical; // ġ��Ÿ 
    public int Speed; // �ӵ�
    public int Helth; // ü��
    public string SpecialAB; // ���� �ɷ�
    public string Skill01;
    public string Skill02;


    // ���� : ���� ����, ���� ���ݷ� ����.
    // ������ : ���� ������, ���� �� ���.
    // �ü� : ���� ���߷�, �뷱�� ��.
    // ���� : ���� ġ��Ÿ, ���� �ӵ�.
    // ���� : ���� ���� �ӵ��� ���� �� ������ ����.


    //public delegate void ChangeHPDelegate(int changeHP);
    //ChangeHPDelegate HPDelegate;
    //public static event ChangeHPDelegate changeHP;

    private void Start()
    {
        //HPDelegate += ModifyHP; // ��������Ʈ �̺�Ʈ ����
        //HPDelegate(30);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"{CharName}��(��) {damage}�� �������� �Ծ����ϴ�.");
    }


    //private void ModifyHP(int newHP)
    //{
    //    curHP = newHP;
    //    // ü�� ���� �̺�Ʈ
    //    changeHP?.Invoke(newHP);
    //}


}
