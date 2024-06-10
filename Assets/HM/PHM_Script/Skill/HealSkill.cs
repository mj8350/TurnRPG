using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
    }

    public override void Skill_Active()
    {
        Debug.Log("힐 스킬 발동");
        targetObject = click.selectedObj;
        if (targetObject != null)
        {
            // 선택한 대상의 체력을 회복시킵니다.
            HealTarget();
        }
        else
        {
            Debug.LogError("타겟 오브젝트가 없습니다.");
        }
    }

    private void HealTarget()
    {
        // 선택한 대상에게서 체력 컴포넌트를 찾습니다.
        PHM_CharStat healthComponent = targetObject.GetComponent<PHM_CharStat>();
        if (healthComponent != null)
        {
            // 체력을 회복
            int healingAmount = 5; // 회복량 (예시로 5 설정)
            healthComponent.Helth += healingAmount;
            Debug.Log("대상의 체력을 회복합니다.");
            Debug.Log(healthComponent.Helth);
        }
        else
        {
            Debug.LogError("대상에게 체력을 회복시킬 수 없습니다. 대상에게 체력 컴포넌트가 없습니다.");
        }
    }
}
