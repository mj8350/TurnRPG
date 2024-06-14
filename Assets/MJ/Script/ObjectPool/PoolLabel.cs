using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolLabel : MonoBehaviour
{
    protected ObjectPool pool; // �ڽ��� �������ִ� Ǯ�� � ��ü����. 

    // ������Ʈ�� Ǯ�� ���ʷ� ����� �ɶ�.
    public virtual void Create(ObjectPool owner)
    {
        pool = owner;
        gameObject.SetActive(false);
    }

    // ������Ʈ�� Ǯ�� ��ȯ�� �ɶ� ȣ��.
    public virtual void ReturnPool()
    {
        pool.Push(this);
    }

    // ������Ʈ�� Ǯ���� ������ ������ ȣ��.
    public virtual void GettingPool()
    {

    }
}
