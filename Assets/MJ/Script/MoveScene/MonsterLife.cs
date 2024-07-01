using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLife : MonoBehaviour
{
    public List<GameObject> Monsters = new List<GameObject>();

    private void Awake()
    {
        for(int i = 0; i < GameManager.Instance.MonsterLife.Length; i++)
        {
            if (!GameManager.Instance.MonsterLife[i])
                Destroy(Monsters[i]);
        }
    }
}
