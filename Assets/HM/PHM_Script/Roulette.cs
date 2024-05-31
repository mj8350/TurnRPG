using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Roulette : MonoBehaviour
{
    
    public TextMeshProUGUI showText;
    private int randomNumber;

    private void Start()
    {
        // 시작 버튼에 클릭 이벤트 추가
        startButton.onClick.AddListener(StartRoulette);

        // 스탑 버튼에 클릭 이벤트 추가
        stopButton.onClick.AddListener(StopRoulette);
    }
            
    public Button startButton;
    public Button stopButton;

    private bool isRoulette = false;

   
    void StartRoulette()
    {
        if (!isRoulette)
        {
            // 애니메이션 시작
            StartCoroutine("RouletteNumbers");
            isRoulette = true;
        }
    }

    void StopRoulette()
    {
        if (isRoulette)
        {
            // 애니메이션 멈춤
            StopCoroutine("RouletteNumbers");
            isRoulette = false;
            Debug.Log(randomNumber);
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

    void InitRoultte()
    {
        randomNumber = 0;
    }
}

