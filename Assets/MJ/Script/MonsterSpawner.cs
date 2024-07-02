using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{

    public List<GameObject> MonsterList = new List<GameObject>();

    private GameObject obj;

    public Transform[] MonsterPos;

    // Start is called before the first frame update
    void Awake()
    {
        MonsterSp();
    }

    public void MonsterSp()
    {
        obj = Instantiate(MonsterList[GameManager.Instance.MonsterStage * 5 + GameManager.Instance.MonsterValue], MonsterPos[0]);
        obj.transform.parent = MonsterPos[0];
        
        for (int i = 1; i < MonsterPos.Length; i++)
        {
            int ran = Random.Range(0, 5);
            obj = Instantiate(MonsterList[GameManager.Instance.MonsterStage*5 + ran], MonsterPos[i]);
            obj.transform.parent = MonsterPos[i];
        }
    }
}
