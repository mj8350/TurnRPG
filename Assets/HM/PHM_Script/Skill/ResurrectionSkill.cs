using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectionSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;

    private void Awake()
    {
        click = gameObject.AddComponent<ClickEvent>();
    }

    public override void Skill_Active()
    {
        Debug.Log("부활 스킬 발동");

        // 부활 대상을 직접 설정하거나, 특정 조건에 따라 부활 대상을 결정할 수 있습니다.
        targetObject = click.selectedObj;

        if (targetObject != null)
        {
            ResurrectTarget();
        }
        else
        {
            Debug.LogError("부활 대상을 찾을 수 없습니다.");
        }
    }

    private void ResurrectTarget()
    {
        // 부활 대상의 사망 상태를 회복합니다.
        PHM_CharStat healthComponent = targetObject.GetComponent<PHM_CharStat>();
        if (healthComponent != null)
        {
            // 부활 대상의 체력을 초기화하거나 일정량 회복할 수 있습니다.
            int resurrectionAmount = 50; // 예시로 50% 회복량 설정
            healthComponent.Helth += resurrectionAmount;
            Debug.Log("부활 대상을 부활시킵니다.");
        }
        else
        {
            Debug.LogError("부활 대상의 체력을 회복시킬 수 없습니다. 대상에게 체력 컴포넌트가 없습니다.");
        }
    }
}
