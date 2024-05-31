using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class YS_QTest : MonoBehaviour
{
    
    // 큐를 저장할 리스트
    private Queue<string> myQueue = new Queue<string>();
    //private Queue<int> myQueue = new Queue<int>();

    void Start()
    {
        // 큐에 데이터 삽입
        EnqueueData("스텔라");
        EnqueueData("드웨인");
        EnqueueData("아르샤");
        EnqueueData("루크");
        EnqueueData("사라");

        // 큐에서 데이터 삭제 및 출력
        DequeueData();
        DequeueData();
        DequeueData();
        DequeueData();
        DequeueData();
    }

    // 큐에 데이터를 삽입하는 함수
    void EnqueueData(string data)
    {
        myQueue.Enqueue(data);
        Debug.Log("Enqueued: " + data);
    }

    // 큐에서 데이터를 삭제하고 출력하는 함수
    void DequeueData()
    {
        if (myQueue.Count > 0)
        {
            string data = myQueue.Dequeue();
            Debug.Log("Dequeued: " + data);
        }
        else
        {
            Debug.Log("Queue is empty!");
        }
    }

    
}
