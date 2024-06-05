using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{

    public List<GameObject> MonsterList = new List<GameObject>();

    public Transform[] MonsterPos;

    // Start is called before the first frame update
    void Start()
    {
        MonsterSp();
    }

    public void MonsterSp()
    {
        //for (int i = 0; i < MonsterPos.Length; i++)
        for (int i = 0; i < 2; i++)
        {
            int ran = Random.Range(0, MonsterList.Count);
            GameObject obj = Instantiate(MonsterList[ran], MonsterPos[i]);
            obj.transform.parent = MonsterPos[i];
        }
    }
}
