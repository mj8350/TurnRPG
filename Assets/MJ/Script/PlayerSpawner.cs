using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform[] PlayerPos;


    private void Awake()
    {
        PlayerSp();
    }

    public void PlayerSp()
    {
        for(int i = 0; i < PlayerPos.Length; i++)
        {
            int ran = Random.Range(0, GameManager.Instance.Prefeb.Length);
            GameObject obj = Instantiate(GameManager.Instance.Prefeb[ran], PlayerPos[i]);
            obj.transform.parent = PlayerPos[i];
        }
    }
}
