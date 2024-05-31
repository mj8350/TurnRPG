using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class YS_QTest : MonoBehaviour
{
    
    // ť�� ������ ����Ʈ
    private Queue<string> myQueue = new Queue<string>();
    //private Queue<int> myQueue = new Queue<int>();

    void Start()
    {
        // ť�� ������ ����
        EnqueueData("���ڶ�");
        EnqueueData("�����");
        EnqueueData("�Ƹ���");
        EnqueueData("��ũ");
        EnqueueData("���");

        // ť���� ������ ���� �� ���
        DequeueData();
        DequeueData();
        DequeueData();
        DequeueData();
        DequeueData();
    }

    // ť�� �����͸� �����ϴ� �Լ�
    void EnqueueData(string data)
    {
        myQueue.Enqueue(data);
        Debug.Log("Enqueued: " + data);
    }

    // ť���� �����͸� �����ϰ� ����ϴ� �Լ�
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
