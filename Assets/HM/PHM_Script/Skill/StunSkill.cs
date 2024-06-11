using Assets.HeroEditor.Common.Scripts.ExampleScripts;
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
        Debug.Log("스턴스킬 발동");
        gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
        motion.PlayerStartAttack();
        targetObject = click.selectedObj;
        if (targetObject != null)
        {
            FightManager.Instance.onStun = true;
        }
        else
        {
            Debug.LogError("타겟 오브젝트가 없습니다.");
        }
    }
}
