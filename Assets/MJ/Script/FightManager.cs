using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class FightManager : MonoBehaviour
{
    public static FightManager Instance;

    public Transform[] PlayerPos;
    public Transform[] MonsterPos;
    public GameObject targetMonster;
    public GameObject targetPlayer;
    public GameObject tauntTarget;

    public MonsterAi monsterAi;
    //private BowExample BowExample;
    private AttackingExample AttackingExample;
    private MJ_Turn turn;
    private FightUI fightUI;

    public Queue<int> TurnQueue = new Queue<int>();
    private Dictionary<int, CharSkillManager> playerSkillManagers = new Dictionary<int, CharSkillManager>();

    public CharSkillManager skillManager;

    public bool onAttack;
    public bool onTaunt;
    public bool onStun;

    private Vector3 turnPos;
    public GameObject turnLight;

    public GameObject DamageCanvas;
    public TextMeshProUGUI DamageText;
    public GameObject Critical;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 인스턴스가 파괴되지 않도록 합니다.
        }
        else
        {
            Destroy(gameObject);
        }

        onAttack = false;
        onTaunt = false;
        onStun = false;
        monsterAi = GameObject.FindFirstObjectByType<MonsterAi>();
        turn = GameObject.FindFirstObjectByType<MJ_Turn>();
        fightUI = GameObject.FindFirstObjectByType<FightUI>();
        DamageCanvas.SetActive(false);
    }

    private void Start()
    {
        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            //MonsterTurn(0);
        if(Input.GetKeyDown(KeyCode.S))
            PlayerTurnAttack(0);

        
    }

    public void NewTurn()
    {
        turn.NewTurn();
        
    }

    public void TurnDraw()
    {
        fightUI.DrawTurn();
        if (TurnQueue.Peek() < 3)
        {
            turnPos = PlayerPos[TurnQueue.Peek()].transform.position;
        }
        else
        {
            turnPos = MonsterPos[TurnQueue.Peek()-3].transform.position;
        }
        turnPos.y += 0.5f;
        turnLight.transform.position = turnPos;

    }
    public void TrunOut()
    {
        fightUI.TurnOut();
    }
    public void SetTargetMonster(GameObject monster)
    {
        targetMonster = monster;
    }

    

    //public void ApplyDamageToSelectedMonster(int damage)
    //{
    //    if (targetMonster != null)
    //    {
    //        GameManager.Instance.Damage(targetMonster, damage);
    //    }
    //    else
    //    {
    //        Debug.Log("No monster selected.");
    //    }
    //}

    public void MonsterTurn(int pos)
    {
        if (onTaunt && !onStun) // 도발상태라면 도발타겟 공격

        {
            targetPlayer = tauntTarget;
            monsterAi.MonsterStart(pos);
            Damage(tauntTarget, 5);
            onTaunt = false;
        }
        else if (!onTaunt && !onStun)
        {
            targetPlayer = PlayerPos[Random.Range(0, PlayerPos.Length)].GetChild(0).gameObject;
            monsterAi.MonsterStart(pos);
            Damage(targetPlayer, 5);
        }
        else if (onStun)
        {
            TurnQueue.Dequeue(); // 턴넘기기
            TrunOut();
            TurnDraw();
            onStun = false;
        }

    }

    public void PlayerTurnAttack(int who/*int pos*/)
    {
        //PlayerPos[who].TryGetComponent<BowExample>(out BowExample);
        //PlayerPos[who].TryGetComponent<AttackingExample>(out AttackingExample);
        PlayerPos[who].transform.GetChild(0).TryGetComponent<AttackingExample>(out AttackingExample);
        AttackingExample.PlayerStartAttack();

        //GameObject obj = MonsterPos[pos].GetChild(0).gameObject;
        GameObject obj = targetMonster;
        Damage(obj, 5);
    }

    public void SetPlayerSkillManager(int playerIndex, CharSkillManager skillManager)
    {
        if (!playerSkillManagers.ContainsKey(playerIndex))
        {
            playerSkillManagers.Add(playerIndex, skillManager);
        }
        else
        {
            playerSkillManagers[playerIndex] = skillManager;
        }
    }

    public void PlayerTurnSkill_1(int who)
    {
        //PlayerPos[who].transform.GetChild(0).TryGetComponent<AttackingExample>(out AttackingExample);
        //AttackingExample.PlayerStartAttack();

        if (playerSkillManagers.ContainsKey(who))
        {
            playerSkillManagers[who].ActivatePrimarySkill();
        }
        else
        {
            Debug.LogError("Player's SkillManager is null!");
        }
    }

    public void PlayerTurnSkill_2(int who)
    {
        //PlayerPos[who].transform.GetChild(0).TryGetComponent<AttackingExample>(out AttackingExample);
        //AttackingExample.PlayerStartAttack();

        if (playerSkillManagers.ContainsKey(who))
        {
            playerSkillManagers[who].ActivateSecondarySkill();
        }
        else
        {
            Debug.LogError("Player's SkillManager is null!");
        }
    }

    Vector3 damagePos;
    public void Damage(GameObject obj, int damage)
    {
        if (obj.TryGetComponent<IDamage>(out IDamage idam))
        {
            idam.TakeDamage(damage);
            StartCoroutine(DamageT(obj, damage,1));
        }

        if (obj.TryGetComponent<PHM_CharStat>(out PHM_CharStat charStat))
        {
            for (int i = 0; i < PlayerPos.Length; i++)
            {
                if (obj.transform.parent.name == PlayerPos[i].name)
                {
                    Debug.Log("찾았다");
                    charStat.TakeDamage(damage);
                    StartCoroutine(getDamage(i, damage,obj));
                    StartCoroutine(DamageT(obj, damage, 2.3f));
                }
            }
        }
        
    }

    private IEnumerator getDamage(int i, int damage, GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.player[i].CurHP -= damage;
        
    }

    private IEnumerator DamageT(GameObject obj, int damage, float time)
    {
        yield return new WaitForSeconds(time);
        
        damagePos = obj.transform.position;
        damagePos.y += 1;
        DamageCanvas.transform.position = damagePos;
        DamageCanvas.SetActive(true);

        DamageText.text = damage.ToString();
        for(int i = 0; i < 50;i++)
        {
            DamageCanvas.transform.position += Vector3.up * Time.deltaTime * 5;
            yield return new WaitForSeconds(0.01f);
        }
        DamageCanvas.SetActive(false);
    }

}
