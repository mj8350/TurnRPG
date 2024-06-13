using Assets.HeroEditor.FantasyHeroes.TestRoom.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public bool onAttack;
    public bool onTaunt;
    public bool onStun;
    public bool onDotDamage;

    public GameObject targetPlayer;
    public GameObject tauntTarget;

    private void Awake()
    {
        onAttack = false;
        onTaunt = false;
        onStun = false;
    }

    public void MonsterTurn(int pos)
    {
        if (onTaunt && !onStun) // 도발상태라면 도발타겟 공격
        {
            AttackProvokedTarget(pos);
        }
        else if (!onTaunt && !onStun)
        {
            AttackNormalTarget(pos);
        }
        else if (onStun)
        {

            Invoke("Stunning", 1f);
            onStun = false;
        }
    }

    public void SetTauntTarget(GameObject target)
    {
        onTaunt = true;
        tauntTarget = target;
    }

    public void SetStunTarget() 
    {
        onStun = true;
    }

    public void SetDotDamage()
    {
        onDotDamage = true;
    }

    private void AttackProvokedTarget(int pos)
    {
        Debug.Log(targetPlayer);
        //FightManager.Instance.monsterAi.MonsterStart(pos);
        //FightManager.Instance.Damage(targetPlayer, 5);
        FightManager.Instance.TauntMonsterTurn(pos, tauntTarget);
        onTaunt = false;
    }

    private void AttackNormalTarget(int pos)
    {
        Debug.Log(targetPlayer);
        FightManager.Instance.MonsterTurn(pos);
    }

    private void Stunning()
    {
        FightManager.Instance.TurnQueue.Dequeue(); // 턴넘기기
        FightManager.Instance.TrunOut();
        FightManager.Instance.TurnDraw();
    }
}

