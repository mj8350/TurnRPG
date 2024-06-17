using Assets.HeroEditor.FantasyHeroes.TestRoom.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class MonsterAttack : MonoBehaviour
{
    public bool onAttack;
    public bool onTaunt;
    public bool onStun;
    public bool onDotDamage;
    public bool onDotDamage_2;

    public GameObject targetPlayer;
    public GameObject tauntTarget;
    public bool onCri;

    private MonsterChar monsterChar;
    private PHM_MonsterStat monStat;

    private void Awake()
    {
        onAttack = false;
        onTaunt = false;
        onStun = false;
        if (!TryGetComponent<MonsterChar>(out monsterChar))
            Debug.Log("MonsterAttack.cs - Awake() - monsterChar참조 오류");
        if (!TryGetComponent<PHM_MonsterStat>(out monStat))
            Debug.Log("MonsterAttack.cs - Awake() - monStat참조 오류");
    }

    public void MonsterTurn(int pos)
    {
        int ran = Random.Range(1, 101);
        if (onTaunt && !onStun) // 도발상태라면 도발타겟 공격
        {
            if(ran <= FightManager.Instance.AccuracyPercent(monStat.Accuracy))
                AttackProvokedTarget(pos);
            else
            {
                // todo: 실패시 텍스트표시
            }
        }
        else if (!onTaunt && !onStun)
        {
            if (ran <= FightManager.Instance.AccuracyPercent(monStat.Accuracy))
                AttackNormalTarget(pos);
            else
            {
                // todo: 실패시 텍스트표시
            }
        }
        else if (onStun)
        {

            Invoke("Stunning", 1f);
            onStun = false;
            monsterChar.StunInit(onStun);
        }
    }

    public void SetTauntTarget(GameObject target)
    {
        onTaunt = true;
        tauntTarget = target;
        monsterChar.AngryInit(onTaunt);
    }

    public void SetStunTarget() 
    {
        onStun = true;
        monsterChar.StunInit(onStun);
    }

    public void SetDotDamage()
    {
        onDotDamage = true;
        monsterChar.PoisonInit(onDotDamage);
    }

    public void SetDotDamage_2()
    {
        onDotDamage_2 = true;
        monsterChar.FireInit(onDotDamage_2);
    }

    private void AttackProvokedTarget(int pos)
    {
        Debug.Log(targetPlayer);
        //FightManager.Instance.monsterAi.MonsterStart(pos);
        //FightManager.Instance.Damage(targetPlayer, 5);
        FightManager.Instance.TauntMonsterTurn(pos, tauntTarget, onCri);
        onTaunt = false;
        monsterChar.AngryInit(onTaunt);
    }

    private void AttackNormalTarget(int pos)
    {
        FightManager.Instance.MonsterTurn(pos, onCri);
    }

    private void Stunning()
    {
        FightManager.Instance.TurnQueue.Dequeue(); // 턴넘기기
        FightManager.Instance.TrunOut();
        FightManager.Instance.TurnDraw();
    }
}

