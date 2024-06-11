using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJ_Turn : MonoBehaviour
{
    [SerializeField]
    public Dictionary<int, int> turnDic = new Dictionary<int, int>();
    public List<KeyValuePair<int, int>> turnList = new List<KeyValuePair<int, int>>();

    private PHM_CharStat[] playerStat = new PHM_CharStat[3];
    private PHM_MonsterStat[] monsterStat = new PHM_MonsterStat[3];
    private FightUI FightUi;

    private string name;

    private void Start()
    {
        FightUi = FindFirstObjectByType<FightUI>();

        


        //Invoke("DicAdd", 0.01f);
        DicAdd();



    }

    public void DicAdd()
    {
        for (int i = 0; i < 3; i++)
        {
            if (FightManager.Instance.PlayerPos[i].childCount > 0)
            {
                if (FightManager.Instance.PlayerPos[i].GetChild(0).TryGetComponent<PHM_CharStat>(out playerStat[i]))
                    turnDic.Add(i, playerStat[i].Speed);

                // FightManager에게 플레이어가진 스킬 정보 전달
                Transform playerTransform = FightManager.Instance.PlayerPos[i].GetChild(0);
                CharSkillManager skillManager = playerTransform.GetComponentInChildren<CharSkillManager>();
                if (skillManager != null)
                {
                    FightManager.Instance.SetPlayerSkillManager(i, skillManager); // FightManager에게 인덱스와 스킬 매니저 전달
                }
                else
                {
                    Debug.LogWarning("CharSkillManager component not found on the player character.");
                }

            }
            if (FightManager.Instance.MonsterPos[i].childCount > 0)
            {
                if (FightManager.Instance.MonsterPos[i].GetChild(0).TryGetComponent<PHM_MonsterStat>(out monsterStat[i]))
                    turnDic.Add(i + 3, monsterStat[i].Speed);

            }
        }
        foreach (KeyValuePair<int, int> dic in turnDic)
        {
            turnList.Add(dic);
        }
        turnList.Sort((y, x) =>
        {
            int sort = x.Value.CompareTo(y.Value);
            if (sort == 0)
                sort = Random.Range(-1, 1);
            return sort;
        });

        //// 여기서 각 플레이어의 캐릭터 스킬 매니저를 FightManager에게 전달
        //for (int i = 0; i < 3; i++)
        //{
        //    if (FightManager.Instance.PlayerPos[i].childCount > 0)
        //    {
        //        Transform playerTransform = FightManager.Instance.PlayerPos[i].GetChild(0);
        //        if (playerTransform.TryGetComponent<PHM_CharStat>(out playerStat[i]))
        //        {
        //            CharSkillManager skillManager = playerTransform.GetComponentInChildren<CharSkillManager>();
        //            if (skillManager != null)
        //            {
        //                FightManager.Instance.SetPlayerSkillManager(i, skillManager); // FightManager에게 인덱스와 스킬 매니저 전달
        //            }
        //            else
        //            {
        //                Debug.LogWarning("CharSkillManager component not found on the player character.");
        //            }
        //        }
        //    }
        //}

        NewTurn();
        FightManager.Instance.TurnDraw();


    }

    public void NewTurn()
    {
        for (int i = 0; i < turnList.Count; i++)
        {
            FightManager.Instance.TurnQueue.Enqueue(turnList[i].Key);

            FindCharName(turnList[i].Key);
            FightUi.TurnUIChange(i, name);
        }
    }

    public void FindCharName(int num)
    {
        switch (num)
        {
            case 0:
                name = playerStat[0].name;
                break;
            case 1:
                name = playerStat[1].name;
                break;
            case 2:
                name = playerStat[2].name;
                break;
            case 3:
                name = monsterStat[0].name;
                break;
            case 4:
                name = monsterStat[1].name;
                break;
            case 5:
                name = monsterStat[2].name;
                break;
        }
        name = name.Substring(0, name.Length - 7);
    }

}
