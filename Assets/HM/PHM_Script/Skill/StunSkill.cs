using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;
    private List<GameObject> stunTargets;
    public Roulette roulette;
    private int successProbability;

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
        roulette = GameObject.FindFirstObjectByType<Roulette>();
        //successProbability = 25 + (stat.Accuracy * 2);
        successProbability = 25 + ((GameManager.Instance.player[FightManager.Instance.GMChar(gameObject)].Accuracy*2));

        //stunTargets = new List<GameObject>();
    }

    public override void Skill_Active()
    {
        if (roulette.randomNumber <= successProbability)
        {
            Debug.Log("���� ��ġ�� �ߵ�");

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
                PHM_CharStat stat = GetComponent<PHM_CharStat>();
                targetObject.TryGetComponent<MonsterAttack>(out MonsterAttack stunTargetScript);
                if (stunTargetScript != null)
                {
                    click.selectedObj.TryGetComponent<PHM_MonsterStat>(out PHM_MonsterStat monStat);
                    int number = FightManager.Instance.GMChar(gameObject);
                    int damage = FightManager.Instance.DamageSum((int)(GameManager.Instance.player[number].Strength /2), GameManager.Instance.player[number].Critical, monStat.P_Defense, out onCri);
                    FightManager.Instance.Damage(targetObject, damage, onCri);
                    stunTargetScript.SetStunTarget(); // ���ϵ� ������ Ÿ�� ����
                    
                }
                else
                {
                    Debug.LogError("���� ����� �ùٸ� ��ũ��Ʈ�� ������ ���� �ʽ��ϴ�.");
                }
            }
        }
        else
        {
            // �귿 ���н� ó��
            roulette.isAttackSuccessful = false;
            Debug.Log("���� ��ġ�� ����!");
            StartCoroutine(DamageT(gameObject));
            roulette.InvokeInitRoulette();
        }
    }
}
