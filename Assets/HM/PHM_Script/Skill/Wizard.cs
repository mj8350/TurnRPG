using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public CharSkillManager skillsManager;
    public ClickEvent click;

    void Start()
    {
        // �������� ��ų �Ŵ��� ����
        skillsManager = gameObject.AddComponent<CharSkillManager>();

        // �������� ��ų ����
        skillsManager.primarySkill = gameObject.AddComponent<WideSkill>();
        skillsManager.secondarySkill = gameObject.AddComponent<TargetSkill>();

        //// �ʿ��� ��� ��ų Ȱ��ȭ
        //skillsManager.ActivatePrimarySkill();
        //skillsManager.ActivateSecondarySkill();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) 
        {
            skillsManager.ActivatePrimarySkill();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            skillsManager.ActivateSecondarySkill();
        }
    }

    public void ActivePrimary()
    {
        skillsManager.ActivatePrimarySkill();
    }

    public void ActiveSecondary()
    {
        skillsManager.ActivateSecondarySkill();
    }

}
