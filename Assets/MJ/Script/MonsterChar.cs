using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChar : MonoBehaviour, IDamage
{

    public void TakeDamage(int damage)
    {
        Debug.Log($"{gameObject.name}��(��) {damage}�� �������� �Ծ����ϴ�.");
    }
}
