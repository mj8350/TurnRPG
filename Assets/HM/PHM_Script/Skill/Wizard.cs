using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public CharSkillManager skillsManager;
    public ClickEvent click;

    void Start()
    {
        if (GameManager.Instance.sceneState == SceneState.BattleScene)
        {
            // �������� ��ų �Ŵ��� ����
            skillsManager = gameObject.AddComponent<CharSkillManager>();

            // �������� ��ų ����
            skillsManager.primarySkill = gameObject.AddComponent<WizardWideSkill>();
            skillsManager.secondarySkill = gameObject.AddComponent<WizardTargetSkill>();

            //// �ʿ��� ��� ��ų Ȱ��ȭ
            //skillsManager.ActivatePrimarySkill();
            //skillsManager.ActivateSecondarySkill();
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
