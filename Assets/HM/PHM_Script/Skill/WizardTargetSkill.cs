using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardTargetSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;
    public Roulette roulette;
    private int successProbability = 100;

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
        roulette = GameObject.FindFirstObjectByType<Roulette>();
    }


    public override void Skill_Active()
    {
        if (roulette.randomNumber < successProbability)
        {
            Debug.Log("������� �ߵ�");
            roulette.isAttackSuccessful = true;
            roulette.InvokeInitRoulette();
            gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
            motion.PlayerStartAttack();
            targetObject = click.selectedObj;
            if (targetObject != null)
            {
                PHM_CharStat stat = GetComponent<PHM_CharStat>();
                click.selectedObj.TryGetComponent<PHM_MonsterStat>(out PHM_MonsterStat monStat);
                if (stat != null)
                {
                    // ĳ������ ���ݷ��� ������� ������ ���
                    int number = FightManager.Instance.GMChar(gameObject);
                    int damage = FightManager.Instance.DamageSum((int) (GameManager.Instance.player[number].Magic* 1.5f), GameManager.Instance.player[number].Critical, monStat.M_Defense);
                    FightManager.Instance.Damage(targetObject, damage);
                }

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
