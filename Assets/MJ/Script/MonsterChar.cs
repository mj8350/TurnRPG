using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChar : MonoBehaviour, IDamage
{

    public void TakeDamage(int damage)
    {
        Debug.Log($"{gameObject.name}이(가) {damage}의 데미지를 입었습니다.");
    }
}
