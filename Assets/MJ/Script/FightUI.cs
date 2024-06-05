using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

public class FightUI : MonoBehaviour
{
    public Image[] caractor;
    public Image[] TurnImg;
    public Sprite[] TurnSp;

    public TextMeshProUGUI PrimarySkill;
    public TextMeshProUGUI SecondarySkill;

    
    
    

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log(FightManager.Instance.TurnQueue.Dequeue());
            
            SkillInfo(FightManager.Instance.TurnQueue.Dequeue()); // 실험용
        }
    }

    public void ProfileUIChange(int num, string name)
    {
        for (int i = 0; i < 5; i++)
        {
            if (TurnSp[i].name == name)
            {
                caractor[num].sprite = TurnSp[i];
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

    public void SkillInfo(int pos)
    {
        //pos = FightManager.Instance.TurnQueue.Dequeue();
        if (pos < 3) // 플레이어 pos
        {
            PrimarySkill.text = GameManager.Instance.player[pos].Skill01.ToString();
            SecondarySkill.text = GameManager.Instance.player[pos].Skill02.ToString();
        }

        Debug.Log(pos);
   
    }
}
