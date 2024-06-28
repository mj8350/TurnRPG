using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveUIManager : MonoBehaviour
{
    public TextMeshProUGUI DiceText;
    public TextMeshProUGUI PointText;



    public void D_TextChange()
    {
        DiceText.text = GameManager.Instance.Dice.ToString();
    }

    public void P_TextChange()
    {
        PointText.text = GameManager.Instance.movePoint.ToString();
    }
}
