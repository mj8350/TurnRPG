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
        TryGetComponent<PHM_CharStat>(out stat);
        successProbability = 25 + (stat.Accuracy * 2);


        stunTargets = new List<GameObject>();
    }

    public override void Skill_Active()
    {
        if (roulette.randomNumber < successProbability)
        {
            Debug.Log("스턴 스킬 발동");

            // 룰렛 성공 여부 설정
            roulette.isAttackSuccessful = true;
            roulette.InvokeInitRoulette();

            // 공격 모션 실행
            gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
            motion.PlayerStartAttack();

            // 스턴 대상 설정
            targetObject = click.selectedObj;
            if (targetObject != null)
            {
                PHM_CharStat stat = GetComponent<PHM_CharStat>();
                targetObject.TryGetComponent<MonsterAttack>(out MonsterAttack stunTargetScript);
                if (stunTargetScript != null)
                { 
                    int damage = stat.Strength / 2;
                    FightManager.Instance.Damage(targetObject, damage, onCri);
                    stunTargetScript.SetStunTarget(); // 스턴된 몬스터의 타겟 설정
                    
                }
                else
                {
                    Debug.LogError("스턴 대상이 올바른 스크립트를 가지고 있지 않습니다.");
                }
            }
        }
        else
        {
            // 룰렛 실패시 처리
            roulette.isAttackSuccessful = false;
            Debug.Log("스턴 스킬 실패!");
            roulette.InvokeInitRoulette();
        }
    }
}
