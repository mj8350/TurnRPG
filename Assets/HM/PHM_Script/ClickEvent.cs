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
    public bool onRoulette;
    private bool onSelected;
    public GameObject selectedObj;
    [SerializeField]
    GameObject selectedRingPrefabs;
    [SerializeField] private Image rouletteImage;
    [SerializeField] private Image ItemImage;
    [SerializeField] private Image SkillImage;
    private bool onItem;
    private bool onSkill;
    private bool myturn;

    public Button attackButton; // 공격 버튼
    public Button skill1Button; // 스킬1 버튼
    public Button skill2Button; // 스킬2 버튼

    // 클릭된 버튼 타입
    private Roulette.ButtonType clickedButtonType = Roulette.ButtonType.None;

    private void Start()
    {
        // 공격 버튼에 클릭 이벤트 핸들러 지정
        attackButton.onClick.AddListener(() => ClickedButton(Roulette.ButtonType.Attack));
        // 스킬1 버튼에 클릭 이벤트 핸들러 지정
        skill1Button.onClick.AddListener(() => ClickedButton(Roulette.ButtonType.Skill1));
        // 스킬2 버튼에 클릭 이벤트 핸들러 지정
        skill2Button.onClick.AddListener(() => ClickedButton(Roulette.ButtonType.Skill2));
    }

    // 버튼이 클릭되었을 때 호출되는 함수
    private void ClickedButton(Roulette.ButtonType buttonType)
    {
        clickedButtonType = buttonType;
    }

    // 클릭된 버튼의 타입을 반환하는 함수
    public Roulette.ButtonType GetClickedButton()
    {
        return clickedButtonType;
    }

    private void Update()
    {
        if (FightManager.Instance.onAttack)
            MouseClickDown();
        if (FightManager.Instance.TurnQueue.Count > 0)
        {
            if (FightManager.Instance.TurnQueue.Peek() < 3)
                myturn = true;
            else myturn = false;
        }

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
            if (onSelected)
            {
                onRoulette = true;
                if (onRoulette)
                    /*rouletteImage.gameObject.SetActive(true)*/
                    rouletteImage.gameObject.transform.localScale = Vector3.one;
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

        

        selectedObj = null;
        HideSkillInfo();
        ItemImage.gameObject.SetActive(false);
        onItem = false;
        onSelected = false;
    }

    public void Attacking()
    {
        //if(onSelected)
        //{
        //    onRoulette = true;
        //    rouletteImage.gameObject.SetActive(true);
        //}
        if (myturn)
        {
            Debug.Log("어택킨다");
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
    }

    public void ActiveWideSkill()
    {
        Debug.Log("어택킨다");
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

        onRoulette = true;
        if (!rouletteImage.gameObject.activeSelf)
            rouletteImage.gameObject.SetActive(true);
    }


    public void ShowItemInfo()
    {
        if (myturn)
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
    }

    public void ShowSkillInfo()
    {
        if (myturn)
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
    }

    public void HideSkillInfo()
    {
        SkillImage.gameObject.SetActive(false);
        onSkill = false;
    }    

    public void HideRoulette()
    {
        rouletteImage.gameObject.transform.localScale = Vector3.zero;
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
