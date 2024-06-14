using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObj; // ������ �� ������Ʈ.
    [SerializeField]
    private int allocateCount; // ������ �� ����.

    private Stack<PoolLabel> poolStack = new Stack<PoolLabel>(); // �̸� ������ ������Ʈ�� ��� ���� ����.

    private int objMaxCount; // ������Ʈ Ǯ�� ���� ������Ʈ�� �� ����.
    private int objActiveCount; // Ȱ��ȭ �Ǿ� ��� ���� ������Ʈ�� �� ����.

    private void Awake()
    {
        objMaxCount = 0;
        objActiveCount = 0;
        Allocate();
    }

    private GameObject obj;
    public void Allocate()
    {
        for (int i = 0; i < allocateCount; i++)
        {
            obj = Instantiate(targetObj, this.transform);
            if (obj.TryGetComponent<PoolLabel>(out PoolLabel label))
            {
                label.Create(this); // �󺧿� �����س� ���ʰ� �������� ���.
                poolStack.Push(label);
                objMaxCount++;
            }
            else
                Debug.Log("ObjectPool.cs - Allocate() - PoolLabel�� �ƴ� ��ü�� ����");
        }
    }


    // pool���� �����ϰ� �ִ� ��ü �Ѱ� ����. 
    public GameObject Pop()
    {
        if (objActiveCount >= objMaxCount)
        {
            Allocate();
        }

        obj = poolStack.Pop().gameObject;
        obj.SetActive(true);
        if (obj.TryGetComponent<PoolLabel>(out PoolLabel label))
            label.GettingPool();
        objActiveCount++;
        return obj;
    }

    // ����� ���� ��ü�� ��ȯ �޴� �Լ�.
    public void Push(PoolLabel returnObj)
    {
        if (returnObj.gameObject.activeSelf) // Ȱ��ȭ�� ���·� ��ȯ �ƴٸ�,
        {
            returnObj.gameObject.SetActive(false); // ��Ȱ��ȭ ó��.
            poolStack.Push(returnObj);
            objActiveCount--;
        }
    }
}

