using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    public CharSkillManager skillsManager;

    void Start()
    {
        // ��ų �Ŵ��� ����
        skillsManager = gameObject.AddComponent<CharSkillManager>();

        // ��ų ����
        skillsManager.primarySkill = gameObject.AddComponent<DotDamageSkill>();
        skillsManager.secondarySkill = gameObject.AddComponent<TargetSkill>();

        //// �ʿ��� ��� ��ų Ȱ��ȭ
        //skillsManager.ActivatePrimarySkill();
        //skillsManager.ActivateSecondarySkill();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            skillsManager.ActivatePrimarySkill();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            skillsManager.ActivateSecondarySkill();
        }
    }
}
