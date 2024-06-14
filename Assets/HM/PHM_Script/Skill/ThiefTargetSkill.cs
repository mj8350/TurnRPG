using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefTargetSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;
    public Roulette roulette;
    private int successProbability = 100;

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
        roulette = GameObject.FindFirstObjectByType<Roulette>();
    }


    public override void Skill_Active()
    {
        if (roulette.randomNumber < successProbability)
        {
            Debug.Log("영혼참 발동");
            roulette.isAttackSuccessful = true;
            roulette.InvokeInitRoulette();
            gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
            motion.PlayerStartAttack();
            targetObject = click.selectedObj;
            if (targetObject != null)
            {
                PHM_CharStat stat = GetComponent<PHM_CharStat>();
                click.selectedObj.TryGetComponent<PHM_MonsterStat>(out PHM_MonsterStat monStat);
                if (stat != null)
                {
                    // 캐릭터의 공격력을 기반으로 데미지 계산
                    int number = FightManager.Instance.GMChar(gameObject);
                    int damage = FightManager.Instance.DamageSum((int)(GameManager.Instance.player[number].Strength * 2f), GameManager.Instance.player[number].Critical, 0, out onCri);
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
