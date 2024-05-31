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
        // ���� ��ư�� Ŭ�� �̺�Ʈ �߰�
        startButton.onClick.AddListener(StartRoulette);

        // ��ž ��ư�� Ŭ�� �̺�Ʈ �߰�
        stopButton.onClick.AddListener(StopRoulette);
    }
            
    public Button startButton;
    public Button stopButton;

    private bool isRoulette = false;

   
    void StartRoulette()
    {
        if (!isRoulette)
        {
            // �ִϸ��̼� ����
            StartCoroutine("RouletteNumbers");
            isRoulette = true;
        }
    }

    void StopRoulette()
    {
        if (isRoulette)
        {
            // �ִϸ��̼� ����
            StopCoroutine("RouletteNumbers");
            isRoulette = false;
            Debug.Log(randomNumber);
        }
    }

    IEnumerator RouletteNumbers()
    {
        while (true)
        {
            // 1���� 100���� ������ ���� �����Ͽ� ǥ��
            randomNumber = Random.Range(1, 101);
            showText.text = randomNumber.ToString();

            // ��� ���
            yield return new WaitForSeconds(0.05f);
        }
    }

    void InitRoultte()
    {
        randomNumber = 0;
    }
}

