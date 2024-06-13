using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDamageSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;
    private int dotDamage = 5; // ��Ʈ ������
    public Roulette roulette;
    private int successProbability = 60;

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
        roulette = GameObject.FindFirstObjectByType<Roulette>();
    }

    public override void Skill_Active()
    {
        if(roulette.randomNumber < successProbability)
        {
            Debug.Log("��Ʈ������ ��ų �ߵ�");
            roulette.isAttackSuccessful = true;
            roulette.InvokeInitRoulette();
            gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
            motion.PlayerStartAttack();
            targetObject = click.selectedObj;
            if (targetObject != null)
            {
                // ���Ϳ��� ��ų�� ����� ��쿡�� ��Ʈ ������ ���� ����
                if (targetObject.CompareTag("Monster"))
                {
                    // ���Ϳ��� ��Ʈ ������ ���� ����
                    ApplyDotDamageState(targetObject);
                }
                else
                {
                    FightManager.Instance.Damage(targetObject, dotDamage);
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

    // ���Ϳ��� ��Ʈ ������ ���¸� �����ϴ� �Լ�
    private void ApplyDotDamageState(GameObject monster)
    {
        // �� ���ÿ����� ���Ϳ��� Ư���� ���¸� �ο��ϴ� ������ ����
        //monster.GetComponent<MonsterController>().ApplyDotDamageState();
    }
}
