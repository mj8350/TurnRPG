using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightUI : MonoBehaviour
{
    public Image[] caractor;
    public TextMeshProUGUI[] plyName;
    public Image[] TurnImg;
    public Sprite[] TurnSp;

    public TextMeshProUGUI PrimarySkill;
    public TextMeshProUGUI SecondarySkill;

    public Slider[] HPSlider;
    public Slider[] EXPSlider;

    public TextMeshProUGUI[] HPText;
    public TextMeshProUGUI[] EXPText;

    public GameObject[] MonsterUI;
    public Slider[] M_HPSlider;
    public TextMeshProUGUI[] M_HPText;
    public TextMeshProUGUI[] M_Level;
    public TextMeshProUGUI[] M_PD;
    public TextMeshProUGUI[] M_MD;


    private void Start()
    {
        for (int i = 0; i < MonsterUI.Length; i++)
        {
            //MonsterUI[i].SetActive(false);
        }
        EXPChange();
        MonsterUISetting();
    }

    public void ProfileUIChange(int num, string name, string PlayerName)
    {
        caractor[num].gameObject.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            if (TurnSp[i].name == name)
            {

                caractor[num].sprite = TurnSp[i];
                plyName[num].text = PlayerName;
                break;
            }
        }
    }


    public void TurnUIChange(int imgnum, string spname)
    {
        for (int i = 0; i < TurnSp.Length; i++)
        {
            if (TurnSp[i].name == spname)
            {
                TurnImg[imgnum].sprite = TurnSp[i];
                TurnImg[imgnum].gameObject.SetActive(true);
                break;
            }
        }
    }
    public int count = -1;
    public void TurnOut()
    {
        TurnImg[count].gameObject.SetActive(false);
    }

    public void DrawTurn()
    {
        if (FightManager.Instance.TurnQueue.Count == 0)
        {
            FightManager.Instance.NewTurn();
            NewTurn();
            count = -1;
            Debug.Log("���� ����");
        }

        int pos = FightManager.Instance.TurnQueue.Peek();
        if (pos < 3)
        {
            Debug.Log(FightManager.Instance.PlayerPos[pos].GetChild(0).gameObject.name); // ť���� ���� ���� �̸� Ȯ��
            SkillTextInfo(pos); // ť���� ���� ���ڸ� ���ڷ� �Ѱ���
        }
        else
        {
            Debug.Log("���� ������");
            //monsterAi.MonsterStart(pos-3);
            FightManager.Instance.MonsterTurn(pos - 3);
        }
        count++;
        Debug.Log("�� ��ο�");
        HPChange();
    }

    public void NewTurn()
    {
        for (int i = 0; i < FightManager.Instance.TurnQueue.Count; i++)
            TurnImg[i].gameObject.SetActive(true);
    }

    public void SkillTextInfo(int pos)
    {
        if (pos < 3)
        {

            Transform playerTransform = FightManager.Instance.PlayerPos[pos];
            PHM_CharStat playerStats = playerTransform.GetComponentInChildren<PHM_CharStat>();

            if (playerStats != null)
            {
                PrimarySkill.text = playerStats.GetPrimarySkill().ToString();
                SecondarySkill.text = playerStats.GetSecondarySkill().ToString();
            }
            else
            {
                Debug.LogError("PHM_CharStat ��������");
            }
        }
        else
        {
            PrimarySkill.text = "���� ��";
            SecondarySkill.text = "���� ��";
        }
    }


    public void HPChange()
    {
        int Maxhp, Curhp;
        for (int i = 0; i < 3; i++)
        {
            Maxhp = GameManager.Instance.player[i].MaxHP;
            Curhp = GameManager.Instance.player[i].CurHP;
            HPSlider[i].value = Curhp/Maxhp;
            HPText[i].text = $"{Curhp}/{Maxhp}";
        }
        for(int i = 0;i < 3;i++)
        {
            GameObject obj = FightManager.Instance.MonsterPos[i].GetChild(0).gameObject;
            if(obj.TryGetComponent<PHM_MonsterStat>(out PHM_MonsterStat monsterStat))
            {
                Maxhp = monsterStat.maxHP;
                Curhp = monsterStat.curHP;
                M_HPSlider[i].value = (Curhp/Maxhp) ;
                M_HPText[i].text = $"{Curhp}/{Maxhp}";
            }
        }
    }

    public void EXPChange()
    {

    }

    public void MonsterUISetting()
    {

        for(int i = 0;i<3 ; i++)
        {
            MonsterUI[i].SetActive(true);

        }
    }
}
