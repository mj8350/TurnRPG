using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardTargetSkill : BaseSkill
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
            // 3%의 확률로 99999의 데미지를 입힘
            if (Random.Range(1, 101) <= 3)
            {
                Debug.Log("고유능력 발동!");
                roulette.isAttackSuccessful = true;
                roulette.InvokeInitRoulette();
                gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
                motion.PlayerStartAttack();
                targetObject = click.selectedObj;
                if (targetObject != null)
                {
                    // 99999의 데미지를 대상에게 입힘
                    FightManager.Instance.Damage(targetObject, 99999, onCri);
                }
                else
                {
                    Debug.LogError("타겟 오브젝트가 없습니다.");
                }
            }
            else
            {
                // 나머지 경우에는 기존의 로직대로 실행
                Debug.Log("고압절단 발동");
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
                    int damage = FightManager.Instance.DamageSum((int)(GameManager.Instance.player[number].Magic * 1.5f), GameManager.Instance.player[number].Critical, monStat.M_Defense, out onCri);
                    FightManager.Instance.Damage(targetObject, damage, onCri);

                }
                else
                {
                    Debug.LogError("타겟 오브젝트가 없습니다.");
                }
            }
        }
        else
        {
            roulette.isAttackSuccessful = false;
            Debug.Log("고압절단 실패!");
            StartCoroutine(DamageT(gameObject));
            roulette.InvokeInitRoulette();
        }

    }
}
