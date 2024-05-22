using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PHM_CharStat : MonoBehaviour
{
    public int maxHP;
    public int curHP;
    public int maxMP;
    public int curMP;
    public int Exp;

    public PHM_Stat STR; // Èû ½ºÅÝ
    public PHM_Stat DEX; // ¹ÎÃ¸ ½ºÅÝ
    public PHM_Stat LUK; // ¿î ½ºÅÝ (¸íÁß·ü °ü·Ã?)
    public PHM_Stat INT; // ¹æ¾î ½ºÅÝ


    public delegate void ChangeHPDelegate(int changeHP);
    ChangeHPDelegate HPDelegate;
    public static event ChangeHPDelegate changeHP;

    private void Start()
    {
        maxHP = curHP = 100;
        maxMP = curMP = 100;

        //HPDelegate += ModifyHP; // µ¨¸®°ÔÀÌÆ® ÀÌº¥Æ® ±¸µ¶
        //HPDelegate(30);

        Debug.Log(STR.GetStat()+5);
    }



    private void ModifyHP(int newHP)
    {
        curHP = newHP;
        // Ã¼·Â °ü·Ã ÀÌº¥Æ®
        changeHP?.Invoke(newHP);
    }

}
