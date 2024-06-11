using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;
    private float wideHealProbability = 0.1f; // 광역 힐 발동 확률

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
    }

    public override void Skill_Active()
    {
        Debug.Log("힐 스킬 발동");

        // 확률에 따라 광역 힐을 적용할지 결정
        if (Random.value < wideHealProbability)
        {
            Debug.Log("광역 힐 발동");
            gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
            motion.Victory();
            ApplyWideHeal();
        }
        else
        {
            Debug.Log("단일 힐 발동");
            gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
            motion.Victory();
            ApplySingleHeal();
        }
    }

    // 광역 힐을 적용하는 함수
    private void ApplyWideHeal()
    {
        // 현재 위치에서 반경 내에 있는 모든 player 태그를 가진 오브젝트에게 힐을 적용
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5f); // 반경 5의 영역 내의 모든 콜라이더 가져오기

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                ApplyHeal(collider.gameObject);
            }
        }
    }

    // 단일 힐을 적용하는 함수
    private void ApplySingleHeal()
    {
        targetObject = click.selectedObj;
        if (targetObject != null && targetObject.CompareTag("Player"))
        {
            ApplyHeal(targetObject);
        }
        else
        {
            Debug.LogError("힐 대상 오브젝트가 없거나 player 태그를 가지고 있지 않습니다.");
        }
    }

    // 힐을 적용하는 함수
    private void ApplyHeal(GameObject target)
    {
        // 선택한 대상에게서 체력 컴포넌트를 찾습니다.
        PHM_CharStat healthComponent = target.GetComponent<PHM_CharStat>();
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
