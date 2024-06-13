using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHM_MonsterStat : MonoBehaviour
{
    public int maxHP;
    public int curHP;

    public string MonsterName; // ĳ���� �̸�
    public int Accuracy; // ���߷�
    public int P_Defense; // ��������
    public int M_Defense; // ��������
    public int Strength; // ���ݷ�
    public int Magic; // ������
    public int Critical; // ġ��Ÿ 
    public int Speed; // �ӵ�
    public int Helth; // ü��

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
