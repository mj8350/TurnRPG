using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefTargetSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;
    public Roulette roulette;
    private int successProbability;

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
        roulette = GameObject.FindFirstObjectByType<Roulette>();
        //successProbability = 10 + (stat.Accuracy * 3);
        successProbability = 10 + ((GameManager.Instance.player[FightManager.Instance.GMChar(gameObject)].Accuracy * 3));
    }


    public override void Skill_Active()
    {
        if (roulette.randomNumber <= successProbability)
        {
            Debug.Log("영혼참 발동");
            roulette.isAttackSuccessful = true;
            roulette.InvokeInitRoulette();
            gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
            motion.PlayerStartAttack();
            targetObject = click.selectedObj;
            if (targetObject != null)
            {
                click.selectedObj.TryGetComponent<PHM_MonsterStat>(out PHM_MonsterStat monStat);
                // 캐릭터의 공격력을 기반으로 데미지 계산
                int number = FightManager.Instance.GMChar(gameObject);
                int damage = FightManager.Instance.DamageSum((int)(GameManager.Instance.player[number].Strength * 2f), GameManager.Instance.player[number].Critical, 0, out onCri);
                FightManager.Instance.Damage(targetObject, damage, onCri);

            }
            else
            {
                Debug.LogError("타겟 오브젝트가 없습니다.");
            }

        }
        else
        {
            roulette.isAttackSuccessful = false;
            Debug.Log("영혼참 실패!");
            StartCoroutine(DamageT(gameObject));
            roulette.InvokeInitRoulette();
        }

    }
}
