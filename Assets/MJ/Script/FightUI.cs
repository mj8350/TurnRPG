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
            int pos = FightManager.Instance.TurnQueue.Dequeue();
            if (pos < 3)
            {
                Debug.Log(FightManager.Instance.PlayerPos[pos].GetChild(0).gameObject.name); // 큐에서 빼기 전에 이름 확인
                SkillTextInfo(pos); // 큐에서 빼온 숫자를 인자로 넘겨줌
            }
            else
            {
                Debug.Log("몬스터 공격턴");
            }

            if(FightManager.Instance.TurnQueue.Count == 0 )
            {
                FightManager.Instance.NewTurn();
            }
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
                Debug.LogError("PHM_CharStat 참조실패");
            }
        }
    }
}
