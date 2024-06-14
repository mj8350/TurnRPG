using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;
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
            Debug.Log("타겟스킬 발동");
            roulette.isAttackSuccessful = true;
            roulette.InvokeInitRoulette();
            gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
            motion.PlayerStartAttack();
            targetObject = click.selectedObj;
            if (targetObject != null)
            {
                PHM_CharStat stat = GetComponent<PHM_CharStat>();
                PHM_MonsterStat monStat = GetComponent<PHM_MonsterStat>();
                if (stat != null)
                {
                    // 캐릭터의 공격력을 기반으로 데미지 계산
                    int damage = stat.Strength;
                    FightManager.Instance.Damage(targetObject, damage, onCri);
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
}
