using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public CharSkillManager skillsManager;

    void Start()
    {
        // 스킬 매니저 설정
        skillsManager = gameObject.AddComponent<CharSkillManager>();

        // 스킬 설정
        skillsManager.primarySkill = gameObject.AddComponent<TargetSkill>();
        skillsManager.secondarySkill = gameObject.AddComponent<WideSkill>();

        //// 필요한 경우 스킬 활성화
        //skillsManager.ActivatePrimarySkill();
        //skillsManager.ActivateSecondarySkill();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            skillsManager.ActivatePrimarySkill();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            skillsManager.ActivateSecondarySkill();
        }
    }
}
