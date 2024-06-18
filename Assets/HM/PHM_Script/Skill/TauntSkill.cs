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
            Debug.Log("���߽�ų �ߵ�");
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
                    tauntTargetScript.SetTauntTarget(this.gameObject); // ���ߵ� ������ Ÿ�� ����
                }
                else
                {
                    Debug.LogError("���� ����� �ùٸ� ��ũ��Ʈ�� ������ ���� �ʽ��ϴ�.");
                }
            }
        }
        else
        {
            roulette.isAttackSuccessful = false;
            Debug.Log("���� ����!");
            roulette.InvokeInitRoulette();
        }

    }
}
