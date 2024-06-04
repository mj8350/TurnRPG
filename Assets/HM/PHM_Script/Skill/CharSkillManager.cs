using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSkillManager : MonoBehaviour
{
    // ĳ���Ͱ� ����� ��ų��
    public BaseSkill primarySkill;
    public BaseSkill secondarySkill;

    // ĳ������ ��ų�� �ʱ�ȭ�ϴ� �Լ�
    public void InitializeSkills(BaseSkill primary, BaseSkill secondary)
    {
        primarySkill = primary;
        secondarySkill = secondary;
    }

    // �� ��ų�� Ȱ��ȭ�ϴ� �Լ�
    public void ActivatePrimarySkill()
    {
        if (primarySkill != null)
        {
            primarySkill.Skill_Active();
        }
    }

    // ���� ��ų�� Ȱ��ȭ�ϴ� �Լ�
    public void ActivateSecondarySkill()
    {
        if (secondarySkill != null)
        {
            secondarySkill.Skill_Active();
        }
    }
}
