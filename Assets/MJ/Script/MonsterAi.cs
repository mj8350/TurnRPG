using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAi : MonoBehaviour
{
    public Transform[] MonsterPos;
    private MonsterAnim Anim;

    public int pos = 0;

    private void Awake()
    {
        Anim = GameObject.FindFirstObjectByType<MonsterAnim>();
    }

    public void MonsterStart(int pos)
    {
        StartCoroutine("monsterAt",pos);
    }

    private IEnumerator monsterAt(int pos)
    {
        yield return new WaitForSeconds(1);
        Anim.ChangeAnimation(MonsterPos[pos].GetChild(0).gameObject, "Attack");
        yield return new WaitForSeconds(1);
        Anim.ChangeAnimation(MonsterPos[pos].GetChild(0).gameObject, "Idle");
    }

    
}
