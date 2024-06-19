using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardTargetSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;
    public Roulette roulette;
    private int successProbability;

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
        roulette = GameObject.FindFirstObjectByType<Roulette>();
        //successProbability = 10 + (stat.Accuracy * 3);
        successProbability = 10 + ((GameManager.Instance.player[FightManager.Instance.GMChar(gameObject)].Accuracy * 3));
    }


    public override void Skill_Active()
    {
        if (roulette.randomNumber <= successProbability)
        {
            // 3%�� Ȯ���� 99999�� �������� ����
            if (Random.Range(1, 101) <= 3)
            {
                Debug.Log("�����ɷ� �ߵ�!");
                roulette.isAttackSuccessful = true;
                roulette.InvokeInitRoulette();
                gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
                motion.PlayerStartAttack();
                targetObject = click.selectedObj;
                if (targetObject != null)
                {
                    // 99999�� �������� ��󿡰� ����
                    FightManager.Instance.Damage(targetObject, 99999, onCri);
                }
                else
                {
                    Debug.LogError("Ÿ�� ������Ʈ�� �����ϴ�.");
                }
            }
            else
            {
                // ������ ��쿡�� ������ ������� ����
                Debug.Log("������� �ߵ�");
                roulette.isAttackSuccessful = true;
                roulette.InvokeInitRoulette();
                gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
                motion.PlayerStartAttack();
                targetObject = click.selectedObj;
                if (targetObject != null)
                {
                    click.selectedObj.TryGetComponent<PHM_MonsterStat>(out PHM_MonsterStat monStat);
                    // ĳ������ ���ݷ��� ������� ������ ���
                    int number = FightManager.Instance.GMChar(gameObject);
                    int damage = FightManager.Instance.DamageSum((int)(GameManager.Instance.player[number].Magic * 1.5f), GameManager.Instance.player[number].Critical, monStat.M_Defense, out onCri);
                    FightManager.Instance.Damage(targetObject, damage, onCri);

                }
                else
                {
                    Debug.LogError("Ÿ�� ������Ʈ�� �����ϴ�.");
                }
            }
        }
        else
        {
            roulette.isAttackSuccessful = false;
            Debug.Log("������� ����!");
            StartCoroutine(DamageT(gameObject));
            roulette.InvokeInitRoulette();
        }

    }
}
