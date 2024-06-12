using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using Assets.HeroEditor.FantasyHeroes.TestRoom.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideSkill : BaseSkill
{
    private ClickEvent click;
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
            Debug.Log("������ų �ߵ�");
            roulette.isAttackSuccessful = true;
            roulette.InvokeInitRoulette();
            gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
            motion.PlayerStartAttack();
            ApplyWideDamage();
        }
        else
        {
            roulette.isAttackSuccessful = false;
            Debug.Log("���� ����!");
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
            // ���Ϳ��� ������ ����
            PHM_CharStat stat = GetComponent<PHM_CharStat>();
            if (stat != null)
            {
                // ĳ������ ���ݷ��� ������� ������ ��� // ���� ������ ���� ����
                int damage = stat.Strength;
                GameManager.Instance.Damage(monster, damage);
            }
            else
            {
                Debug.LogWarning("MonsterStats ������Ʈ�� ã�� �� �����ϴ�.");
            }
        }
    }
}
