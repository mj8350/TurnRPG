using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
    public List<GameObject> provokedMonsters = new List<GameObject>(); // 도발당한 몬스터 리스트
    public List<GameObject> stunedMonsters = new List<GameObject>(); // 스턴당한 몬스터 리스트

    public CharSkillManager skillManager;

    public bool onAttack;
    public bool onTaunt;
    public bool onStun;

    private Vector3 turnPos;
    public GameObject turnLight;

    //public GameObject DamageCanvas;
    //public TextMeshProUGUI DamageText;
    //public GameObject Critical;

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
        //DamageCanvas.SetActive(false);
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
        int plyDef;
        // 무작위 플레이어를 공격
        targetPlayer = PlayerPos[Random.Range(0, PlayerPos.Length)].GetChild(0).gameObject;
        monsterAi.MonsterStart(pos); // 몬스터의 공격 시작
        
        MonsterPos[pos].GetChild(0).TryGetComponent<PHM_MonsterStat>(out var monsterStat);
        int dftDmg = big(monsterStat.Strength, monsterStat.Magic);
        if (dftDmg == monsterStat.Strength)
            plyDef = GameManager.Instance.player[GMChar(targetPlayer)].P_Defense;
        else
            plyDef = GameManager.Instance.player[GMChar(targetPlayer)].Magic;

        Damage(targetPlayer, DamageSum(dftDmg, monsterStat.Critical, plyDef)); // 몬스터의 공격력만큼 피해 입힘

        //if (onTaunt && !onStun) // 도발상태라면 도발타겟 공격
        //{
        //    if (provokedMonsters.Count > 0)
        //    {
        //        GameObject provokedMonster = provokedMonsters[0]; // 도발된 몬스터 중 첫 번째 몬스터 선택
        //        provokedMonsters.RemoveAt(0); // 도발된 몬스터 리스트에서 제거

        //        // 도발된 몬스터를 공격하는 코드 추가
        //        provokedMonster.GetComponent<MonsterAttack>().MonsterTurn(pos);
        //    }
        //    else
        //    {
        //        Debug.LogWarning("도발된 몬스터가 없습니다.");
        //    }
        //}
        //else if (!onTaunt && !onStun) // 도발 상태가 아니고 스턴 상태도 아니라면
        //{
        //    // 무작위 플레이어를 공격
        //    targetPlayer = PlayerPos[Random.Range(0, PlayerPos.Length)].GetChild(0).gameObject;
        //    monsterAi.MonsterStart(pos); // 몬스터의 공격 시작
        //    Damage(targetPlayer, 5); // 몬스터의 공격력만큼 피해 입힘
        //}
        //else if (onStun) // 스턴 상태라면
        //{
        //    //다음 턴으로 넘김
        //    TurnQueue.Dequeue();
        //    TrunOut();
        //    TurnDraw();
        //    onStun = false; // 스턴 상태 비활성화

        //}

    }

    public void TauntMonsterTurn(int pos, GameObject target)
    {
        int plyDef;
        targetPlayer = target;
        monsterAi.MonsterStart(pos); // 몬스터의 공격 시작

        MonsterPos[pos].GetChild(0).TryGetComponent<PHM_MonsterStat>(out var monsterStat);
        int dftDmg = big(monsterStat.Strength, monsterStat.Magic);
        if (dftDmg == monsterStat.Strength)
            plyDef = GameManager.Instance.player[GMChar(targetPlayer)].P_Defense;
        else
            plyDef = GameManager.Instance.player[GMChar(targetPlayer)].Magic;

        Damage(targetPlayer, DamageSum(dftDmg, monsterStat.Critical, plyDef));
    }

    public void PlayerTurnAttack(int who/*int pos*/)
    {
        int monDef;
        //PlayerPos[who].TryGetComponent<BowExample>(out BowExample);
        //PlayerPos[who].TryGetComponent<AttackingExample>(out AttackingExample);
        PlayerPos[who].transform.GetChild(0).TryGetComponent<AttackingExample>(out AttackingExample);
        AttackingExample.PlayerStartAttack();

        //GameObject obj = MonsterPos[pos].GetChild(0).gameObject;
        GameObject obj = targetMonster;
        obj.TryGetComponent<PHM_MonsterStat>(out var monsterStat);
        int dftDmg = big(GameManager.Instance.player[who].Strength, GameManager.Instance.player[who].Magic);
        if( dftDmg == GameManager.Instance.player[who].Strength)
            monDef = monsterStat.P_Defense;
        else
            monDef = monsterStat.M_Defense;
        Damage(obj, DamageSum(dftDmg, GameManager.Instance.player[who].Critical,monDef));
    }
    int big(int a, int b)
    {
        return a > b ? a : b;
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
                    charStat.TakeDamage(damage);
                    StartCoroutine(getDamage(i, damage));
                    StartCoroutine(DamageT(obj, damage, 2.3f));
                }
            }
        }
        
    }

    private IEnumerator getDamage(int i, int damage)
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.player[i].CurHP -= damage;
        
    }

    //private IEnumerator DamageT(GameObject obj, int damage, float time, bool onCri)
    private IEnumerator DamageT(GameObject obj, int damage, float time)
    {
        yield return new WaitForSeconds(time);
        GameObject DT;
        damagePos = obj.transform.position;
        damagePos.y += 1;
        //if (onCri)
        if(true)
            DT = PoolManager.Inst.pools[1].Pop();
        else
            DT = PoolManager.Inst.pools[0].Pop();

        if(DT.TryGetComponent<DamageText>(out DamageText dt))
        {
            DT.transform.position = damagePos;
            dt.TextChange(damage);
            dt.StartUp();
        }

        //DamageCanvas.transform.position = damagePos;
        //DamageCanvas.SetActive(true);

        //DamageText.text = damage.ToString();
        //for(int i = 0; i < 50;i++)
        //{
        //    DamageCanvas.transform.position += Vector3.up * Time.deltaTime * 5;
        //    yield return new WaitForSeconds(0.01f);
        //}
        //DamageCanvas.SetActive(false);
    }

    public void Heal(GameObject obj, int heal)
    {
        if (obj.TryGetComponent<PHM_CharStat>(out PHM_CharStat charStat))
        {
            for (int i = 0; i < PlayerPos.Length; i++)
            {
                if (obj.transform.parent.name == PlayerPos[i].name)
                {
                    StartCoroutine(getHeal(i,heal));
                    StartCoroutine(HealText(obj, heal));
                }
            }
        }
    }
    private IEnumerator getHeal(int i, int heal)
    {
        yield return new WaitForSeconds(0.5f);
        if (GameManager.Instance.player[i].MaxHP < GameManager.Instance.player[i].CurHP + heal)
            GameManager.Instance.player[i].CurHP = GameManager.Instance.player[i].MaxHP;
        else
            GameManager.Instance.player[i].CurHP += heal;
    }
    private IEnumerator HealText(GameObject obj, int heal)
    {
        yield return new WaitForSeconds(1f);
        GameObject DT;
        damagePos = obj.transform.position;
        damagePos.y += 1;
        DT = PoolManager.Inst.pools[2].Pop();

        if (DT.TryGetComponent<DamageText>(out DamageText dt))
        {
            DT.transform.position = damagePos;
            dt.TextChange(heal);
            dt.StartUp();
        }
    }

    //bool onCri = false;
    //public int DamageSum(int dftDamage, int critical, int def, out bool onCri)
    public int DamageSum(int dftDamage, int critical, int def)
    {
        int finalDmg = dftDamage - (def / 2);

        int ran = Random.Range(1, 101);
        Debug.Log(ran);
        if (10 + (critical * 2) >= ran)
        {
            finalDmg *= 2;
            //onCri = true;
        }//else
            //onCri = false;

        if (finalDmg <= 0)
            return 0;
        else
            return finalDmg;
    }

    public int GMChar(GameObject obj)
    {
        for (int i = 0; i < PlayerPos.Length; i++)
        {
            if (obj.transform.parent == PlayerPos[i])
            {
                return i;
            }
        }
        return -1;

    }
}
