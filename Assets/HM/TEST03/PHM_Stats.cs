using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHM_Stats : MonoBehaviour
{
    public PHM_UnitCode unitCode { get; } // �����ڵ�� ���� �Ұ�
    public string name { get; set; }
    public int maxHP {  get; set; }
    public int curHP {  get; set; }
    public int maxMP { get; set; }
    public int curMP { get; set; }
    public int Damage {  get; set; }
    public int Speed { get; set; }
    public int AttackRange {  get; set; }
    public int Accuracy { get; set; }
    public int Critical {  get; set; }
}
