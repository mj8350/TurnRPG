using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : MonoBehaviour
{
    public CharSkillManager skillsManager;

    void Start()
    {
        // 스킬 매니저 설정
        skillsManager = gameObject.AddComponent<CharSkillManager>();

        // 스킬 설정
        skillsManager.primarySkill = gameObject.AddComponent<PriestSkill01>();
        skillsManager.secondarySkill = gameObject.AddComponent<PriestSkill02>();

        //// 필요한 경우 스킬 활성화
        //skillsManager.ActivatePrimarySkill();
        //skillsManager.ActivateSecondarySkill();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            skillsManager.ActivatePrimarySkill();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            skillsManager.ActivateSecondarySkill();
        }
    }
}
