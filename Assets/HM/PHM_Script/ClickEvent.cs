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
    public Vector2 targetPosition;
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
    private bool click;

    private void Awake()
    {
        //rouletteImage.gameObject.SetActive(false);
        click = true;
    }

    private void Update()
    {
        /*if (FightManager.Instance.onAttack)
            MouseClickDown();
        if (Input.GetMouseButtonUp(0))
            click = true;*/

        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(gameObject.name);
        }
    }

    void MouseClickDown()
    {
        if (click&&Input.GetMouseButtonDown(0))
        {
            click = false;
            Debug.Log("����");
            if (!EventSystem.current.IsPointerOverGameObject()) // ���콺�� UI ��� ���� ������ Ȯ��
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hit = Physics2D.Raycast(worldPoint, Vector2.zero);

                BoolOnonSelected();

                
            }
            //if (onSelected)
            {
                onRoulette = true;
<<<<<<< Updated upstream
                //if(!rouletteImage.gameObject.activeSelf)
=======
                if (!rouletteImage.gameObject.activeSelf)
>>>>>>> Stashed changes
                    rouletteImage.gameObject.SetActive(true);
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
            Debug.Log(selectedObj.transform +"1");
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
        //if(onSelected)
        //{
        //    onRoulette = true;
        //    rouletteImage.gameObject.SetActive(true);
        //}
        Debug.Log("����Ų��");
        FightManager.Instance.onAttack = true;
        if (onSkill)
        {
            SkillImage.gameObject.SetActive(false);
            onSkill = false;
        }
        if (onItem)
        {
            ItemImage.gameObject.SetActive(false);
            onItem = false;
        }

    }

    public void ShowItemInfo()
    {
        if (!onItem)
        {
            if (onSkill)
            {
                SkillImage.gameObject.SetActive(false);
                onSkill = false;
            }
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
            if (onItem)
            {
                ItemImage.gameObject.SetActive(false);
                onItem = false;
            }
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
