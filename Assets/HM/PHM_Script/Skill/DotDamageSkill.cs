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
    private int successProbability = 60;

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
        roulette = GameObject.FindFirstObjectByType<Roulette>();
    }

    public override void Skill_Active()
    {
        if(roulette.randomNumber < successProbability)
        {
            Debug.Log("도트데미지 스킬 발동");
            roulette.isAttackSuccessful = true;
            roulette.InvokeInitRoulette();
            gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
            motion.PlayerStartAttack();
            targetObject = click.selectedObj;
            if (targetObject != null)
            {
                // 몬스터에게 스킬을 사용한 경우에만 도트 데미지 상태 설정
                if (targetObject.CompareTag("Monster"))
                {
                    // 몬스터에게 도트 데미지 상태 설정
                    ApplyDotDamageState(targetObject);
                }
                else
                {
                    FightManager.Instance.Damage(targetObject, dotDamage);
                }
            }
            else
            {
                Debug.LogError("타겟 오브젝트가 없습니다.");
            }
        }
        else
        {
            roulette.isAttackSuccessful = false;
            Debug.Log("공격 실패!");
            roulette.InvokeInitRoulette();
        }
    }

    // 몬스터에게 도트 데미지 상태를 설정하는 함수
    private void ApplyDotDamageState(GameObject monster)
    {
        // 이 예시에서는 몬스터에게 특정한 상태를 부여하는 것으로 간주
        //monster.GetComponent<MonsterController>().ApplyDotDamageState();
    }
}
