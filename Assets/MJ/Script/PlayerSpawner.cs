using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform[] PlayerPos;
    public FightUI fightUI;
    public string spname;
    public string PlayerName;

    private void Awake()
    {
        fightUI = FindFirstObjectByType<FightUI>();
        PlayerSp();
    }

    public void PlayerSp()
    {
        for (int i = 0; i < PlayerPos.Length; i++)
            //for (int i = 0; i < 2; i++)
        {
            //int ran = Random.Range(0, GameManager.Instance.Prefeb.Length);
            GameObject obj = Instantiate(GameManager.Instance.Prefeb[GameManager.Instance.player[i].id], PlayerPos[i]);
            obj.transform.parent = PlayerPos[i];
            spname = obj.name;
            spname = spname.Substring(0, spname.Length - 7);
            if(obj.TryGetComponent<PHM_CharStat>(out PHM_CharStat Player))
            {
                PlayerName = Player.CharName;
            }
            fightUI.ProfileUIChange(i, spname, PlayerName);
        }
    }
}
