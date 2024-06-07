using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;

    private void Awake()
    {
        click = gameObject.AddComponent<ClickEvent>();
    }

    public override void Skill_Active()
    {
        Debug.Log("Ÿ�ٽ�ų �ߵ�");
        targetObject = click.selectedObj;
        if (targetObject != null)
        {
            // ������ ����� ü���� ȸ����ŵ�ϴ�.
            HealTarget();
        }
        else
        {
            Debug.LogError("Ÿ�� ������Ʈ�� �����ϴ�.");
        }
    }

    private void HealTarget()
    {
        // ������ ��󿡰Լ� ü�� ������Ʈ�� ã���ϴ�.
        PHM_CharStat healthComponent = targetObject.GetComponent<PHM_CharStat>();
        if (healthComponent != null)
        {
            // ü���� ȸ���մϴ�.
            int healingAmount = 20; // ȸ���� (���÷� 20 ����)
            healthComponent.Helth += healingAmount;
            Debug.Log("����� ü���� ȸ���մϴ�.");
            Debug.Log(healthComponent.Helth);
        }
        else
        {
            Debug.LogError("��󿡰� ü���� ȸ����ų �� �����ϴ�. ��󿡰� ü�� ������Ʈ�� �����ϴ�.");
        }
    }
}
