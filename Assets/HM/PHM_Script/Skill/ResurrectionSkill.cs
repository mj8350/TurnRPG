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
            Debug.Log("��Ȱ ��ų �ߵ�");
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
                Debug.LogError("��Ȱ ����� ã�� �� �����ϴ�.");
            }
        }
        else
        {
            roulette.isAttackSuccessful = false;
            Debug.Log("���� ����!");
            roulette.InvokeInitRoulette();
        }
    }

    private void ResurrectTarget()
    {
        PHM_CharStat healthComponent = targetObject.GetComponent<PHM_CharStat>();
        if (healthComponent != null)
        {
            int resurrectionAmount = 50; // ���÷� 50% ȸ���� ����
            healthComponent.Helth += resurrectionAmount;
            Debug.Log("��Ȱ��ŵ�ϴ�.");
        }
        else
        {
            Debug.LogError("��Ȱ ��ų �� �����ϴ�.");
        }
    }
}
