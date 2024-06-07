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
    public GameObject targetMonster;
    public GameObject targetPlayer;
    public GameObject tauntTarget;

    private MonsterAi monsterAi;
    //private BowExample BowExample;
    private AttackingExample AttackingExample;

    public Queue<int> TurnQueue = new Queue<int>();

    public bool onAttack;
    public bool onTaunt;
    public bool onStun;

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

        onAttack = false;
        onTaunt = false;
        onStun = false;
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
        
        if(onTaunt && !onStun) // 도발상태라면 도발타겟 공격
        {
            targetPlayer = tauntTarget;
            monsterAi.MonsterStart(0);
            GameManager.Instance.Damage(tauntTarget, 5);
            onTaunt = false;
        }
        else if(!onTaunt && !onStun)
        {
            targetPlayer = PlayerPos[Random.Range(0, PlayerPos.Length)].GetChild(0).gameObject;
            monsterAi.MonsterStart(0);
            GameManager.Instance.Damage(targetPlayer, 5);
        }
        else if(onStun)
        {
            TurnQueue.Dequeue(); // 턴넘기기
            onStun = false;
        }
        
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
