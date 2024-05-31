using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClickEvent : MonoBehaviour
{

    private RaycastHit2D hit;
    private Vector2 targetPosition;
    private bool onSelected;
    private GameObject selectedObj;
    [SerializeField]
    GameObject seletedRingPrefabs;
    [SerializeField]
    private Image image;

    private void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        //    if(hit.collider != null)
        //    {
        //        GameObject obj = hit.collider.gameObject;
        //        Debug.Log(obj.name);
        //    }
        //}

        MouseClickDown();

    }

    void MouseClickDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            BoolOnonSelected();
        }
    }

    void BoolOnonSelected()
    {
        if (hit.collider != null)
        {
            targetPosition = hit.transform.position;
            selectedObj = hit.collider.gameObject;

            InstantiateSelectRing();
            Debug.Log(selectedObj.name);
            AttackObject(selectedObj);
            
        }
        else
        {
            onSelected = false;
            Destroy(GameObject.FindGameObjectWithTag("SelectRing"));
            image.gameObject.SetActive(false);

        }

    }

    void InstantiateSelectRing()
    {
        if (!onSelected && !GameObject.FindGameObjectWithTag("SelectRing"))
        {
            onSelected = true;
           
            
            Instantiate(seletedRingPrefabs, targetPosition - new Vector2(-1, 1), Quaternion.identity);
            image.gameObject.SetActive(true);
        }
        else if (onSelected && GameObject.FindGameObjectWithTag("SelectRing"))
        {
            GameObject.FindGameObjectWithTag("SelectRing").transform.position = targetPosition - new Vector2(-1, 1);
            image.gameObject.SetActive(true);
        }
    }

    void AttackObject(GameObject obj)
    {
        // 선택된 오브젝트가 Attackable 태그를 가지고 있는지 확인
        if (obj.CompareTag("Monster"))
        {
            image.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("공격할 수 없는 오브젝트입니다.");
            image.gameObject.SetActive(false);
        }
    }
}
