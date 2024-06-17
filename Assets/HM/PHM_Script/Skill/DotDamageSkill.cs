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
    private int successProbability;

    public int damage;

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
        roulette = GameObject.FindFirstObjectByType<Roulette>();
        TryGetComponent<PHM_CharStat>(out stat);
        successProbability = 10 + (stat.Accuracy * 2);
    }

    public override void Skill_Active()
    {
        if (roulette.randomNumber <= successProbability)
        {
            Debug.Log("��Ʈ�� ��ų �ߵ�");

            // �귿 ���� ���� ����
            roulette.isAttackSuccessful = true;
            roulette.InvokeInitRoulette();

            // ���� ��� ����
            gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
            motion.PlayerStartAttack();

            // ���� ��� ����
            targetObject = click.selectedObj;
            if (targetObject != null)
            {
                targetObject.TryGetComponent<MonsterAttack>(out MonsterAttack dotDamgeTarget);
                if (dotDamgeTarget != null)
                {
                    dotDamgeTarget.SetDotDamage(); // ���ϵ� ������ Ÿ�� ����
                }
                else
                {
                    Debug.LogError("��ų ����� �ùٸ��� �ʽ��ϴ�.");
                }

                PHM_CharStat stat = GetComponent<PHM_CharStat>();
                click.selectedObj.TryGetComponent<PHM_MonsterStat>(out PHM_MonsterStat monStat);
                if (stat != null)
                {
                    int damage = stat.Strength / 2;
                    FightManager.Instance.Damage(targetObject, damage, onCri); // todo: �ִϸ��̼� ��� �ʿ�
                }
            }
        }
        else
        {
            // �귿 ���н� ó��
            roulette.isAttackSuccessful = false;
            Debug.Log("��ų ����!");
            roulette.InvokeInitRoulette();
        }
    }
}
