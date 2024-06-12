using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChar : MonoBehaviour, IDamage
{

    public void TakeDamage(int damage)
    {
        if(TryGetComponent<PHM_MonsterStat>(out PHM_MonsterStat monster))
        {
            monster.curHP -= damage;
        }
        Debug.Log($"{gameObject.name}��(��) {damage}�� �������� �Ծ����ϴ�.");
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
