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

    public int Stage;
    public int Value;

    private void Awake()
    {
        Level = GameManager.Instance.MonsterLevel;

        LevelUpdate();

        maxHP = 10 + (Helth * 2);
        curHP = maxHP;
    }
    private void Update()
    {
        if (curHP<=0)
        {
            curHP = 0;
        }
    }

    private void LevelUpdate()//������ ���� ��������
    {
        Accuracy += (Level-1) * 2;
        P_Defense += (Level - 1) * 2;
        M_Defense += (Level - 1) * 2;
        Strength += (Level - 1) * 2;
        Magic += (Level - 1) * 2;
        Critical += (Level - 1) * 2;
        Speed += (Level - 1) * 2;
        Helth += (Level - 1) * 2;
    }
}
