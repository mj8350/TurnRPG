using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Roulette : MonoBehaviour
{
    // 룰렛의 성공 확률을 변경하는 이벤트
    public static event Action<int> OnSuccessProbabilityChanged;


    // 클릭 버튼 타입
    public enum ButtonType
    {
        None,
        Attack,
        Skill1,
        Skill2
    }

    public TextMeshProUGUI showText;
    public Button startButton;
    public TextMeshProUGUI startText;
    //public Button stopButton;

    public int randomNumber;
    public int randomNumber_2;
    public int successProbability = 60;
    private bool isRoulette = false;

    private bool isRouletteRunning = false;
    private Coroutine rouletteCoroutine;
    public bool isAttackSuccessful = false; // 공격 성공 여부를 기록하는 변수
    private ClickEvent clickEvent;

    public CharSkillManager skillsManager;
    public SkillButton skillBtn;

    public bool onCri;


    private void Awake()
    {
        // 시작 버튼에 클릭 이벤트 추가
        startButton.onClick.AddListener(StartRoulette);
        startText.text = "시작";
        // 스탑 버튼에 클릭 이벤트 추가
        //stopButton.onClick.AddListener(StopRoulette);

        clickEvent = GameObject.FindFirstObjectByType<ClickEvent>();
        skillsManager = GameObject.FindFirstObjectByType<CharSkillManager>();
        skillBtn = GameObject.FindFirstObjectByType<SkillButton>();
    }

    private void Start()
    {
        InitRoulette();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        InitRoulette();
    }

    public void StartRoulette()
    {
        if (!isRouletteRunning)
        {
            StartCoroutine("RoulettOver");
            // 애니메이션 시작
            rouletteCoroutine = StartCoroutine("RouletteNumbers");
            isRouletteRunning = true;
            isAttackSuccessful = false; // 새로운 룰렛 시작 시 공격 판정 초기화
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(StopRoulette);
            startText.text = "멈춤";
        }
        //else
        //{
        //    // 룰렛을 재시작할 수 있도록
        //    StopCoroutine(rouletteCoroutine);
        //    rouletteCoroutine = StartCoroutine("RouletteNumbers");
        //    isAttackSuccessful = false; // 새로운 룰렛 시작 시 공격 판정 초기화
        //}
    }

    void StopRoulette()
    {
        if (isRouletteRunning)
        {
            FightManager.Instance.onAttack = false;
            StopCoroutine("RoulettOver");

            // 애니메이션 멈춤
            StopCoroutine(rouletteCoroutine);
            isRouletteRunning = false;
            Debug.Log($"Random number: {randomNumber}");
            Debug.Log($"Random number: {randomNumber_2}");

            //AttackJudgment();

            // 클릭된 버튼의 상태에 따라 적절한 저지먼트를 발동
            switch (clickEvent.GetClickedButton())
            {
                case ButtonType.Attack:
                    AttackJudgment();
                    break;
                case ButtonType.Skill1:
                    SkillJudgment_1();
                    break;
                case ButtonType.Skill2:
                    SkillJudgment_2();
                    break;
            }

            Invoke("Turnoff", 1);
            
        }
    }

    IEnumerator RouletteNumbers()
    {
        while (true)
        {
            // 1부터 100까지 랜덤한 숫자 선택하여 표시
            randomNumber = UnityEngine.Random.Range(1, 101);
            randomNumber_2 = UnityEngine.Random.Range(1, 101);
            showText.text = randomNumber.ToString();

            // 잠시 대기
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator RoulettOver()
    {
        yield return new WaitForSeconds(5);
        StopCoroutine("RouletteNumbers");
        StopRoulette();
    }

    public void InitRoulette()
    {
        // 초기화
        //gameObject.SetActive(false);
        clickEvent.onRoulette = false;
        gameObject.transform.localScale = Vector3.zero;
        showText.text = "0";
        isAttackSuccessful = false;
        clickEvent.DestroySelectRing();
    }

    public void ShowRoulette()
    {
        gameObject.transform.localScale = Vector3.one;
    }

    public void InvokeInitRoulette()
    {
        Invoke("InitRoulette", 1);
    }

    public void HideSkillInfo()
    {
        clickEvent.HideSkillInfo();
    }

    public void AttackJudgment()
    {
        Debug.Log("일반공격 성공확률" + FightManager.Instance.AccuracyPercent(GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Accuracy));

        // 랜덤 숫자에 따라 공격 성공 또는 실패 판정
        if (randomNumber <= FightManager.Instance.AccuracyPercent(GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Accuracy))
        {
            isAttackSuccessful = true;
            Debug.Log("공격 성공!");
            if (FightManager.Instance != null)
            {
                FightManager.Instance.PlayerTurnAttack(FightManager.Instance.TurnQueue.Dequeue(), onCri);
                Invoke("InitRoulette", 1);
            }
            else
            {
                Debug.LogError("FightManager instance is null.");
            }
        }
        else
        {
            isAttackSuccessful = false;
            Debug.Log("공격 실패!");
            StartCoroutine(DamageT(FightManager.Instance.PlayerPos[FightManager.Instance.TurnQueue.Peek()].gameObject));
            FightManager.Instance.TurnQueue.Dequeue();
            Invoke("InitRoulette", 1);
        }
        
    }

    public void Turnoff()
    {
        FightManager.Instance.TrunOut();
        FightManager.Instance.TurnDraw();
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(StartRoulette);
        startText.text = "시작";
    }


    public void SkillJudgment_1()
    {
        FightManager.Instance.PlayerTurnSkill_1(FightManager.Instance.TurnQueue.Dequeue());

        //if (randomNumber <= 100)
        //{
        //    isAttackSuccessful = true;
        //    Debug.Log("공격 성공!");
        //    if (FightManager.Instance != null)
        //    {
        //        FightManager.Instance.PlayerTurnSkill_1(FightManager.Instance.TurnQueue.Dequeue());
        //        Invoke("InitRoulette", 1);
        //    }
        //    else
        //    {
        //        Debug.LogError("FightManager instance is null.");
        //    }
        //}
        //else
        //{
        //    isAttackSuccessful = false;
        //    Debug.Log("공격 실패!");
        //    FightManager.Instance.TurnQueue.Dequeue();
        //    Invoke("InitRoulette", 1);
        //}
        //Invoke("Turnoff", 1);

    }

    public void SkillJudgment_2()
    {
        FightManager.Instance.PlayerTurnSkill_2(FightManager.Instance.TurnQueue.Dequeue());

        //Debug.Log("스킬02 성공확률" + successProbability);
        //// 랜덤 숫자에 따라 공격 성공 또는 실패 판정
        //if (randomNumber <= 100)
        //{
        //    isAttackSuccessful = true;
        //    Debug.Log("공격 성공!");
        //    if (FightManager.Instance != null)
        //    {
        //        FightManager.Instance.PlayerTurnSkill_2(FightManager.Instance.TurnQueue.Dequeue());
        //        Invoke("InitRoulette", 1);
        //    }
        //    else
        //    {
        //        Debug.LogError("FightManager instance is null.");
        //    }
        //}
        //else
        //{
        //    isAttackSuccessful = false;
        //    Debug.Log("공격 실패!");
        //    FightManager.Instance.TurnQueue.Dequeue();
        //    Invoke("InitRoulette", 1);
        //}
        //Invoke("Turnoff", 1);

    }

    Vector3 damagePos;
    private IEnumerator DamageT(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        GameObject DT;
        damagePos = obj.transform.position;
        damagePos.y += 1;
        DT = PoolManager.Inst.pools[3].Pop();

        if (DT.TryGetComponent<DamageText>(out DamageText dt))
        {
            DT.transform.position = damagePos;
            dt.TextChange("실패");
            dt.StartUp();
        }
    }
}