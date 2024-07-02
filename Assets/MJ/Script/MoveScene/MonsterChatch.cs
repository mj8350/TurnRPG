using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterChatch : MonoBehaviour
{

    public Move_Player player;
    public MonsterLife ML;
    public MoveUIManager UiManager;

    private void Awake()
    {
        player = GameObject.FindFirstObjectByType<Move_Player>();
        ML = GameObject.FindFirstObjectByType<MonsterLife>();
        UiManager = GameObject.FindFirstObjectByType<MoveUIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (collision.TryGetComponent<PHM_MonsterStat>(out PHM_MonsterStat Stat))
            {
                GameManager.Instance.MonsterStage = Stat.Stage;
                GameManager.Instance.MonsterValue = Stat.Value;
            }
            FindML(collision.gameObject);
            player.CanMove = false;
            StartCoroutine("Cant");
            StopCoroutine("GoScene");
            StartCoroutine("GoScene");
            GameManager.Instance.sceneState = SceneState.BattleScene;
        }
        if (collision.CompareTag("Boss"))
        {
            GameManager.Instance.MonsterStage = 4;
            GameManager.Instance.MonsterValue = 5;
            if (GameManager.Instance.MonsterLevel < 10)
                GameManager.Instance.MonsterLevel = 10;
            player.CanMove = false;
            StartCoroutine("Cant");
            StopCoroutine("GoBoss");
            StartCoroutine("GoBoss");
            GameManager.Instance.sceneState = SceneState.BattleScene;
        }
        if (collision.CompareTag("shop"))
        {
            UiManager.motelImg.gameObject.SetActive(true);
            player.CanMove = false;
            StartCoroutine("Cant");
        }
    }

    IEnumerator Cant()
    {
        yield return new WaitForSeconds(0.1f);
        player.CanMove = false;
    }

    IEnumerator GoScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("BattleScene");
        if (player.lastPos != null)
            GameManager.Instance.PlayerMovePos = player.lastPos;
        else
            GameManager.Instance.PlayerMovePos = player.transform.position;

    }

    IEnumerator GoBoss()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("BossScene");
        if (player.lastPos != null)
            GameManager.Instance.PlayerMovePos = player.lastPos;
        else
            GameManager.Instance.PlayerMovePos = player.transform.position;
    }

    private void FindML(GameObject obj)
    {
        for (int i = 0; i < ML.Monsters.Count; i++)
        {
            if (obj == ML.Monsters[i])
            {
                GameManager.Instance.WhatMonster = i;
                break;
            }
        }
    }
}
