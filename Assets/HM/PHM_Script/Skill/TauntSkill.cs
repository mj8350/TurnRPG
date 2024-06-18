using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;
    private List<GameObject> tauntTargets;
    public Roulette roulette;
    private int successProbability = 100;

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
        roulette = GameObject.FindFirstObjectByType<Roulette>();
        TryGetComponent<PHM_CharStat>(out stat);
        successProbability = 34 + (stat.Accuracy * 4);
        tauntTargets = new List<GameObject>();
    }

    public override void Skill_Active()
    {
        if (roulette.randomNumber <= 100)
        {
            Debug.Log("도발스킬 발동");
            roulette.isAttackSuccessful = true;
            roulette.InvokeInitRoulette();
            gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
            motion.PlayerStartAttack();
            targetObject = click.selectedObj;
            if (targetObject != null)
            {
                targetObject.TryGetComponent<MonsterAttack>(out MonsterAttack tauntTargetScript);
                if (tauntTargetScript != null)
                {
                    tauntTargetScript.SetTauntTarget(this.gameObject); // 도발된 몬스터의 타겟 설정
                }
                else
                {
                    Debug.LogError("도발 대상이 올바른 스크립트를 가지고 있지 않습니다.");
                }
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
