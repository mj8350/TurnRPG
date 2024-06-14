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
        skillsManager.primarySkill = gameObject.AddComponent<ArcherDotDamageSkill>();
        skillsManager.secondarySkill = gameObject.AddComponent<ArcherWideSkill>();

        //// 필요한 경우 스킬 활성화
        //skillsManager.ActivatePrimarySkill();
        //skillsManager.ActivateSecondarySkill();
    }

    
}
