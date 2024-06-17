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
        TryGetComponent<PHM_CharStat>(out stat);
        successProbability = 10 + (stat.Accuracy * 3);
    }

    public override void Skill_Active()
    {

        if (roulette.randomNumber <= successProbability || roulette.randomNumber_2 <= 40)
        {
            Debug.Log("화살비 발동");
            roulette.isAttackSuccessful = true;
            roulette.InvokeInitRoulette();
            gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
            motion.PlayerStartAttack();
            ApplyWideDamage();
        }
        else
        {
            roulette.isAttackSuccessful = false;
            Debug.Log("공격 실패!");
            roulette.InvokeInitRoulette();
        }
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
            monster.TryGetComponent<PHM_MonsterStat>(out PHM_MonsterStat monStat);
            if (stat != null)
            {
                // 캐릭터의 공격력을 기반으로 데미지 계산 // 추후 데미지 계산식 적용
                //int damage = stat.Magic - monStat.M_Defense;
                int number = FightManager.Instance.GMChar(gameObject);
                int damage = FightManager.Instance.DamageSum(GameManager.Instance.player[number].Strength, GameManager.Instance.player[number].Critical, monStat.P_Defense, out onCri);
                FightManager.Instance.Damage(monster, damage, onCri);
            }
            else
            {
                Debug.LogWarning("MonsterStats 컴포넌트를 찾을 수 없습니다.");
            }
        }
    }
}
