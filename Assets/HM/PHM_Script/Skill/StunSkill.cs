using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
    }

    public override void Skill_Active()
    {
        Debug.Log("���Ͻ�ų �ߵ�");
        targetObject = click.selectedObj;
        if (targetObject != null)
        {
            FightManager.Instance.onStun = true;
        }
        else
        {
            Debug.LogError("Ÿ�� ������Ʈ�� �����ϴ�.");
        }
    }
}
