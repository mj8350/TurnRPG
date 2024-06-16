using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectionSkill : BaseSkill
{
    private ClickEvent click;
    public Roulette roulette;
    private GameObject targetObject;
    private int successProbability = 100;


    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
        roulette = GameObject.FindFirstObjectByType<Roulette>();
    }

    public override void Skill_Active()
    {
        targetObject = click.selectedObj;
        if (FightManager.Instance.IsResurrectionTarget(targetObject))
        {
            if (roulette.randomNumber < successProbability)
            {
                Debug.Log("부활 스킬 발동");
                roulette.isAttackSuccessful = true;
                roulette.InvokeInitRoulette();
                gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
                motion.Victory();
                ApplyResurrection();

            }
            else
            {
                roulette.isAttackSuccessful = false;
                Debug.Log("부활 실패!");
                roulette.InvokeInitRoulette();
            }
        }
        else
        {
            Debug.Log("부활 대상이 아닙니다.");
        }
    
    }

    private void ApplyResurrection()
    {        
        if (targetObject != null && targetObject.CompareTag("Player"))
        {
            FightManager.Instance.Resurrection(targetObject);
        }
        else
        {
            Debug.LogError("부활 대상이 아닙니다.");
        }
    }
    
}
