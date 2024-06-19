using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using Assets.HeroEditor.FantasyHeroes.TestRoom.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardWideSkill : BaseSkill
{
    private ClickEvent click;
    public Roulette roulette;
    private int successProbability;

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
        roulette = GameObject.FindFirstObjectByType<Roulette>();
        TryGetComponent<PHM_CharStat>(out stat);
        //successProbability = 20 + (stat.Accuracy * 3);
        successProbability = 20 + ((GameManager.Instance.player[FightManager.Instance.GMChar(gameObject)].Accuracy * 3));
    }

    public override void Skill_Active()
    {

        if (roulette.randomNumber <= successProbability)
        {
            // 1%�� Ȯ���� 99999�� �������� ����
            if (Random.Range(1, 101) <= 1)
            {
                Debug.Log("�����ɷ� �ߵ�!");
                roulette.isAttackSuccessful = true;
                roulette.InvokeInitRoulette();
                gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
                motion.PlayerStartAttack();
                ApplyWideMaxDamage();


            }
            else
            {
                Debug.Log("���� �ߵ�");
                roulette.isAttackSuccessful = true;
                roulette.InvokeInitRoulette();
                gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
                motion.PlayerStartAttack();
                ApplyWideDamage();
            }
        }
        else
        {
            roulette.isAttackSuccessful = false;
            Debug.Log("���� ����!");
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
            int damage = FightManager.Instance.DamageSum(GameManager.Instance.player[number].Magic, GameManager.Instance.player[number].Critical, monStat.M_Defense, out onCri);
            FightManager.Instance.Damage(monster, damage, onCri);
        }
        else
        {
            Debug.Log("��� ���Ͱ� �����ϴ�.");
        }
    }

    private void ApplyWideMaxDamage()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster"); // �ʿ� �ִ� ��� ���͵��� ã��
        foreach (GameObject monster in monsters)
        {
            ApplyMaxDamage(monster); // �� ���Ϳ��� �������� ����
        }
    }

    private void ApplyMaxDamage(GameObject monster)
    {
        if (monster != null)
        {
            monster.TryGetComponent<PHM_MonsterStat>(out PHM_MonsterStat monStat);
            FightManager.Instance.Damage(monster, 99999, onCri);
        }
        else
        {
            Debug.Log("��� ���Ͱ� �����ϴ�.");
        }
    }
}
