using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform[] PlayerPos;
    public FightUI fightUI;
    public string spname;

    private void Awake()
    {
        fightUI = FindFirstObjectByType<FightUI>();
        PlayerSp();
    }

    public void PlayerSp()
    {
        for(int i = 0; i < PlayerPos.Length; i++)
        {
            int ran = Random.Range(0, GameManager.Instance.Prefeb.Length);
            GameObject obj = Instantiate(GameManager.Instance.Prefeb[ran], PlayerPos[i]);
            obj.transform.parent = PlayerPos[i];
            spname = obj.name;
            spname = spname.Substring(0, spname.Length - 7);
            fightUI.ProfileUIChange(i, spname);
        }
    }
}
