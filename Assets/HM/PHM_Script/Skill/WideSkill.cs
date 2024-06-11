using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using Assets.HeroEditor.FantasyHeroes.TestRoom.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideSkill : BaseSkill
{
    

    public override void Skill_Active()
    {
        Debug.Log("광역스킬 발동");
        gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
        motion.PlayerStartAttack();
        ApplyWideDamage();
    }

    private void ApplyWideDamage()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster"); // 맵에 있는 모든 몬스터들을 찾음
        foreach (GameObject monster in monsters)
        {
            ApplyDamage(monster); // 각 몬스터에게 데미지를 적용
        }
    }

    private void ApplyDamage(GameObject monster)
    {
        if (monster != null)
        {
            // 몬스터에게 데미지 적용
            PHM_CharStat stat = GetComponent<PHM_CharStat>();
            if (stat != null)
            {
                // 캐릭터의 공격력을 기반으로 데미지 계산 // 추후 데미지 계산식 적용
                int damage = stat.Strength;
                GameManager.Instance.Damage(monster, damage);
            }
            else
            {
                Debug.LogWarning("MonsterStats 컴포넌트를 찾을 수 없습니다.");
            }
        }
    }
}
