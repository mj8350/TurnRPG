using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectionSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;

    private void Awake()
    {
        click = gameObject.AddComponent<ClickEvent>();
    }

    public override void Skill_Active()
    {
        Debug.Log("��Ȱ ��ų �ߵ�");

        // ��Ȱ ����� ���� �����ϰų�, Ư�� ���ǿ� ���� ��Ȱ ����� ������ �� �ֽ��ϴ�.
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
        // ��Ȱ ����� ��� ���¸� ȸ���մϴ�.
        PHM_CharStat healthComponent = targetObject.GetComponent<PHM_CharStat>();
        if (healthComponent != null)
        {
            // ��Ȱ ����� ü���� �ʱ�ȭ�ϰų� ������ ȸ���� �� �ֽ��ϴ�.
            int resurrectionAmount = 50; // ���÷� 50% ȸ���� ����
            healthComponent.Helth += resurrectionAmount;
            Debug.Log("��Ȱ ����� ��Ȱ��ŵ�ϴ�.");
        }
        else
        {
            Debug.LogError("��Ȱ ����� ü���� ȸ����ų �� �����ϴ�. ��󿡰� ü�� ������Ʈ�� �����ϴ�.");
        }
    }
}
