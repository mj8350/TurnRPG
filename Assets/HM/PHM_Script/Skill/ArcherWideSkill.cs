using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherWideSkill : BaseSkill
{
    private ClickEvent click;
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

        if (roulette.randomNumber <= successProbability || roulette.randomNumber_2 <= 40)
        {
            Debug.Log("ȭ��� �ߵ�");
            roulette.isAttackSuccessful = true;
            roulette.InvokeInitRoulette();
            gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
            motion.PlayerStartAttack();
            ApplyWideDamage();
        }
        else
        {
            roulette.isAttackSuccessful = false;
            Debug.Log("ȭ��� ����!");
            StartCoroutine(DamageT(gameObject));
            roulette.InvokeInitRoulette();
        }
    }


    private void ApplyWideDamage()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster"); // �ʿ� �ִ� ��� ���͵��� ã��
        foreach (GameObject monster in monsters)
        {
            ApplyDamage(monster); // �� ���Ϳ��� �������� ����
        }
    }

    private void ApplyDamage(GameObject monster)
    {
        if (monster != null)
        {
            monster.TryGetComponent<PHM_MonsterStat>(out PHM_MonsterStat monStat);
            // ĳ������ ���ݷ��� ������� ������ ��� // ���� ������ ���� ����
            //int damage = stat.Magic - monStat.M_Defense;
            int number = FightManager.Instance.GMChar(gameObject);
            int damage = FightManager.Instance.DamageSum(GameManager.Instance.player[number].Strength, GameManager.Instance.player[number].Critical, monStat.P_Defense, out onCri);
            FightManager.Instance.Damage(monster, damage, onCri);
            
        }
        else
        {
            Debug.LogWarning("MonsterStats ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }
}
