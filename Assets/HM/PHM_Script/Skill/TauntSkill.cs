using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;

    private void Awake()
    {
        click = gameObject.AddComponent<ClickEvent>();
    }

    public override void Skill_Active()
    {
        Debug.Log("���߽�ų �ߵ�");
        targetObject = click.selectedObj;
        if (targetObject != null)
        {
            FightManager.Instance.onTaunt = true;
            FightManager.Instance.tauntTarget = this.gameObject; // ������ Ÿ���� �ڱ� �ڽ����� ����
        }
        else
        {
            Debug.LogError("Ÿ�� ������Ʈ�� �����ϴ�.");
        }
    }
}
