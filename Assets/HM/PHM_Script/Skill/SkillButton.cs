using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public Button button;
    public Button button2;
    public CharSkillManager skill; // ��ų�� ���� ĳ���� Ŭ����

    void Start()
    {
        // ��ư�� Ŭ�� �̺�Ʈ ������ �߰�
        button.onClick.AddListener(PrimarySkill);
        button2.onClick.AddListener(SecondarySkill);
    }

    void PrimarySkill()
    {
        // ĳ������ ��ų �Լ� ȣ��
        skill.ActivatePrimarySkill();
    }
    void SecondarySkill()
    {
        // ĳ������ ��ų �Լ� ȣ��
        skill.ActivateSecondarySkill();
    }
}
