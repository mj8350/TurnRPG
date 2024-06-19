using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    public bool onCri;
    public PHM_CharStat stat;

    private void Awake()
    {
        
    }
    public virtual void Skill_Active()
    {
        Debug.Log("스킬 발동");
    }

    Vector3 damagePos;
    public IEnumerator DamageT(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        GameObject DT;
        damagePos = obj.transform.position;
        damagePos.y += 1;
        DT = PoolManager.Inst.pools[3].Pop();

        if (DT.TryGetComponent<DamageText>(out DamageText dt))
        {
            DT.transform.position = damagePos;
            dt.TextChange("실패");
            dt.StartUp();
        }
    }
}
