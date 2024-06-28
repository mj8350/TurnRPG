using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterChatch : MonoBehaviour
{

    public Move_Player player;

    private void Awake()
    {
        player = GameObject.FindFirstObjectByType<Move_Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            player.CanMove = false;
            StopCoroutine("GoScene");
            StartCoroutine("GoScene");
            GameManager.Instance.sceneState = SceneState.BattleScene;
        }
    }

    IEnumerator GoScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("BattleScene");
        GameManager.Instance.PlayerMovePos = player.lastPos;
    }
}
