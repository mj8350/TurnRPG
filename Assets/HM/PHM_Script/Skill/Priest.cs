using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : MonoBehaviour
{
    public CharSkillManager skillsManager;

    void Start()
    {
        // ��ų �Ŵ��� ����
        skillsManager = gameObject.AddComponent<CharSkillManager>();

        // ��ų ����
        skillsManager.primarySkill = gameObject.AddComponent<PriestSkill01>();
        skillsManager.secondarySkill = gameObject.AddComponent<PriestSkill02>();

        //// �ʿ��� ��� ��ų Ȱ��ȭ
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
