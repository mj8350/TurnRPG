using Assets.HeroEditor.FantasyHeroes.TestRoom.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public bool onAttack;
    public bool onTaunt;
    public bool onStun;

    public GameObject targetPlayer;
    public GameObject tauntTarget;

    private void Awake()
    {
        onAttack = false;
        onTaunt = false;
        onStun = false;
    }

    public void MonsterTurn()
    {
        if (onTaunt && !onStun) // 도발상태라면 도발타겟 공격
        {
            AttackProvokedTarget();
        }
        else if (!onTaunt && !onStun)
        {
            AttackNormalTarget();
        }
        else if (onStun)
        {
            FightManager.Instance.TurnQueue.Dequeue(); // 턴넘기기
            FightManager.Instance.TrunOut();
            FightManager.Instance.TurnDraw();
            onStun = false;
        }
    }

    public void SetTauntTarget(GameObject target)
    {
        onTaunt = true;
        tauntTarget = target;
    }

    private void AttackProvokedTarget()
    {
        targetPlayer = tauntTarget;
        Debug.Log(targetPlayer);
        FightManager.Instance.monsterAi.MonsterStart(FightManager.Instance.TurnQueue.Dequeue());
        GameManager.Instance.Damage(targetPlayer, 5);
    }

    private void AttackNormalTarget()
    {
        Debug.Log(targetPlayer);
        FightManager.Instance.MonsterTurn(FightManager.Instance.TurnQueue.Dequeue());
        
    }
}

