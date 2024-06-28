using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

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
        {
            ClassImg[i].gameObject.SetActive(false);
            ClassBtn[i].gameObject.SetActive(false);
        }

        
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
        GameManager.Instance.CreateUserData(index, curChar[index]);
        InfoChange(index);
        ClassBtn_On(index);
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
        GameManager.Instance.CreateUserData(index, curChar[index]);
        InfoChange(index);
        ClassBtn_On(index);
    }
    private void InfoChange(int index)
    {
        

        NameUI[index].text = GameManager.Instance.player[index].CharName;
        JobUI[index].text = GameManager.Instance.player[index].CharJob;

        NameUI_C[index].text = GameManager.Instance.player[index].CharName;
        StatUI_C[index].text = $"공격력: {GameManager.Instance.player[index].Strength}, 마법력: {GameManager.Instance.player[index].Magic},\n" +
            $"속도: {GameManager.Instance.player[index].Speed}, 체력: {GameManager.Instance.player[index].Helth}\n" +
            $"명중률: {GameManager.Instance.player[index].Accuracy}, 치명타: {GameManager.Instance.player[index].Critical},\n" +
            $"물리방여력: {GameManager.Instance.player[index].P_Defense},\n" +
            $"마법방어력: {GameManager.Instance.player[index].M_Defense},\n";
        AbilityUI_C[index].text = $"{GameManager.Instance.player[index].SpecialAB}\n" +
            $"스킬: {GameManager.Instance.player[index].Skill01}, {GameManager.Instance.player[index].Skill02}";
    }

    public void OnClick_ClassBtn(int index)
    {
        if (ClassImg[index].gameObject.activeSelf)
            ClassImg[index].gameObject.SetActive(false);
        else
            ClassImg[index].gameObject.SetActive(true);
    }

    public void ClassBtn_On(int index)
    {
        if (!ClassBtn[index].gameObject.activeSelf)
            ClassBtn[index].gameObject.SetActive(true);
    }

    public void OnClick_StartBtn()
    {
        if (curChar[0] != -1 && curChar[1] != -1 && curChar[2] != -1)
        {
            SceneManager.LoadScene("Move3");
            GameManager.Instance.sceneState = SceneState.MoveScene;
            GameManager.Instance.PlayerMovePos = Vector3.zero;
        }
    }

    public void OnClick_Title()
    {
        SceneManager.LoadScene("TitleScene");
        GameManager.Instance.sceneState = SceneState.TitleScene;
    }

}
