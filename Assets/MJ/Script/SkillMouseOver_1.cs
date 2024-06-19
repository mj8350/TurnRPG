using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillMouseOver_1 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image SKInfo;
    public TextMeshProUGUI SKInfoText;
    private string InfoT;
    private int Accuracy;
    private int Critical;
    private string WhatDmg;
    private int damage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (FightManager.Instance.TurnQueue.Peek() < 3)
        {
            Accuracy = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Accuracy;
            Critical = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Critical;

            switch(GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].id)
            {
                case 0:
                    InfoT = "����� �� ���� �ڽŸ� �����ϰ� ��";
                    Accuracy = 34 + (Accuracy * 4);
                    WhatDmg = "���ݷ�";
                    damage = 0;
                    break;
                case 1:
                    InfoT = "������ ��ŭ ���� ��� ȸ��";
                    Accuracy = 10 + (Accuracy * 4);
                    WhatDmg = "������";
                    damage = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Magic / 2;
                    break;
                case 2:
                    InfoT = "���� ��󿡰� �� �ϸ��� �������� �����ϴ� �͵� �ο�";
                    Accuracy = 10 + (Accuracy * 2);
                    WhatDmg = "���ݷ�";
                    damage = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Strength / 2;
                    break;
                case 3:
                    InfoT = "������ ��ŭ ���� ����";
                    Accuracy = 20 + (Accuracy * 3);
                    WhatDmg = "������";
                    damage = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Magic;
                    break;
                case 4:
                    InfoT = "���� ��󿡰� �� �ϸ��� �������� �ִ� ȭ���� ����";
                    Accuracy = 10 + (Accuracy * 2);
                    WhatDmg = "���ݷ�";
                    damage = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Strength / 2;
                    break;
            }

            FightManager.Instance.percent = Accuracy;
            Critical = 10 + (Critical * 2);

            SKInfo.gameObject.SetActive(true);


            SKInfoText.text = $"{InfoT} \n\n" +
                $"Ȯ��: {Accuracy}%, \n" +
                $"{WhatDmg}: {damage}\n" +
                $"ġ��Ÿ��: {Critical}%";
            
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SKInfo.gameObject.SetActive(false);
    }
}
