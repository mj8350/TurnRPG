using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Roulette : MonoBehaviour
{
    public TextMeshProUGUI showText;
    public Button startButton;
    public Button stopButton;

    private int randomNumber;
    private bool isRoulette = false;

    private bool isRouletteRunning = false;
    private Coroutine rouletteCoroutine;
    private bool isAttackSuccessful = false; // 공격 성공 여부를 기록하는 변수

    private void Awake()
    {
        // 시작 버튼에 클릭 이벤트 추가
        startButton.onClick.AddListener(StartRoulette);

        // 스탑 버튼에 클릭 이벤트 추가
        stopButton.onClick.AddListener(StopRoulette);
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
            // 애니메이션 시작
            rouletteCoroutine = StartCoroutine(RouletteNumbers());
            isRouletteRunning = true;
            isAttackSuccessful = false; // 새로운 룰렛 시작 시 공격 판정 초기화
        }
        else
        {
            // 룰렛을 재시작할 수 있도록
            StopCoroutine(rouletteCoroutine);
            rouletteCoroutine = StartCoroutine(RouletteNumbers());
            isAttackSuccessful = false; // 새로운 룰렛 시작 시 공격 판정 초기화
        }
    }

    void StopRoulette()
    {
        if (isRouletteRunning)
        {
            // 애니메이션 멈춤
            StopCoroutine(rouletteCoroutine);
            isRouletteRunning = false;
            Debug.Log($"Random number: {randomNumber}");

            // 랜덤 숫자에 따라 공격 성공 또는 실패 판정
            if (randomNumber <= 50)
            {
                isAttackSuccessful = true;
                Debug.Log("공격 성공!");
                if (FightManager.Instance != null)
                {
                    //FightManager.Instance.ApplyDamageToSelectedMonster(5);
                    FightManager.Instance.PlayerTurnAttack(0);
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
                Invoke("InitRoulette", 1);
            }
        }
    }

    IEnumerator RouletteNumbers()
    {
        while (true)
        {
            // 1부터 100까지 랜덤한 숫자 선택하여 표시
            randomNumber = Random.Range(1, 101);
            showText.text = randomNumber.ToString() + "%";

            // 잠시 대기
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void InitRoulette()
    {
        // 초기화
        gameObject.SetActive(false);
        showText.text = "0%";
        isAttackSuccessful = false;
    }
}