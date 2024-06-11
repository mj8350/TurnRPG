using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChar : MonoBehaviour, IDamage
{

    public void TakeDamage(int damage)
    {
        Debug.Log($"{gameObject.name}이(가) {damage}의 데미지를 입었습니다.");
        StartCoroutine("Hit");
    }

    IEnumerator Hit()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 5; i++)
        {
            transform.position += Vector3.right * Time.deltaTime * 20f;
            yield return new WaitForSeconds(0.02f);
        }
        for (int i = 0; i < 5; i++)
        {
            transform.position += Vector3.left * Time.deltaTime * 20f;
            yield return new WaitForSeconds(0.02f);
        }
    }

}
