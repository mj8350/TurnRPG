using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SelectUI : MonoBehaviour
{
    public Button[] LeftBtn;
    public Button[] RightBtn;
    public Button[] ClassBtn;

    public TextMeshProUGUI[] JobUI;
    public TextMeshProUGUI[] NameUI;

    public Image[] ClassImg;
    public TextMeshProUGUI[] NameUI_C;
    public TextMeshProUGUI[] StatUI_C;
    public TextMeshProUGUI[] AbilityUI_C;

    private int[] curChar = { -1, -1, -1 };


    private void Awake()
    {
        for (int i = 0; i < 3; i++)
            ClassImg[i].gameObject.SetActive(false);

        
    }

    public void OnClick_LeftBtn(int index)
    {

        if (curChar[index] <= 0)
            curChar[index] = 4;
        else
            curChar[index]--;

        int[] newChar = (int[])curChar.Clone();
        newChar[index] = -1;
        while (Array.IndexOf(newChar, curChar[index]) > -1)
        {
            curChar[index]--;
            if (curChar[index] < 0)
                curChar[index] = 4;
        }

        GameManager.Instance.Player_Select(index, curChar[index]);
    }
    public void OnClick_RightBtn(int index)
    {
        if (curChar[index] > 3)
            curChar[index] = 0;
        else
            curChar[index]++;

        int[] newChar = (int[])curChar.Clone();
        newChar[index] = -1;
        while (Array.IndexOf(newChar, curChar[index]) > -1)
        {
            curChar[index]++;
            if (curChar[index] > 4)
                curChar[index] = 0;
        }


        GameManager.Instance.Player_Select(index, curChar[index]);
    }
    private void InfoChange(int index)
    {
        NameUI[index].text = GameManager.Instance.player[index].CharName;
        JobUI[index].text = GameManager.Instance.player[index].CharJob;

        NameUI_C[index].text = GameManager.Instance.player[index].CharName;
        //StatUI_C[index].text = $"명중률 {}, 물리방어 \n ";
    }


}
