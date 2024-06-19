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
    //private int Strength;
    //private int Magic;
    private int Critical;
    private string WhatDmg;
    private int damage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (FightManager.Instance.TurnQueue.Peek() < 3)
        {
            Accuracy = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Accuracy;
            //Strength = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Strength;
            //Magic = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Magic;
            Critical = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Critical;
            /*if (FightManager.Instance.big(Strength, Magic) == Strength)
            {
                damage = Strength;
                WhatDmg = "����������";
            }
            else
            {
                damage = Magic;
                WhatDmg = "����������";
            }*/

            switch(GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].id)
            {
                case 0:
                    InfoT = "";
                    Accuracy = 34 + (Accuracy * 4);
                    WhatDmg = "���ݷ�";
                    damage = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Strength;
                    break;
                case 1:
                    InfoT = "";
                    Accuracy = 34 + (Accuracy * 4);
                    WhatDmg = "������";
                    damage = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Magic;
                    break;
                case 2:
                    InfoT = "";
                    Accuracy = 34 + (Accuracy * 4);
                    WhatDmg = "����������";
                    damage = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Strength;
                    break;
                case 3:
                    InfoT = "";
                    Accuracy = 34 + (Accuracy * 4);
                    WhatDmg = "����������";
                    damage = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Strength;
                    break;
                case 4:
                    InfoT = "";
                    Accuracy = 34 + (Accuracy * 4);
                    WhatDmg = "����������";
                    damage = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Strength;
                    break;
            }


            SKInfo.gameObject.SetActive(true);

            Critical = 10 + (Critical * 2);

            SKInfoText.text = $"{InfoT} \n\n" +
                $"Ȯ��: {Accuracy}%, \n" +
                $"{WhatDmg}: {damage}\n" +
                $"ġ��Ÿ��: {Critical}%";
            FightManager.Instance.percent = Accuracy;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SKInfo.gameObject.SetActive(false);
    }
}
