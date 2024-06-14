using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;
    public static PoolManager Inst => instance;

    private void Awake()
    {
        if (instance) // instance �� null �� �ƴϸ�,
        {
            Destroy(gameObject);
            return;
        }
        else
            instance = this; // ���ʷ� ��������� instance���, 
    }

    public List<ObjectPool> pools; // �ش� �Ŵ����� �����ϴ� ������Ʈ Ǯ�� ����Ʈ.
}
