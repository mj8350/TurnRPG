using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterChatch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            StopCoroutine("GoScene");
            StartCoroutine("GoScene");
            GameManager.Instance.sceneState = SceneState.BattleScene;
        }
    }

    IEnumerator GoScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("BattleScene");
    }
}
