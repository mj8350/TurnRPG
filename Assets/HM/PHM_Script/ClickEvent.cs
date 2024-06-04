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
    private bool onRoulette;
    private bool onSelected;
    public GameObject selectedObj;
    [SerializeField]
    GameObject selectedRingPrefabs;
    [SerializeField] private Image rouletteImage;
    [SerializeField] private Image ItemImage;
    [SerializeField] private Image SkillImage;
    private bool onItem;
    private bool onSkill;

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
        if (hit.collider != null && hit.collider.CompareTag("Monster"))
        {
            targetPosition = hit.transform.position;
            selectedObj = hit.collider.gameObject;

            InstantiateSelectRing();
            Debug.Log(selectedObj.transform);
            FightManager.Instance.SetTargetMonster(selectedObj);
            //AttackObject(selectedObj);
        }
        else if(hit.collider != null && hit.collider.CompareTag("Player"))
        {
            targetPosition = hit.transform.position;
            selectedObj = hit.collider.gameObject;
            InstantiateSelectRing();
            Debug.Log(selectedObj.transform);
        }
        else
        {
            onSelected = false;
            DestroySelectRing();
            if (onRoulette)
            {
                HideRoulette();
            }
        }
    }

    void InstantiateSelectRing()
    {
        if (!onSelected)
        {
            onSelected = true;
            if (GameObject.FindGameObjectWithTag("SelectRing") == null)
            {
                Instantiate(selectedRingPrefabs, targetPosition + new Vector2(0,0.5f), Quaternion.identity);
            }
            else
            {
                GameObject.FindGameObjectWithTag("SelectRing").transform.position = targetPosition + new Vector2(0, 0.5f);
            }
            //image.gameObject.SetActive(true);
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("SelectRing") == null)
            {
                Instantiate(selectedRingPrefabs, targetPosition + new Vector2(0, 0.5f), Quaternion.identity);
            }
            else
            {
                GameObject.FindGameObjectWithTag("SelectRing").transform.position = targetPosition + new Vector2(0, 0.5f);
            }
            //image.gameObject.SetActive(true);
        }
        
    }

    public void DestroySelectRing()
    {
        Destroy(GameObject.FindGameObjectWithTag("SelectRing"));
    }

    public void Attacking()
    {
        if(onSelected)
        {
            onRoulette = true;
            rouletteImage.gameObject.SetActive(true);
        }
    }

    public void ShowItemInfo()
    {
        if (!onItem)
        {
            ItemImage.gameObject.SetActive(true);
            onItem = true;
        }
        else
        {
            ItemImage.gameObject.SetActive(false);
            onItem = false;
        }
        
    }

    public void ShowSkillInfo()
    {
        if (!onSkill)
        {
            SkillImage.gameObject.SetActive(true);
            onSkill = true;
        }
        else
        {
            SkillImage.gameObject.SetActive(false);
            onSkill = false;
        }
    }

    public void HideSkillInfo()
    {
        SkillImage.gameObject.SetActive(false);
        onSkill = false;
    }    

    public void HideRoulette()
    {
        rouletteImage.gameObject.SetActive(false);
        onRoulette = false;
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
