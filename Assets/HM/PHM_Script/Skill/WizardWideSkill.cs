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
            // 1%의 확률로 99999의 데미지를 입힘
            if (Random.Range(1, 101) <= 1)
            {
                Debug.Log("고유능력 발동!");
                roulette.isAttackSuccessful = true;
                roulette.InvokeInitRoulette();
                gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
                motion.PlayerStartAttack();
                ApplyWideMaxDamage();


            }
            else
            {
                Debug.Log("해일 발동");
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
            Debug.Log("해일 실패!");
            StartCoroutine(DamageT(gameObject));
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
            monster.TryGetComponent<PHM_MonsterStat>(out PHM_MonsterStat monStat);
            // 캐릭터의 공격력을 기반으로 데미지 계산 // 추후 데미지 계산식 적용
            //int damage = stat.Magic - monStat.M_Defense;
            int number = FightManager.Instance.GMChar(gameObject);
            int damage = FightManager.Instance.DamageSum(GameManager.Instance.player[number].Magic, GameManager.Instance.player[number].Critical, monStat.M_Defense, out onCri);
            FightManager.Instance.Damage(monster, damage, onCri);
        }
        else
        {
            Debug.Log("대상 몬스터가 없습니다.");
        }
    }

    private void ApplyWideMaxDamage()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster"); // 맵에 있는 모든 몬스터들을 찾음
        foreach (GameObject monster in monsters)
        {
            ApplyMaxDamage(monster); // 각 몬스터에게 데미지를 적용
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
            Debug.Log("대상 몬스터가 없습니다.");
        }
    }
}
