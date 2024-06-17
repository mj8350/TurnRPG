using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : BaseSkill
{
    private ClickEvent click;
    private GameObject targetObject;
    public Roulette roulette;
    private float wideHealProbability = 0.1f; // ���� �� �ߵ� Ȯ��
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
            Debug.Log("�� ��ų �ߵ�");

            // Ȯ���� ���� ���� ���� �������� ����
            if (Random.value < wideHealProbability)
            {
                Debug.Log("���� �� �ߵ�");
                roulette.isAttackSuccessful = true;
                roulette.InvokeInitRoulette();
                gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
                motion.Victory();
                ApplyWideHeal();
            }
            else
            {
                Debug.Log("���� �� �ߵ�");
                roulette.isAttackSuccessful = true;
                roulette.InvokeInitRoulette();
                gameObject.TryGetComponent<AttackingExample>(out AttackingExample motion);
                motion.Victory();
                ApplySingleHeal();
            }
        }
        else
        {
            roulette.isAttackSuccessful = false;
            Debug.Log("�� ��ų ��� ����!");
            roulette.InvokeInitRoulette();
        }
    }

    // ���� ���� �����ϴ� �Լ�
    private void ApplyWideHeal()
    {
        // ���� ��ġ���� �ݰ� ���� �ִ� ��� player �±׸� ���� ������Ʈ���� ���� ����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5f); // �ݰ� 5�� ���� ���� ��� �ݶ��̴� ��������
        PHM_CharStat stat = GetComponent<PHM_CharStat>();
        int heal = stat.Magic / 2;
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                FightManager.Instance.Heal(collider.gameObject, heal);
            }
        }
    }

    // ���� ���� �����ϴ� �Լ�
    private void ApplySingleHeal()
    {
        targetObject = click.selectedObj;
        PHM_CharStat stat = GetComponent<PHM_CharStat>();
        if (targetObject != null && targetObject.CompareTag("Player"))
        {
            int heal = stat.Magic / 2;
            FightManager.Instance.Heal(targetObject, heal);
        }
        else
        {
            Debug.LogError("�� ��� ������Ʈ�� ���ų� player �±׸� ������ ���� �ʽ��ϴ�.");
        }
    }

    // ���� �����ϴ� �Լ�
    //private void ApplyHeal(GameObject target)
    //{
    //    // ������ ��󿡰Լ� ü�� ������Ʈ�� ã���ϴ�.
    //    PHM_CharStat healthComponent = target.GetComponent<PHM_CharStat>();
    //    if (healthComponent != null)
    //    {
    //        // ü���� ȸ��
    //        int healingAmount = 5; // ȸ���� (���÷� 5 ����)
    //        healthComponent.Helth += healingAmount;
    //        Debug.Log("����� ü���� ȸ���մϴ�.");
    //        Debug.Log(healthComponent.Helth);
    //    }
    //    else
    //    {
    //        Debug.LogError("��󿡰� ü���� ȸ����ų �� �����ϴ�. ��󿡰� ü�� ������Ʈ�� �����ϴ�.");
    //    }
    //}
}
