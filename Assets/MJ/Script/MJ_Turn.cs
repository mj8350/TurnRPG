using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJ_Turn : MonoBehaviour
{
    private Dictionary<string,int> turnDic = new Dictionary<string,int>();
    private List<KeyValuePair<string, int>> turnList = new List<KeyValuePair<string, int>> ();
    private Queue<string> turnQueue = new Queue<string>();

    private void Awake()
    {
        turnDic.Add("A",7);
        turnDic.Add("B", 9);
        turnDic.Add("C", 15);
        turnDic.Add("D", 8);
        turnDic.Add("E",5);
        turnDic.Add("F",8);

        foreach (KeyValuePair<string,int> dic in turnDic)
        {
            turnList.Add(dic);
        }

        Debug.Log(turnList[0]);
        

        turnList.Sort((y, x) =>
        {
            int sort = x.Value.CompareTo(y.Value);
            if (sort == 0)
                sort = Random.Range(-1,1);
            return sort;
        });
        for (int i = 0; i < turnList.Count; i++)
        {
            Debug.Log(turnList[i]);
            turnQueue.Enqueue(turnList[i].Key);
        }
    }

    public void DicAdd()
    {
        FightManager.Instance.PlayerPos[0].GetChild(0).TryGetComponent<PHM_CharStat>(out PHM_CharStat stat);
        turnDic.Add(stat.CharName, stat.Speed);
    }

}
