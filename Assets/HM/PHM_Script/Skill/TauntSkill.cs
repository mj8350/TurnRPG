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
        Debug.Log("도발스킬 발동");
        targetObject = click.selectedObj;
        if (targetObject != null)
        {
            FightManager.Instance.onTaunt = true;
            FightManager.Instance.tauntTarget = this.gameObject; // 몬스터의 타겟을 자기 자신으로 변경
        }
        else
        {
            Debug.LogError("타겟 오브젝트가 없습니다.");
        }
    }
}
