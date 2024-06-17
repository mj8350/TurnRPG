using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChar : MonoBehaviour, IDamage
{
    public GameObject Angry;
    public GameObject Poison;
    public GameObject Fire;
    public GameObject Stun;

    private void Awake()
    {
        Angry.SetActive(false);
        Poison.SetActive(false);
        Fire.SetActive(false);
        Stun.SetActive(false);
    }

    public void AngryInit(bool on)
    {
        Angry.SetActive(on);
    }

    public void PoisonInit(bool on)
    {
        Poison.SetActive(on);
    }

    public void FireInit(bool on)
    {
        Fire.SetActive(on);
    }

    public void StunInit(bool on) 
    { 
        Stun.SetActive(on);
    }



    public void TakeDamage(int damage)
    {
        if(TryGetComponent<PHM_MonsterStat>(out PHM_MonsterStat monster))
        {
            monster.curHP -= damage;
        }
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
