using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using Assets.HeroEditor.FantasyHeroes.TestRoom.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideSkill : BaseSkill
{
    

    public override void Skill_Active()
    {
        Debug.Log("������ų �ߵ�");
        gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
        motion.PlayerStartAttack();
        ApplyWideDamage();
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
