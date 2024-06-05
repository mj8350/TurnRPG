using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public CharSkillManager skillsManager;
    public ClickEvent click;

    void Start()
    {
        // 마법사의 스킬 매니저 설정
        skillsManager = gameObject.AddComponent<CharSkillManager>();
        click = gameObject.AddComponent<ClickEvent>();

        // 마법사의 스킬 설정
        skillsManager.primarySkill = gameObject.AddComponent<TargetSkill>();
        skillsManager.secondarySkill = gameObject.AddComponent<WideSkill>();

        //// 필요한 경우 스킬 활성화
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
