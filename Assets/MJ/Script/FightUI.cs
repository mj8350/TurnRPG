using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightUI : MonoBehaviour
{
    public Image[] caractor;
    public Image[] TurnImg;
    public Sprite[] TurnSp;

    private void Awake()
    {
        
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

}
