using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public CharSkillManager skillsManager;

    void Start()
    {
        // ��ų �Ŵ��� ����
        skillsManager = gameObject.AddComponent<CharSkillManager>();

        // ��ų ����
        skillsManager.primarySkill = gameObject.AddComponent<ArcherDotDamageSkill>();
        skillsManager.secondarySkill = gameObject.AddComponent<ArcherWideSkill>();

        //// �ʿ��� ��� ��ų Ȱ��ȭ
        //skillsManager.ActivatePrimarySkill();
        //skillsManager.ActivateSecondarySkill();
    }

    
}
