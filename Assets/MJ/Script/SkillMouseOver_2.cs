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
                    InfoT = "대상의 기절시켜 한 턴을 제압";
                    Accuracy = 25 + (Accuracy * 2);
                    WhatDmg = "공격력";
                    damage = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Strength / 2;
                    break;
                case 1:
                    InfoT = "낮은 확률로 지정 대상(플레이어)을 부활";
                    Accuracy = 10 + (Accuracy);
                    WhatDmg = "마법력";
                    damage = 0;
                    break;
                case 2:
                    InfoT = "대상의 방어력을 무시하는 공격";
                    Accuracy = 10 + (Accuracy * 3);
                    WhatDmg = "공격력";
                    damage = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Strength * 2;
                    break;
                case 3:
                    InfoT = "마법력 만큼 지정 대상 공격";
                    Accuracy = 10 + (Accuracy * 3);
                    WhatDmg = "마법력";
                    damage = (int)(GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Magic * 1.5f);
                    break;
                case 4:
                    InfoT = "공격력 만큼 광역 공격";
                    Accuracy = 10 + (Accuracy * 3);
                    WhatDmg = "공격력";
                    damage = GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Strength;
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
