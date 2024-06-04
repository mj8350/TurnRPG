using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightUI : MonoBehaviour
{
    public Image[] TurnImg;
    public Sprite[] TurnSp;

    private void Awake()
    {
        
    }




    public void TurnUIChange(int imgnum, string spname)
    {
        for(int i = 0; i < TurnSp.Length; i++)
        {
            Debug.Log(TurnSp[i]);
            Debug.Log(spname);
            if (TurnSp[i].name == spname)
            {
                Debug.Log("¹Ù²Þ");
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
