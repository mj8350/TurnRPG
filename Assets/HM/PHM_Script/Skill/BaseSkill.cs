using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    public bool onCri;
    public virtual void Skill_Active()
    {
        Debug.Log("스킬 발동");
    }
}
