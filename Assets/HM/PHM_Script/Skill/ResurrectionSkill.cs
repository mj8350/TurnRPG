using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectionSkill : BaseSkill
{
    private ClickEvent click;
    public Roulette roulette;
    private GameObject targetObject;
    private int successProbability;


    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
        roulette = GameObject.FindFirstObjectByType<Roulette>();
        //successProbability = 10 + (stat.Accuracy);
        successProbability = 10 + ((GameManager.Instance.player[FightManager.Instance.GMChar(gameObject)].Accuracy));
    }

    public override void Skill_Active()
    {
        targetObject = click.selectedObj;
        if (FightManager.Instance.IsResurrectionTarget(targetObject))
        {
            if (roulette.randomNumber <= successProbability)
            {
                Debug.Log("��Ȱ �ߵ�");
                roulette.isAttackSuccessful = true;
                roulette.InvokeInitRoulette();
                gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
                motion.Victory();
                ApplyResurrection();

            }
            else
            {
                roulette.isAttackSuccessful = false;
                Debug.Log("��Ȱ ����!");
                StartCoroutine(DamageT(gameObject));
                roulette.InvokeInitRoulette();
            }
        }
        else
        {
            roulette.isAttackSuccessful = false;
            Debug.Log("��Ȱ ����� �ƴմϴ�.");
            roulette.InvokeInitRoulette();
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
            Debug.LogError("��Ȱ ����� �ƴմϴ�.");
        }
    }
    
}
