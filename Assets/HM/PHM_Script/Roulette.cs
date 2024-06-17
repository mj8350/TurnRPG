using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Roulette : MonoBehaviour
{
    // �귿�� ���� Ȯ���� �����ϴ� �̺�Ʈ
    public static event Action<int> OnSuccessProbabilityChanged;


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
    public TextMeshProUGUI startText;
    //public Button stopButton;

    public int randomNumber;
    public int randomNumber_2;
    public int successProbability = 60;
    private bool isRoulette = false;

    private bool isRouletteRunning = false;
    private Coroutine rouletteCoroutine;
    public bool isAttackSuccessful = false; // ���� ���� ���θ� ����ϴ� ����
    private ClickEvent clickEvent;

    public CharSkillManager skillsManager;
    public SkillButton skillBtn;

    public bool onCri;


    private void Awake()
    {
        // ���� ��ư�� Ŭ�� �̺�Ʈ �߰�
        startButton.onClick.AddListener(StartRoulette);
        startText.text = "����";
        // ��ž ��ư�� Ŭ�� �̺�Ʈ �߰�
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
            // �ִϸ��̼� ����
            rouletteCoroutine = StartCoroutine("RouletteNumbers");
            isRouletteRunning = true;
            isAttackSuccessful = false; // ���ο� �귿 ���� �� ���� ���� �ʱ�ȭ
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(StopRoulette);
            startText.text = "����";
        }
        //else
        //{
        //    // �귿�� ������� �� �ֵ���
        //    StopCoroutine(rouletteCoroutine);
        //    rouletteCoroutine = StartCoroutine("RouletteNumbers");
        //    isAttackSuccessful = false; // ���ο� �귿 ���� �� ���� ���� �ʱ�ȭ
        //}
    }

    void StopRoulette()
    {
        if (isRouletteRunning)
        {
            FightManager.Instance.onAttack = false;
            StopCoroutine("RoulettOver");

            // �ִϸ��̼� ����
            StopCoroutine(rouletteCoroutine);
            isRouletteRunning = false;
            Debug.Log($"Random number: {randomNumber}");
            Debug.Log($"Random number: {randomNumber_2}");

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

            Invoke("Turnoff", 1);
            
        }
    }

    IEnumerator RouletteNumbers()
    {
        while (true)
        {
            // 1���� 100���� ������ ���� �����Ͽ� ǥ��
            randomNumber = UnityEngine.Random.Range(1, 101);
            randomNumber_2 = UnityEngine.Random.Range(1, 101);
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
        Debug.Log("�Ϲݰ��� ����Ȯ��" + FightManager.Instance.AccuracyPercent(GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Accuracy));

        // ���� ���ڿ� ���� ���� ���� �Ǵ� ���� ����
        if (randomNumber <= FightManager.Instance.AccuracyPercent(GameManager.Instance.player[FightManager.Instance.TurnQueue.Peek()].Accuracy))
        {
            isAttackSuccessful = true;
            Debug.Log("���� ����!");
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
            Debug.Log("���� ����!");
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
        startText.text = "����";
    }


    public void SkillJudgment_1()
    {
        FightManager.Instance.PlayerTurnSkill_1(FightManager.Instance.TurnQueue.Dequeue());

        //if (randomNumber <= 100)
        //{
        //    isAttackSuccessful = true;
        //    Debug.Log("���� ����!");
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
        //    Debug.Log("���� ����!");
        //    FightManager.Instance.TurnQueue.Dequeue();
        //    Invoke("InitRoulette", 1);
        //}
        //Invoke("Turnoff", 1);

    }

    public void SkillJudgment_2()
    {
        FightManager.Instance.PlayerTurnSkill_2(FightManager.Instance.TurnQueue.Dequeue());

        //Debug.Log("��ų02 ����Ȯ��" + successProbability);
        //// ���� ���ڿ� ���� ���� ���� �Ǵ� ���� ����
        //if (randomNumber <= 100)
        //{
        //    isAttackSuccessful = true;
        //    Debug.Log("���� ����!");
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
        //    Debug.Log("���� ����!");
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
            dt.TextChange("����");
            dt.StartUp();
        }
    }
}