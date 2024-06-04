using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSkillManager : MonoBehaviour
{
    // 캐릭터가 사용할 스킬들
    public BaseSkill primarySkill;
    public BaseSkill secondarySkill;

    // 캐릭터의 스킬을 초기화하는 함수
    public void InitializeSkills(BaseSkill primary, BaseSkill secondary)
    {
        primarySkill = primary;
        secondarySkill = secondary;
    }

    // 주 스킬을 활성화하는 함수
    public void ActivatePrimarySkill()
    {
        if (primarySkill != null)
        {
            primarySkill.Skill_Active();
        }
    }

    // 보조 스킬을 활성화하는 함수
    public void ActivateSecondarySkill()
    {
        if (secondarySkill != null)
        {
            secondarySkill.Skill_Active();
        }
    }
}
