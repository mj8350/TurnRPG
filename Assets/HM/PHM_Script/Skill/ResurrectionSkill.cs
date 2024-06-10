using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectionSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
    }

    public override void Skill_Active()
    {
        Debug.Log("��Ȱ ��ų �ߵ�");

        targetObject = click.selectedObj;

        if (targetObject != null)
        {
            ResurrectTarget();
        }
        else
        {
            Debug.LogError("��Ȱ ����� ã�� �� �����ϴ�.");
        }
    }

    private void ResurrectTarget()
    {
        PHM_CharStat healthComponent = targetObject.GetComponent<PHM_CharStat>();
        if (healthComponent != null)
        {
            int resurrectionAmount = 50; // ���÷� 50% ȸ���� ����
            healthComponent.Helth += resurrectionAmount;
            Debug.Log("��Ȱ��ŵ�ϴ�.");
        }
        else
        {
            Debug.LogError("��Ȱ ��ų �� �����ϴ�.");
        }
    }
}
