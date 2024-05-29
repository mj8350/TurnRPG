using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PHM_Stat
{
    [SerializeField]
    private int baseStat;

    public int GetStat()
    {
        return baseStat;
    }
}
