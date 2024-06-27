using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveCollider : MonoBehaviour
{
    public GameObject Arrow;
    private Move_Player player;
    public Vector3 transPos;
    public bool isGround;


    private void Awake()
    {
        player = GameObject.FindFirstObjectByType<Move_Player>();
        Arrow.SetActive(false);
        isGround = true;
    }

    private void OnMouseDown()
    {
        if (player.CanMove&&Arrow.activeSelf)
        {
            GameManager.Instance.movePoint--;
            Debug.Log(GameManager.Instance.movePoint);
            player.ClickCollider(transPos);
            Arrow.SetActive(false);
        }
    }


    private void OnMouseOver()
    {
        if (player.CanMove && !Arrow.activeSelf&&isGround)
        {
            if (GameManager.Instance.movePoint > 0)
                Arrow.SetActive(true);
        }
    }

    private void OnMouseExit()
    {

        Arrow.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Debug.Log("몬스터 발견");

        }

        if (collision.CompareTag("Object")|| collision.CompareTag("Water"))
        {
            Debug.Log("걸림");
            isGround = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Object") || collision.CompareTag("Water"))
        {
            isGround = true;
        }
    }
}
