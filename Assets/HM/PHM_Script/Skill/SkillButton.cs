using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public Button button;
    public Button button2;
    public CharSkillManager skill; // 스킬을 가진 캐릭터 클래스

    void Start()
    {
        // 버튼에 클릭 이벤트 리스너 추가
        button.onClick.AddListener(PrimarySkill);
        button2.onClick.AddListener(SecondarySkill);
    }

    void PrimarySkill()
    {
        // 캐릭터의 스킬 함수 호출
        skill.ActivatePrimarySkill();
    }
    void SecondarySkill()
    {
        // 캐릭터의 스킬 함수 호출
        skill.ActivateSecondarySkill();
    }
}
