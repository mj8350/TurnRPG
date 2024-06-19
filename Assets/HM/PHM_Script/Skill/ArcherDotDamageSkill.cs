using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherDotDamageSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;
    public Roulette roulette;
    private int successProbability;

    public int damage;

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
        roulette = GameObject.FindFirstObjectByType<Roulette>();

        //successProbability = 10+ (stat.Accuracy * 2); // 15+(Ac*4)
        successProbability = 10 + ((GameManager.Instance.player[FightManager.Instance.GMChar(gameObject)].Accuracy * 2));
    }

    public override void Skill_Active()
    {
        if (roulette.randomNumber <= successProbability || roulette.randomNumber_2 <= 40)
        {
            Debug.Log("화염화살 발동");

            // 룰렛 성공 여부 설정
            roulette.isAttackSuccessful = true;
            roulette.InvokeInitRoulette();

            // 공격 모션 실행
            gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
            motion.PlayerStartAttack();

            // 도트뎀 대상 설정
            targetObject = click.selectedObj;
            if (targetObject != null)
            {
                targetObject.TryGetComponent<MonsterAttack>(out MonsterAttack dotDamgeTarget);
                if (dotDamgeTarget != null)
                {
                    dotDamgeTarget.SetDotDamage_2();
                }
                else
                {
                    Debug.LogError("스킬 대상이 올바르지 않습니다.");
                }

                click.selectedObj.TryGetComponent<PHM_MonsterStat>(out PHM_MonsterStat monStat);
                int number = FightManager.Instance.GMChar(gameObject);
                int damage = FightManager.Instance.DamageSum((int)(GameManager.Instance.player[number].Strength / 2f), GameManager.Instance.player[number].Critical, monStat.P_Defense, out onCri);
                FightManager.Instance.Damage(targetObject, damage, false); // 크리티컬이 뜨지 않도록 펄스
            }
        }
        else
        {
            // 룰렛 실패시 처리
            roulette.isAttackSuccessful = false;
            Debug.Log("화염화살 실패!");
            StartCoroutine(DamageT(gameObject));
            roulette.InvokeInitRoulette();
        }
        
    }
}
