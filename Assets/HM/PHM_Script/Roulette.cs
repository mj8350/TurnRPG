using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Roulette : MonoBehaviour
{
    // Ŭ�� ��ư Ÿ��
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
    private bool isAttackSuccessful = false; // ���� ���� ���θ� ����ϴ� ����
    private ClickEvent clickEvent;

    public CharSkillManager skillsManager;
    public SkillButton skillBtn;

    private void Awake()
    {
        // ���� ��ư�� Ŭ�� �̺�Ʈ �߰�
        startButton.onClick.AddListener(StartRoulette);

        // ��ž ��ư�� Ŭ�� �̺�Ʈ �߰�
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
        FightManager.Instance.onAttack = false;
        StopCoroutine("RoulettOver");
        if (isRouletteRunning)
        {
            // �ִϸ��̼� ����
            StopCoroutine(rouletteCoroutine);
            isRouletteRunning = false;
            Debug.Log($"Random number: {randomNumber}");

            //AttackJudgment();

            // Ŭ���� ��ư�� ���¿� ���� ������ ������Ʈ�� �ߵ�
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

    public void AttackJudgment()
    {
        Debug.Log("����Ȯ��" + successProbability);
        // ���� ���ڿ� ���� ���� ���� �Ǵ� ���� ����
        if (randomNumber <= successProbability)
        {
            isAttackSuccessful = true;
            Debug.Log("���� ����!");
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
            Debug.Log("���� ����!");
            FightManager.Instance.TurnQueue.Dequeue();
            Invoke("InitRoulette", 1);
        }
        FightManager.Instance.TrunOut();
        FightManager.Instance.TurnDraw();
    }

    public void SkillJudgment_1()
    {
        Debug.Log("����Ȯ��" + successProbability);
        // ���� ���ڿ� ���� ���� ���� �Ǵ� ���� ����
        if (randomNumber <= successProbability)
        {
            isAttackSuccessful = true;
            Debug.Log("���� ����!");
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
            Debug.Log("���� ����!");
            FightManager.Instance.TurnQueue.Dequeue();
            Invoke("InitRoulette", 1);
        }
        FightManager.Instance.TrunOut();
        FightManager.Instance.TurnDraw();
    }

    public void SkillJudgment_2()
    {
        Debug.Log("����Ȯ��" + successProbability);
        // ���� ���ڿ� ���� ���� ���� �Ǵ� ���� ����
        if (randomNumber <= 100)
        {
            isAttackSuccessful = true;
            Debug.Log("���� ����!");
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
            Debug.Log("���� ����!");
            FightManager.Instance.TurnQueue.Dequeue();
            Invoke("InitRoulette", 1);
        }
        FightManager.Instance.TrunOut();
        FightManager.Instance.TurnDraw();
    }
}