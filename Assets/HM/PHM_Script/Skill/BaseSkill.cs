using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    public int Damage;

    public virtual void Skill_Active()
    {
        Debug.Log("��ų �ߵ�");
    }
}
