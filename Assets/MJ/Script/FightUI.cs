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

    private void Awake()
    {
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(FightManager.Instance.PlayerPos[FightManager.Instance.TurnQueue.Dequeue()].GetChild(0).gameObject.name);
            SkillTextInfo(FightManager.Instance.TurnQueue.Dequeue());
        }
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
        for(int i = 0; i < TurnSp.Length; i++)
        {
            if (TurnSp[i].name == spname)
            {
                TurnImg[imgnum].sprite = TurnSp[i];
                TurnImg[imgnum].gameObject.SetActive(true);
                break;
            }
        }
    }

    public void TurnOut(int num)
    {
        TurnImg[num].gameObject.SetActive(false);
    }

    public void NewTurn(int end)
    {
        for( int i = 0;i < end;i++)
            TurnImg[i].gameObject.SetActive(true);
    }

    public void SkillTextInfo(int pos)
    {
        if(pos < 3)
        {
            PrimarySkill.text = GameManager.Instance.player[pos].Skill01.ToString(); // 자식에 있는 스텟 스크립트 찾아서 받아아야함.
            SecondarySkill.text = GameManager.Instance.player[pos].Skill02.ToString();
        }
    }
}
