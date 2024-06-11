using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;
    private float wideHealProbability = 0.1f; // ���� �� �ߵ� Ȯ��

    private void Awake()
    {
        click = GameObject.FindFirstObjectByType<ClickEvent>();
    }

    public override void Skill_Active()
    {
        Debug.Log("�� ��ų �ߵ�");

        // Ȯ���� ���� ���� ���� �������� ����
        if (Random.value < wideHealProbability)
        {
            Debug.Log("���� �� �ߵ�");
            gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
            motion.Victory();
            ApplyWideHeal();
        }
        else
        {
            Debug.Log("���� �� �ߵ�");
            gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
            motion.Victory();
            ApplySingleHeal();
        }
    }

    // ���� ���� �����ϴ� �Լ�
    private void ApplyWideHeal()
    {
        // ���� ��ġ���� �ݰ� ���� �ִ� ��� player �±׸� ���� ������Ʈ���� ���� ����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5f); // �ݰ� 5�� ���� ���� ��� �ݶ��̴� ��������

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                ApplyHeal(collider.gameObject);
            }
        }
    }

    // ���� ���� �����ϴ� �Լ�
    private void ApplySingleHeal()
    {
        targetObject = click.selectedObj;
        if (targetObject != null && targetObject.CompareTag("Player"))
        {
            ApplyHeal(targetObject);
        }
        else
        {
            Debug.LogError("�� ��� ������Ʈ�� ���ų� player �±׸� ������ ���� �ʽ��ϴ�.");
        }
    }

    // ���� �����ϴ� �Լ�
    private void ApplyHeal(GameObject target)
    {
        // ������ ��󿡰Լ� ü�� ������Ʈ�� ã���ϴ�.
        PHM_CharStat healthComponent = target.GetComponent<PHM_CharStat>();
        if (healthComponent != null)
        {
            // ü���� ȸ��
            int healingAmount = 5; // ȸ���� (���÷� 5 ����)
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
