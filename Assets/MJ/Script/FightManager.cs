using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FightManager : MonoBehaviour
{
    public Transform[] PlayerPos;
    public Transform[] MonsterPos;

    private MonsterAi monsterAi;
    private BowExample BowExample;
    private AttackingExample AttackingExample;

    private void Awake()
    {
        monsterAi = GameObject.FindFirstObjectByType<MonsterAi>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            MonsterTurn(0);
        if(Input.GetKeyDown(KeyCode.S))
            PlayerTurnAttack(0);
    }
    public void MonsterTurn(int pos)
    {
        GameObject obj = PlayerPos[Random.Range(0, PlayerPos.Length)].GetChild(0).gameObject;

        GameManager.Instance.Damage(obj, 5);
    }

    public void PlayerTurnAttack(int pos)
    {
        GameObject obj = MonsterPos[pos].GetChild(0).gameObject;
        GameManager.Instance.Damage(obj, 5);
    }
}
