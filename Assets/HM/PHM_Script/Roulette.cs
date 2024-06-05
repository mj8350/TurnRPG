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
    private bool isAttackSuccessful = false; // ���� ���� ���θ� ����ϴ� ����
    private ClickEvent clickEvent;

    public CharSkillManager skillsManager;

    private void Awake()
    {
        // ���� ��ư�� Ŭ�� �̺�Ʈ �߰�
        startButton.onClick.AddListener(StartRoulette);

        // ��ž ��ư�� Ŭ�� �̺�Ʈ �߰�
        stopButton.onClick.AddListener(StopRoulette);

        clickEvent = GameObject.FindFirstObjectByType<ClickEvent>();
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
            // �ִϸ��̼� ����
            rouletteCoroutine = StartCoroutine("RouletteNumbers");
            isRouletteRunning = true;
            isAttackSuccessful = false; // ���ο� �귿 ���� �� ���� ���� �ʱ�ȭ
        }
        else
        {
            // �귿�� ������� �� �ֵ���
            StopCoroutine(rouletteCoroutine);
            rouletteCoroutine = StartCoroutine("RouletteNumbers");
            isAttackSuccessful = false; // ���ο� �귿 ���� �� ���� ���� �ʱ�ȭ
        }
    }

    void StopRoulette()
    {

        clickEvent.onAttack = false;
        clickEvent.targetPosition = Vector2.zero;
        StopCoroutine("RoulettOver");
        if (isRouletteRunning)
        {
            // �ִϸ��̼� ����
            StopCoroutine(rouletteCoroutine);
            isRouletteRunning = false;
            Debug.Log($"Random number: {randomNumber}");

            // ���� ���ڿ� ���� ���� ���� �Ǵ� ���� ����
            if (randomNumber <= 50)
            {
                isAttackSuccessful = true;
                Debug.Log("���� ����!");
                if (FightManager.Instance != null)
                {
                    //FightManager.Instance.ApplyDamageToSelectedMonster(5);
                    FightManager.Instance.PlayerTurnAttack(0);
                    Invoke("InitRoulette", 1);
                    //Invoke("HideSkillInfo", 1);
                }
                else
                {
                    Debug.LogError("FightManager instance is null.");
                }
            }
            else
            {
                isAttackSuccessful = false;
                Debug.Log("���� ����!");
                Invoke("InitRoulette", 1);
                //Invoke("HideSkillInfo", 1);
            }
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

    IEnumerator RoulettOver()
    {
        yield return new WaitForSeconds(5);
        StopCoroutine("RouletteNumbers");
        StopRoulette();
    }

    public void InitRoulette()
    {
        // �ʱ�ȭ
        gameObject.SetActive(false);
        showText.text = "0";
        isAttackSuccessful = false;
        clickEvent.DestroySelectRing();
    }

    public void HideSkillInfo()
    {
        clickEvent.HideSkillInfo();
    }
}