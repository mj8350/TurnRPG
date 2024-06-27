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
            // 마법사의 스킬 매니저 설정
            skillsManager = gameObject.AddComponent<CharSkillManager>();

            // 마법사의 스킬 설정
            skillsManager.primarySkill = gameObject.AddComponent<WizardWideSkill>();
            skillsManager.secondarySkill = gameObject.AddComponent<WizardTargetSkill>();

            //// 필요한 경우 스킬 활성화
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
