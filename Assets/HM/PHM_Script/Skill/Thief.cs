using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    public CharSkillManager skillsManager;

    void Start()
    {
        // 스킬 매니저 설정
        skillsManager = gameObject.AddComponent<CharSkillManager>();

        // 스킬 설정
        skillsManager.primarySkill = gameObject.AddComponent<DotDamageSkill>();
        skillsManager.secondarySkill = gameObject.AddComponent<ThiefTargetSkill>();

        //// 필요한 경우 스킬 활성화
        //skillsManager.ActivatePrimarySkill();
        //skillsManager.ActivateSecondarySkill();
    }
}
