using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Roulette : MonoBehaviour
{
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
    public Button stopButton;

    private int randomNumber;
    public int successProbability = 60;
    private bool isRoulette = false;

    private bool isRouletteRunning = false;
    private Coroutine rouletteCoroutine;
    private bool isAttackSuccessful = false; // 공격 성공 여부를 기록하는 변수
    private ClickEvent clickEvent;

    public CharSkillManager skillsManager;
    public SkillButton skillBtn;

    private void Awake()
    {
        // 시작 버튼에 클릭 이벤트 추가
        startButton.onClick.AddListener(StartRoulette);

        // 스탑 버튼에 클릭 이벤트 추가
        stopButton.onClick.AddListener(StopRoulette);

        clickEvent = GameObject.FindFirstObjectByType<ClickEvent>();
        skillsManager = GameObject.FindFirstObjectByType<CharSkillManager>();
        skillBtn = GameObject.FindFirstObjectByType<SkillButton>();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        InitRoulette();
    }

    void StartRoulette()
    {
        if (!isRouletteRunning)
        {
            StartCoroutine("RoulettOver");
            // 애니메이션 시작
            rouletteCoroutine = StartCoroutine("RouletteNumbers");
            isRouletteRunning = true;
            isAttackSuccessful = false; // 새로운 룰렛 시작 시 공격 판정 초기화
        }
        else
        {
            // 룰렛을 재시작할 수 있도록
            StopCoroutine(rouletteCoroutine);
            rouletteCoroutine = StartCoroutine("RouletteNumbers");
            isAttackSuccessful = false; // 새로운 룰렛 시작 시 공격 판정 초기화
        }
    }

    void StopRoulette()
    {
        FightManager.Instance.onAttack = false;
        StopCoroutine("RoulettOver");
        if (isRouletteRunning)
        {
            // 애니메이션 멈춤
            StopCoroutine(rouletteCoroutine);
            isRouletteRunning = false;
            Debug.Log($"Random number: {randomNumber}");

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

        }

    }

    IEnumerator RouletteNumbers()
    {
        while (true)
        {
            // 1부터 100까지 랜덤한 숫자 선택하여 표시
            randomNumber = Random.Range(1, 101);
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
        gameObject.SetActive(false);
        showText.text = "0";
        isAttackSuccessful = false;
        clickEvent.DestroySelectRing();
    }

    public void HideSkillInfo()
    {
        clickEvent.HideSkillInfo();
    }

    public void AttackJudgment()
    {
        Debug.Log("성공확률" + successProbability);
        // 랜덤 숫자에 따라 공격 성공 또는 실패 판정
        if (randomNumber <= successProbability)
        {
            isAttackSuccessful = true;
            Debug.Log("공격 성공!");
            if (FightManager.Instance != null)
            {
                FightManager.Instance.PlayerTurnAttack(FightManager.Instance.TurnQueue.Dequeue());
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
            FightManager.Instance.TurnQueue.Dequeue();
            Invoke("InitRoulette", 1);
        }
        FightManager.Instance.TrunOut();
        FightManager.Instance.TurnDraw();
    }

    public void SkillJudgment_1()
    {
        Debug.Log("성공확률" + successProbability);
        // 랜덤 숫자에 따라 공격 성공 또는 실패 판정
        if (randomNumber <= successProbability)
        {
            isAttackSuccessful = true;
            Debug.Log("공격 성공!");
            if (FightManager.Instance != null)
            {
                FightManager.Instance.PlayerTurnSkill_1(FightManager.Instance.TurnQueue.Dequeue());
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
            FightManager.Instance.TurnQueue.Dequeue();
            Invoke("InitRoulette", 1);
        }
        FightManager.Instance.TrunOut();
        FightManager.Instance.TurnDraw();
    }

    public void SkillJudgment_2()
    {
        Debug.Log("성공확률" + successProbability);
        // 랜덤 숫자에 따라 공격 성공 또는 실패 판정
        if (randomNumber <= 100)
        {
            isAttackSuccessful = true;
            Debug.Log("공격 성공!");
            if (FightManager.Instance != null)
            {
                FightManager.Instance.PlayerTurnSkill_2(FightManager.Instance.TurnQueue.Dequeue());
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
            FightManager.Instance.TurnQueue.Dequeue();
            Invoke("InitRoulette", 1);
        }
        FightManager.Instance.TrunOut();
        FightManager.Instance.TurnDraw();
    }
}