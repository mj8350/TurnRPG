using System.Collections;
using System.Collections.Generic;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using UnityEngine;

public class Move_Player : MonoBehaviour
{
    private MoveUIManager M_UI;
    public Transform[] playerPos;
    public GameObject obj;

    private Character[] Character = new Character[3];

    public bool CanMove;

    private void Start()
    {
        //юс╫ц
        //GameManager.Instance.PlayerMovePos = Vector3.zero;
        transform.position = GameManager.Instance.PlayerMovePos;

        M_UI = GameObject.FindFirstObjectByType<MoveUIManager>();
        GameManager.Instance.MoveUIText();
        CanMove = true;
        StartCoroutine("Create");

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (GameManager.Instance.Dice > 0&&GameManager.Instance.movePoint==0)
            {
                int ran = Random.Range(1, 7);
                //Debug.Log(ran);
                GameManager.Instance.movePoint += ran;
                GameManager.Instance.Dice--;
                GameManager.Instance.MoveUIText();
            }
        }
    }

    IEnumerator Create()
    {
        yield return new WaitForSeconds (0.001f);
        for (int i = 0; i < GameManager.Instance.player.Length; i++)
        {
            obj = Instantiate(GameManager.Instance.Prefeb[GameManager.Instance.player[i].id], playerPos[i]);
            obj.transform.parent = playerPos[i];
            obj.TryGetComponent<Character>(out Character[i]);
        }
    }

    private void PlayerAnim(int st)
    {
        for (int i = 0; i < 3; i++)
        {
            var state = Character[i].GetState();
            if (st == 1)
            {
                state = CharacterState.Run;
            }
            else if (st == 0)
            {
                state = CharacterState.Idle;
            }

            Character[i].SetState(state);
        }

    }
    public void ClickCollider(Vector3 pos)
    {
        Vector3 newPos = transform.position + pos;
        CanMove = false;
        if (pos.x > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                playerPos[i].rotation = Quaternion.identity;
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                playerPos[i].rotation = Quaternion.Euler(0, 180f, 0);
            }
        }

        //transform.position = Vector3.MoveTowards(transform.position, pos, 1f);
        StartCoroutine("playerWalk", newPos);
    }

    private IEnumerator playerWalk(Vector3 pos)
    {
        PlayerAnim(1);
        while (transform.position != pos)
        {
            yield return null;
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime*3f);
        }
        transform.position = pos;
        PlayerAnim(0);
        CanMove = true;
        GameManager.Instance.PlayerMovePosClear = transform.position;
        if (GameManager.Instance.Dice == 0 && GameManager.Instance.movePoint == 0)
            GameManager.Instance.RoundEnd();
    }
    public Vector3 lastPos;
    public void LastPosChange()
    {
        lastPos = transform.position;
    }

}
