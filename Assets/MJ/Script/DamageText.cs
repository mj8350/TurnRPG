using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : PoolLabel
{
    public TextMeshProUGUI DamageValue;


    public void TextChange(int dmg)
    {
        DamageValue.text = dmg.ToString();
    }

    public void StartUp()
    {
        StartCoroutine("Up");
    }

    IEnumerator Up()
    {
        for(int i = 0; i < 50;i++)
        {
            transform.position += Vector3.up * Time.deltaTime * 5;
            yield return new WaitForSeconds(0.01f);
        }
        ReturnPool();
    }
}
