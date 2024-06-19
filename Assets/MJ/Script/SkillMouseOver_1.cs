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
                    InfoT = "대상의 한 턴을 자신만 공격하게 함";
                    Accuracy = 34 + (Accuracy * 4);
                    WhatDmg = "공격력";
                    damage = 0;
                    break;
                case 1:
                    InfoT = "마법력 만큼 지정 대상 회복";
                    Accuracy = 10 + (Accuracy * 4);
                    WhatDmg = "마법력";
                    damage = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Magic / 2;
                    break;
                case 2:
                    InfoT = "지정 대상에게 매 턴마다 데미지가 증가하는 맹독 부여";
                    Accuracy = 10 + (Accuracy * 2);
                    WhatDmg = "공격력";
                    damage = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Strength / 2;
                    break;
                case 3:
                    InfoT = "마법력 만큼 광역 공격";
                    Accuracy = 20 + (Accuracy * 3);
                    WhatDmg = "마법력";
                    damage = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Magic;
                    break;
                case 4:
                    InfoT = "지정 대상에게 매 턴마다 데미지를 주는 화상을 입힘";
                    Accuracy = 10 + (Accuracy * 2);
                    WhatDmg = "공격력";
                    damage = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Strength / 2;
                    break;
            }

            FightManager.Instance.percent = Accuracy;
            Critical = 10 + (Critical * 2);

            SKInfo.gameObject.SetActive(true);


            SKInfoText.text = $"{InfoT} \n\n" +
                $"확률: {Accuracy}%, \n" +
                $"{WhatDmg}: {damage}\n" +
                $"치명타율: {Critical}%";
            
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SKInfo.gameObject.SetActive(false);
    }
}
