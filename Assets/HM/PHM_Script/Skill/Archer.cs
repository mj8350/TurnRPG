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
        skillsManager.primarySkill = gameObject.AddComponent<TargetSkill>();
        skillsManager.secondarySkill = gameObject.AddComponent<WideSkill>();

        //// �ʿ��� ��� ��ų Ȱ��ȭ
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
