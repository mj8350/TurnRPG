using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;
    public Roulette roulette;
    private int successProbability = 60;

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
        roulette = GameObject.FindFirstObjectByType<Roulette>();
    }

    public override void Skill_Active()
    {
        if (roulette.randomNumber < successProbability)
        {
            Debug.Log("���߽�ų �ߵ�");
            roulette.isAttackSuccessful = true;
            roulette.InvokeInitRoulette();
            gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
            motion.PlayerStartAttack();
            targetObject = click.selectedObj;
            if (targetObject != null)
            {
                FightManager.Instance.onTaunt = true;
                FightManager.Instance.tauntTarget = this.gameObject; // ������ Ÿ���� �ڱ� �ڽ����� ����
            }
            else
            {
                Debug.LogError("Ÿ�� ������Ʈ�� �����ϴ�.");
            }
        }
        else
        {
            roulette.isAttackSuccessful = false;
            Debug.Log("���� ����!");
            roulette.InvokeInitRoulette();
        }

    }
}
