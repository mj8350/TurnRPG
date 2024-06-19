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
                WhatDmg = "물리데미지";
            }
            else
            {
                damage = Magic;
                WhatDmg = "마법데미지";
            }*/


            SKInfo.gameObject.SetActive(true);

            Accuracy = 15 + (Accuracy * 4);
            Critical = 10 + (Critical * 2);

            SKInfoText.text = $"{InfoT} \n\n" +
                $"확률: {Accuracy}%, \n" +
                $"{WhatDmg}: {damage}\n" +
                $"치명타율: {Critical}%";
            FightManager.Instance.percent = Accuracy;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SKInfo.gameObject.SetActive(false);
    }
}
