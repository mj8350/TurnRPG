using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
    }

    public override void Skill_Active()
    {
        Debug.Log("Ÿ�ٽ�ų �ߵ�");
        targetObject = click.selectedObj;
        if (targetObject != null)
        {
            PHM_CharStat stat = GetComponent<PHM_CharStat>();
            if (stat != null)
            {
                // ĳ������ ���ݷ��� ������� ������ ���
                int damage = stat.Strength;
                GameManager.Instance.Damage(targetObject, damage);
            }
            
        }
        else
        {
            Debug.LogError("Ÿ�� ������Ʈ�� �����ϴ�.");
        }
    }
}
