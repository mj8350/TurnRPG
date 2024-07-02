using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveUIManager : MonoBehaviour
{
    public TextMeshProUGUI DiceText;
    public TextMeshProUGUI PointText;

    public TextMeshProUGUI[] LevelUI;
    public TextMeshProUGUI[] PlayerName;
    public TextMeshProUGUI[] HPText;
    public TextMeshProUGUI[] EXPText;

    public Image[] Profile;
    public Slider[] HPSlider;
    public Slider[] EXPSlider;

    public Sprite[] profileSP;

    private void Awake()
    {
        EXPChange();
        HPChange();
        ProfileChange();
    }


    public void D_TextChange()
    {
        DiceText.text = GameManager.Instance.Dice.ToString();
    }

    public void P_TextChange()
    {
        PointText.text = GameManager.Instance.movePoint.ToString();
    }

    public void EXPChange()
    {
        int MaxEXP, CurEXP;
        for (int i = 0; i < 3; i++)
        {
            if (GameManager.Instance.PlayerLevel < 10)
            {
                MaxEXP = GameManager.Instance.MaxEXP[GameManager.Instance.PlayerLevel - 1];
                CurEXP = GameManager.Instance.PlayerExp;
                EXPSlider[i].value = ((float)CurEXP / MaxEXP);
                EXPText[i].text = $"{CurEXP}/{MaxEXP}";
                LevelUI[i].text = GameManager.Instance.PlayerLevel.ToString();
            }
        }
    }
    public void HPChange()
    {
        int Maxhp, Curhp;
        for (int i = 0; i < 3; i++)
        {
            Maxhp = GameManager.Instance.player[i].MaxHP;
            Curhp = GameManager.Instance.player[i].CurHP;
            HPSlider[i].value = ((float)Curhp / Maxhp);
            HPText[i].text = $"{Curhp}/{Maxhp}";
        }
    }

    public void ProfileChange()
    {
        for(int i = 0;i < 3; i++)
        {
            Profile[i].sprite = profileSP[GameManager.Instance.player[i].id];
            PlayerName[i].text = GameManager.Instance.player[i].CharName;
        }
    }

}
