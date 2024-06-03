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
            if (!EventSystem.current.IsPointerOverGameObject()) // ���콺�� UI ��� ���� ������ Ȯ��
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
    //    // ���õ� ������Ʈ�� Monster �±׸� ������ �ִ��� Ȯ��
    //    if (obj.CompareTag("Monster"))
    //    {
    //        image.gameObject.SetActive(true);
    //    }
    //    else
    //    {
    //        Debug.Log("������ �� ���� ������Ʈ�Դϴ�.");
    //        image.gameObject.SetActive(false);
    //    }
    //}
}
