using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCollider : MonoBehaviour
{
    public GameObject Arrow;
    private Move_Player player;
    public Vector3 transPos;
    public bool isGround;
    private MoveUIManager M_UI;

    private void Awake()
    {
        M_UI = GameObject.FindFirstObjectByType<MoveUIManager>();
        player = GameObject.FindFirstObjectByType<Move_Player>();
        Arrow.SetActive(false);
        isGround = true;
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (player.CanMove && Arrow.activeSelf)
            {
                player.LastPosChange();
                GameManager.Instance.movePoint--;
                M_UI.RText();
                //Debug.Log(GameManager.Instance.movePoint);
                GameManager.Instance.MoveUIText();
                player.ClickCollider(transPos);
                Arrow.SetActive(false);
            }
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
        }

        if (collision.CompareTag("Object") || collision.CompareTag("Water"))
        {
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
