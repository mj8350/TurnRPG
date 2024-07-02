using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveUIManager : MonoBehaviour
{
    private Move_Player player;


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

    public Image motelImg;
    public Image motelQuest;
    public Image motelShop;

    public Image Quest;

    public Image Inventory;

    public Image Stat;

    public Image RoundEnd;
    public TextMeshProUGUI RoundEndText;

    public TextMeshProUGUI R;

    private void Awake()
    {
        player = GameObject.FindFirstObjectByType<Move_Player>();

        RText();

        EXPChange();
        HPChange();
        ProfileChange();
    }

    public void RText()
    {
        if (GameManager.Instance.movePoint == 0)
            R.gameObject.SetActive(true);
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

    public void RoundEND()
    {
        RoundEnd.gameObject.SetActive(true);
        StartCoroutine("end");
    }

    private IEnumerator end()
    {
        RoundEndText.text = "주사위를 모두 소진하여 라운드가 끝났습니다.";
        yield return new WaitForSeconds(2f);
        RoundEndText.text = $"몬스터의 레벨이 올라갑니다. ( {GameManager.Instance.MonsterLevel} )";
        yield return new WaitForSeconds(2f);
        RoundEndText.text = "잠시 후 주사위가 재충전 됩니다.";
        yield return new WaitForSeconds(2f);
        RoundEnd.gameObject.SetActive(false);
    }

    public void OnClick_MotelExit()
    {
        motelImg.gameObject.SetActive(false);
        player.CanMove = true;
    }

    public void OnClick_MotelHeal()
    {
        for(int i = 0; i<3; i++)
        {
            GameManager.Instance.player[i].CurHP = GameManager.Instance.player[i].MaxHP;
        }
        HPChange();
    }

    public void OnClick_MotelShop()
    {
        motelShop.gameObject.SetActive(true);
    }

    public void OnClick_MotelShopExit()
    {
        motelShop.gameObject.SetActive(false);
    }

    public void OnClick_MotelQuest()
    {
        motelQuest.gameObject.SetActive(true);
    }

    public void OnClick_MotelQuestExit()
    {
        motelQuest.gameObject.SetActive(false);
    }

    public void OnClick_Quest()
    {
        Quest.gameObject.SetActive(true);
    }

    public void OnClick_QuestExit()
    {
        Quest.gameObject.SetActive(false);
    }

    public void OnClick_Inven()
    {
        Inventory.gameObject.SetActive(true);
    }

    public void OnClick_InvenExit()
    {
        Inventory.gameObject.SetActive(false);
    }

    public void OnClick_Stat()
    {
        Stat.gameObject.SetActive(true);
    }

    public void OnClick_StatExit()
    {
        Stat.gameObject.SetActive(false);
    }
}
