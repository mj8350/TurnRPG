using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectionSkill : BaseSkill
{
    private ClickEvent click;
    public Roulette roulette;
    private GameObject targetObject;
    private int successProbability = 20;


    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
        roulette = GameObject.FindFirstObjectByType<Roulette>();
    }

    public override void Skill_Active()
    {
        if (roulette.randomNumber < successProbability)
        {
            Debug.Log("부활 스킬 발동");
            roulette.isAttackSuccessful = true;
            roulette.InvokeInitRoulette();
            gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
            motion.Victory();
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
        else
        {
            roulette.isAttackSuccessful = false;
            Debug.Log("공격 실패!");
            roulette.InvokeInitRoulette();
        }
    }

    private void ResurrectTarget()
    {
        PHM_CharStat healthComponent = targetObject.GetComponent<PHM_CharStat>();
        if (healthComponent != null)
        {
            int resurrectionAmount = 50; // 예시로 50% 회복량 설정
            healthComponent.Helth += resurrectionAmount;
            Debug.Log("부활시킵니다.");
        }
        else
        {
            Debug.LogError("부활 시킬 수 없습니다.");
        }
    }
}
