using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public Button button;
    public Button button2;
    public CharSkillManager skill; // ��ų�� ���� ĳ���� Ŭ����
    public Wizard wizard;
    public Roulette roulette;
    public Image rouletteImage;

    void Start()
    {
        wizard = FindFirstObjectByType<Wizard>();
        roulette = FindFirstObjectByType<Roulette>();

        // ��ư�� Ŭ�� �̺�Ʈ ������ �߰�
        button.onClick.AddListener(PrimarySkill);
        button2.onClick.AddListener(SecondarySkill);
    }

    public void PrimarySkill()
    {
        wizard.ActivePrimary();
    }

    public void SecondarySkill()
    {
        // ĳ������ ��ų �Լ� ȣ��
        skill.ActivateSecondarySkill();
        wizard.ActiveSecondary();
    }
}
