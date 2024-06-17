using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDamageSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;
    private int dotDamage = 5; // 도트 데미지
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
            Debug.Log("도트뎀 스킬 발동");

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
                targetObject.TryGetComponent<MonsterAttack>(out MonsterAttack dotDamgeTarget);
                if (dotDamgeTarget != null)
                {
                    dotDamgeTarget.SetDotDamage(); // 스턴된 몬스터의 타겟 설정
                }
                else
                {
                    Debug.LogError("스킬 대상이 올바르지 않습니다.");
                }

                PHM_CharStat stat = GetComponent<PHM_CharStat>();
                click.selectedObj.TryGetComponent<PHM_MonsterStat>(out PHM_MonsterStat monStat);
                if (stat != null)
                {
                    int damage = stat.Strength / 2;
                    FightManager.Instance.Damage(targetObject, damage, onCri); // todo: 애니메이션 재생 필요
                }
            }
        }
        else
        {
            // 룰렛 실패시 처리
            roulette.isAttackSuccessful = false;
            Debug.Log("스킬 실패!");
            roulette.InvokeInitRoulette();
        }
    }
}
