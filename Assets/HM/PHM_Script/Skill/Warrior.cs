using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    public CharSkillManager skillsManager;

    void Start()
    {
        // 스킬 매니저 설정
        skillsManager = gameObject.AddComponent<CharSkillManager>();

        // 스킬 설정
        skillsManager.primarySkill = gameObject.AddComponent<WarriorSkill01>();
        skillsManager.secondarySkill = gameObject.AddComponent<WarriorSkill02>();

        //// 필요한 경우 스킬 활성화
        //skillsManager.ActivatePrimarySkill();
        //skillsManager.ActivateSecondarySkill();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            skillsManager.ActivatePrimarySkill();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            skillsManager.ActivateSecondarySkill();
        }
    }
}
