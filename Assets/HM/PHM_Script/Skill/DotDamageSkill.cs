using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDamageSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;
    private int dotDamage = 5; // ��Ʈ ������

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
    }

    public override void Skill_Active()
    {
        Debug.Log("��Ʈ������ ��ų �ߵ�");
        gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
        motion.PlayerStartAttack();
        targetObject = click.selectedObj;
        if (targetObject != null)
        {
            // ���Ϳ��� ��ų�� ����� ��쿡�� ��Ʈ ������ ���� ����
            if (targetObject.CompareTag("Monster"))
            {
                // ���Ϳ��� ��Ʈ ������ ���� ����
                ApplyDotDamageState(targetObject);
            }
            else
            {
                GameManager.Instance.Damage(targetObject, dotDamage);
            }
        }
        else
        {
            Debug.LogError("Ÿ�� ������Ʈ�� �����ϴ�.");
        }
    }

    // ���Ϳ��� ��Ʈ ������ ���¸� �����ϴ� �Լ�
    private void ApplyDotDamageState(GameObject monster)
    {
        // �� ���ÿ����� ���Ϳ��� Ư���� ���¸� �ο��ϴ� ������ ����
        //monster.GetComponent<MonsterController>().ApplyDotDamageState();
    }
}
