using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    public CharSkillManager skillsManager;

    void Start()
    {
        if (GameManager.Instance.sceneState == SceneState.BattleScene)
        {
            // ��ų �Ŵ��� ����
            skillsManager = gameObject.AddComponent<CharSkillManager>();

            // ��ų ����
            skillsManager.primarySkill = gameObject.AddComponent<DotDamageSkill>();
            skillsManager.secondarySkill = gameObject.AddComponent<ThiefTargetSkill>();

            //// �ʿ��� ��� ��ų Ȱ��ȭ
            //skillsManager.ActivatePrimarySkill();
            //skillsManager.ActivateSecondarySkill();
        }
    }
}
