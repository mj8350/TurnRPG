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

    public MonsterAi monsterAi;

    private void Awake()
    {
        monsterAi = GameObject.FindFirstObjectByType<MonsterAi>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            int pos = FightManager.Instance.TurnQueue.Peek();
            if (pos < 3)
            {
                Debug.Log(FightManager.Instance.PlayerPos[pos].GetChild(0).gameObject.name); // ť���� ���� ���� �̸� Ȯ��
                SkillTextInfo(pos); // ť���� ���� ���ڸ� ���ڷ� �Ѱ���
            }
            else
            {
                Debug.Log("���� ������");
                monsterAi.MonsterStart(pos-3);
            }

        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            FightManager.Instance.TurnQueue.Dequeue();
            FightManager.Instance.TurnDraw();
            FightManager.Instance.TrunOut();
            Debug.Log("�ϳѱ��");
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
    public int count=-1;
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
            FightManager.Instance.MonsterTurn(pos-3);
        }
        count++;
        Debug.Log("�� ��ο�");
    }

    public void NewTurn()
    {
        for( int i = 0;i < FightManager.Instance.TurnQueue.Count;i++)
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
}
