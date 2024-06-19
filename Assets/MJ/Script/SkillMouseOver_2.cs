using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillMouseOver_2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image SKInfo;
    public TextMeshProUGUI SKInfoText;
    public string InfoT;
    public int Accuracy;
    private int Critical;
    private string WhatDmg;
    private int damage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (FightManager.Instance.TurnQueue.Peek() < 3)
        {
            Accuracy = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Accuracy;
            Critical = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Critical;

            switch (GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].id)
            {
                case 0:
                    InfoT = "����� �������� �� ���� ����";
                    Accuracy = 25 + (Accuracy * 2);
                    WhatDmg = "���ݷ�";
                    damage = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Strength / 2;
                    break;
                case 1:
                    InfoT = "���� Ȯ���� ���� ���(�÷��̾�)�� ��Ȱ";
                    Accuracy = 10 + (Accuracy);
                    WhatDmg = "������";
                    damage = 0;
                    break;
                case 2:
                    InfoT = "����� ������ �����ϴ� ����";
                    Accuracy = 10 + (Accuracy * 3);
                    WhatDmg = "���ݷ�";
                    damage = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Strength * 2;
                    break;
                case 3:
                    InfoT = "������ ��ŭ ���� ��� ����";
                    Accuracy = 10 + (Accuracy * 3);
                    WhatDmg = "������";
                    damage = (int)(GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Magic * 1.5f);
                    break;
                case 4:
                    InfoT = "���ݷ� ��ŭ ���� ����";
                    Accuracy = 10 + (Accuracy * 3);
                    WhatDmg = "���ݷ�";
                    damage = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Strength;
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
