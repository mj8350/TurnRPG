using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterStats", menuName = "Character Stats")]
public class PHM_CreateStats : ScriptableObject
{
    public string CharName; // ĳ���� �̸�
    public int Accuracy; // ���߷�
    public int Defense; // ����
    public int Strength; // ���ݷ�
    public int Magic; // ������
    public int Range; // �����Ÿ�
    public int Critical; // ġ��Ÿ 
    public int Speed; // �ӵ�
    public int HP;
    public int MP;
}
