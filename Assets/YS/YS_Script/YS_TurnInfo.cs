using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static YS_CharacterManager;

public class YS_TurnInfo : MonoBehaviour
{
    //public Dictionary<int,int> turnDictionary = new Dictionary<int,int>();
    //public List<int> turnList = new List<int>();
    //public Queue<int> turnQueue = new Queue<int>();


    //private void Awake()
    //{
    //    turnDictionary.Add(0, 1);
    //    turnDictionary.Add(0, 14);
    //    turnDictionary.Add(0, 16);
    //    turnDictionary.Add(0, 8);
    //    turnDictionary.Add(0, 12);
    //    turnDictionary.Add(0, 6);



    //    foreach(int i in turnDictionary.Values)
    //    {
    //        turnList.Add(i);
    //    }
    //}


    //public void TurnRe()
    //{
    //    turnList.Reverse();
    //    for (int i = 0; i < turnList.Count; i++)
    //    {
    //        turnQueue.Enqueue(turnList[i]);
    //    }
    //}

    public Dictionary<int, int> turnDictionary = new Dictionary<int, int>();
    public List<int> turnList = new List<int>();
    public Queue<int> turnQueue = new Queue<int>();

    private void Awake()
    {
        turnDictionary.Add(0, 1);
        turnDictionary.Add(1, 14);
        turnDictionary.Add(2, 16);
        turnDictionary.Add(3, 8);
        turnDictionary.Add(4, 12);
        turnDictionary.Add(5, 6);

        // ��ųʸ��� ����Ʈ�� ��ȯ�ϰ� ��(value)�� �������� ����
        List<KeyValuePair<int, int>> sortedList = turnDictionary.ToList();
        sortedList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

        // ���ĵ� ����Ʈ�� ���� turnList�� �߰�
        foreach (KeyValuePair<int, int> pair in sortedList)
        {
            turnList.Add(pair.Value);
        }
    }

    public void TurnRe()
    {
        turnList.Reverse();
        for (int i = 0; i < turnList.Count; i++)
        {
            turnQueue.Enqueue(turnList[i]);
        }
    }

    
}
