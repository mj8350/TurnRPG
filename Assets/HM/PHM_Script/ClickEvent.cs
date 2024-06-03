using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickEvent : MonoBehaviour
{
    private RaycastHit2D hit;
    private Vector2 targetPosition;
    private bool onSelected;
    private GameObject selectedObj;
    [SerializeField]
    GameObject selectedRingPrefabs;
    [SerializeField]
    private Image image;

    private void Update()
    {
        MouseClickDown();
    }

    void MouseClickDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject()) // 마우스가 UI 요소 위에 없는지 확인
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hit = Physics2D.Raycast(worldPoint, Vector2.zero);

                BoolOnonSelected();
            }
            
        }
    }

    void BoolOnonSelected()
    {
        if (hit.collider != null)
        {
            targetPosition = hit.transform.position;
            selectedObj = hit.collider.gameObject;

            InstantiateSelectRing();
            Debug.Log(selectedObj.transform);
            FightManager.Instance.SetTargetMonster(selectedObj);
            //AttackObject(selectedObj);
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
        if (!onSelected)
        {
            onSelected = true;
            if (GameObject.FindGameObjectWithTag("SelectRing") == null)
            {
                Instantiate(selectedRingPrefabs, targetPosition, Quaternion.identity);
            }
            else
            {
                GameObject.FindGameObjectWithTag("SelectRing").transform.position = targetPosition;
            }
            image.gameObject.SetActive(true);
        }
        else
        {
            GameObject.FindGameObjectWithTag("SelectRing").transform.position = targetPosition;
            image.gameObject.SetActive(true);
        }
    }

    //void AttackObject(GameObject obj)
    //{
    //    // 선택된 오브젝트가 Monster 태그를 가지고 있는지 확인
    //    if (obj.CompareTag("Monster"))
    //    {
    //        image.gameObject.SetActive(true);
    //    }
    //    else
    //    {
    //        Debug.Log("공격할 수 없는 오브젝트입니다.");
    //        image.gameObject.SetActive(false);
    //    }
    //}
}
