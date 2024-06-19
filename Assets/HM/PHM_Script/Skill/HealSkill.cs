using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;
    public Roulette roulette;
    private float wideHealProbability = 0.1f; // 광역 힐 발동 확률
    private int successProbability;

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
        roulette = GameObject.FindFirstObjectByType<Roulette>();
        //successProbability = 10 + (stat.Accuracy * 4);
        successProbability = 10 + ((GameManager.Instance.player[FightManager.Instance.GMChar(gameObject)].Accuracy * 4));
    }

    public override void Skill_Active()
    {
        if (roulette.randomNumber <= successProbability)
        {
            // 확률에 따라 광역 힐을 적용할지 결정
            if (Random.value < wideHealProbability)
            {
                Debug.Log("광역 회복 발동");
                roulette.isAttackSuccessful = true;
                roulette.InvokeInitRoulette();
                gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
                motion.Victory();
                ApplyWideHeal();
            }
            else
            {
                Debug.Log("단일 회복 발동");
                roulette.isAttackSuccessful = true;
                roulette.InvokeInitRoulette();
                gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
                motion.Victory();
                ApplySingleHeal();
            }
        }
        else
        {
            roulette.isAttackSuccessful = false;
            Debug.Log("회복 실패!");
            StartCoroutine(DamageT(gameObject));
            roulette.InvokeInitRoulette();
        }
    }

    // 광역 힐을 적용하는 함수
    private void ApplyWideHeal()
    {
        // 현재 위치에서 반경 내에 있는 모든 player 태그를 가진 오브젝트에게 힐을 적용
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5f); // 반경 5의 영역 내의 모든 콜라이더 가져오기
        int number = FightManager.Instance.GMChar(gameObject);
        int heal = GameManager.Instance.player[number].Magic / 2;
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                FightManager.Instance.Heal(collider.gameObject, heal);
            }
        }
    }

    // 단일 힐을 적용하는 함수
    private void ApplySingleHeal()
    {
        targetObject = click.selectedObj;
        PHM_CharStat stat = GetComponent<PHM_CharStat>();
        if (targetObject != null && targetObject.CompareTag("Player"))
        {
            int number = FightManager.Instance.GMChar(gameObject);
            int heal = GameManager.Instance.player[number].Magic / 2;
            FightManager.Instance.Heal(targetObject, heal);
        }
        else
        {
            Debug.Log("힐 대상 오브젝트가 없거나 player 태그를 가지고 있지 않습니다.");
        }
    }

    // 힐을 적용하는 함수
    //private void ApplyHeal(GameObject target)
    //{
    //    // 선택한 대상에게서 체력 컴포넌트를 찾습니다.
    //    PHM_CharStat healthComponent = target.GetComponent<PHM_CharStat>();
    //    if (healthComponent != null)
    //    {
    //        // 체력을 회복
    //        int healingAmount = 5; // 회복량 (예시로 5 설정)
    //        healthComponent.Helth += healingAmount;
    //        Debug.Log("대상의 체력을 회복합니다.");
    //        Debug.Log(healthComponent.Helth);
    //    }
    //    else
    //    {
    //        Debug.LogError("대상에게 체력을 회복시킬 수 없습니다. 대상에게 체력 컴포넌트가 없습니다.");
    //    }
    //}
}
