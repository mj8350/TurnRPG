using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class FightManager : MonoBehaviour
{
    public static FightManager Instance;

    public Transform[] PlayerPos;
    public Transform[] MonsterPos;
    private GameObject targetMonster;

    private MonsterAi monsterAi;
    //private BowExample BowExample;
    private AttackingExample AttackingExample;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 인스턴스가 파괴되지 않도록 합니다.
        }
        else
        {
            Destroy(gameObject);
        }


        monsterAi = GameObject.FindFirstObjectByType<MonsterAi>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            MonsterTurn(0);
        if(Input.GetKeyDown(KeyCode.S))
            PlayerTurnAttack(0);
    }

    public void SetTargetMonster(GameObject monster)
    {
        targetMonster = monster;
    }

    //public void ApplyDamageToSelectedMonster(int damage)
    //{
    //    if (targetMonster != null)
    //    {
    //        GameManager.Instance.Damage(targetMonster, damage);
    //    }
    //    else
    //    {
    //        Debug.Log("No monster selected.");
    //    }
    //}

    public void MonsterTurn(int pos)
    {
        GameObject obj = PlayerPos[Random.Range(0, PlayerPos.Length)].GetChild(0).gameObject;

        monsterAi.MonsterStart(0);
        GameManager.Instance.Damage(obj, 5);
    }

    public void PlayerTurnAttack(int who/*int pos*/)
    {
        //PlayerPos[who].TryGetComponent<BowExample>(out BowExample);
        //PlayerPos[who].TryGetComponent<AttackingExample>(out AttackingExample);
        PlayerPos[who].transform.GetChild(0).TryGetComponent<AttackingExample>(out AttackingExample);
        AttackingExample.PlayerStartAttack();

        //GameObject obj = MonsterPos[pos].GetChild(0).gameObject;
        GameObject obj = targetMonster;
        GameManager.Instance.Damage(obj, 5);
    }
}
