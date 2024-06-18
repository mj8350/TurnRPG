using Assets.HeroEditor.Common.Scripts.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

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
    public List<int> DeadList = new List<int>();// 데드리스트
    public List<int> LiveList = new List<int>();//라이브 리스트

    public CharSkillManager skillManager;

    public bool onAttack;
    public bool onTaunt;
    public bool onStun;

    private Vector3 turnPos;
    public GameObject turnLight;

    //public GameObject DamageCanvas;
    //public TextMeshProUGUI DamageText;
    //public GameObject Critical;

    private PlayerManager playerManager;

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
        playerManager = GameObject.FindFirstObjectByType<PlayerManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            playerManager.PlayerDeath(0, 6);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            playerManager.PlayerDeath(0, 0);
        }
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
    
    public void MonsterTurn(int pos, bool onCri)
    {
        int plyDef;
        // 무작위 플레이어를 공격
        int random;
        random = Random.Range(0, PlayerPos.Length);
        if (DeadList.Count>0)
        {
            while (true)
            {
                random = Random.Range(0, PlayerPos.Length);
                int con = 0;
                for (int i = 0; i < DeadList.Count; i++)
                {
                    if (random == DeadList[i])
                    {
                        con++;
                    }
                }
                if (con == 0)
                    break;
            }
        }
        targetPlayer = PlayerPos[random].GetChild(0).gameObject;
        monsterAi.MonsterStart(pos); // 몬스터의 공격 시작
        
        MonsterPos[pos].GetChild(0).TryGetComponent<PHM_MonsterStat>(out var monsterStat);
        int dftDmg = big(monsterStat.Strength, monsterStat.Magic);
        if (dftDmg == monsterStat.Strength)
            plyDef = GameManager.Instance.player[GMChar(targetPlayer)].P_Defense;
        else
            plyDef = GameManager.Instance.player[GMChar(targetPlayer)].M_Defense;

        if(targetPlayer.TryGetComponent<Warrior>(out Warrior warrior))
        {
            int ran = Random.Range(1,101);
            if (ran <= 30)
                Damage(targetPlayer, 0, false);
            else
                Damage(targetPlayer, DamageSum(dftDmg, monsterStat.Critical, plyDef, out onCri), onCri);
        }
        else
            Damage(targetPlayer, DamageSum(dftDmg, monsterStat.Critical, plyDef, out onCri), onCri);// 몬스터의 공격력만큼 피해 입힘
    }

    public void TauntMonsterTurn(int pos, GameObject target, bool onCri)
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

        if (targetPlayer.TryGetComponent<Warrior>(out Warrior warrior))
        {
            int ran = Random.Range(1, 101);
            if (ran <= 30)
                Damage(targetPlayer, 0, false);
            else
                Damage(targetPlayer, DamageSum(dftDmg, monsterStat.Critical, plyDef, out onCri), onCri);
        }
        else
            Damage(targetPlayer, DamageSum(dftDmg, monsterStat.Critical, plyDef, out onCri), onCri); // 몬스터의 공격력만큼 피해 입힘
        //Damage(targetPlayer, DamageSum(dftDmg, monsterStat.Critical, plyDef, out onCri), onCri);
    }

    public void PlayerTurnAttack(int who, bool onCri)
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
        Damage(obj, DamageSum(dftDmg, GameManager.Instance.player[who].Critical,monDef , out onCri), onCri);
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

    public void Damage(GameObject obj, int damage, bool onCri)
    {


        if (!onCri)
        {
            if (turn.turnList[fightUI.count].Key < 3)
            {
                GameObject ply = PlayerPos[turn.turnList[fightUI.count].Key].GetChild(0).gameObject;
                if (ply.TryGetComponent<Thief>(out Thief thief))
                {
                    int ran = Random.Range(1, 101);
                    if (ran <= 30)
                    {
                        onCri = true;
                        Debug.Log("도적 고유스킬 발동");
                    }
                }
            }
        }
        if (onCri)
            damage *= 2;

        //if (obj.TryGetComponent<IDamage>(out IDamage idam))
        //{
        //    idam.TakeDamage(damage);
        //    StartCoroutine(DamageT(obj, damage,1, onCri));
        //}
        if (obj.TryGetComponent<MonsterChar>(out MonsterChar monChar))
        {
            monChar.TakeDamage(damage);
            if (monChar.ImDie())
            {
                //monsterAi.monsterDie(obj);
                //DeadList.Add(MTChar(obj));
                //turn.FindKeyAndDelete(MTChar(obj));
                MonsterDead(obj);
            }

            StartCoroutine(DamageT(obj, damage, 1, onCri));
        }

        if (obj.TryGetComponent<PHM_CharStat>(out PHM_CharStat charStat))
        {
            for (int i = 0; i < PlayerPos.Length; i++)
            {
                if (obj.transform.parent.name == PlayerPos[i].name)
                {
                    charStat.TakeDamage(damage);
                    StartCoroutine(getDamage(i, damage));
                    StartCoroutine(DamageT(obj, damage, 2.3f, onCri));
                }
            }
        }
    }

    public void MonsterDead(GameObject obj)
    {
        monsterAi.monsterDie(obj);
        DeadList.Add(MTChar(obj));
        LiveCut(MTChar(obj));
        DeadList = DeadList.Distinct().ToList();//중복제거;
        turn.FindKeyAndDelete(MTChar(obj));
        Destroy(obj.GetComponent<Collider2D>());
    }

    public void LiveCut(int key)
    {
        for(int i = 0;i < LiveList.Count;i++)
        {
            if (LiveList[i]==key)
                LiveList.RemoveAt(i);
        }
    }

    public bool PlayerWin = false;
    public bool MonsterWin = false;
    public bool GameOver = false;

    public bool WhoWin()
    {
        if (LiveList[0] > 2)
        {
            MonsterWin = true;
            GameOver = true;
            onAttack = false;
            return GameOver;
        }
        else if (LiveList[LiveList.Count - 1] < 3)
        {
            PlayerWin = true;
            GameOver = true;
            onAttack = false;
            return GameOver;
        }
        else
        {
            GameOver = false;
            return GameOver;
        }
    }

    private IEnumerator getDamage(int i, int damage)
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.player[i].CurHP -= damage;

        if (GameManager.Instance.player[i].CurHP <= 0)
        {
            GameManager.Instance.player[i].CurHP = 0;
            GameManager.Instance.player[i].onPlayerDead = true;
            playerManager.PlayerDeath(i, 6); // 죽음 애니메이션 표시
            DeadList.Add(i);// 캐릭터의 턴은 넘기도록.
            LiveCut(i);
            DeadList = DeadList.Distinct().ToList();//중복제거;
            turn.FindKeyAndDelete(i);
        }
        
    }

    private IEnumerator DamageT(GameObject obj, int damage, float time, bool onCri)
    {
        yield return new WaitForSeconds(time);
        GameObject DT;
        damagePos = obj.transform.position;
        damagePos.y += 1;
        if (damage > 0)
        {
            if (onCri)
                DT = PoolManager.Inst.pools[1].Pop();
            else
                DT = PoolManager.Inst.pools[0].Pop();
        }
        else
            DT = PoolManager.Inst.pools[3].Pop();


        if(DT.TryGetComponent<DamageText>(out DamageText dt))
        {
            DT.transform.position = damagePos;
            if(damage > 0)
                dt.TextChange(damage);
            else
                dt.TextChange("막기");
            fightUI.HPChange();
            dt.StartUp();
        }
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

    public void Resurrection(GameObject obj)
    {
        if (obj.TryGetComponent<PHM_CharStat>(out PHM_CharStat charStat))
        {
            for (int i = 0; i < PlayerPos.Length; i++)
            {
                if (obj.transform.parent.name == PlayerPos[i].name)
                {
                    StartCoroutine(getResurrection(i));
                }
            }
        }
    }

    public bool IsResurrectionTarget(GameObject obj)
    {
        // 전달받은 GameObject가 플레이어인지 확인하고, 플레이어이며 onPlayerDead가 true이면 부활 대상으로 판단합니다.
        if (obj != null && obj.CompareTag("Player"))
        {
            int playerId = GMChar(obj);
            if (playerId != -1 && GameManager.Instance.player[playerId].onPlayerDead)
            {
                return true;
            }
        }

        return false;
    }



    private IEnumerator getHeal(int i, int heal)
    {
        yield return new WaitForSeconds(0.5f);
        if (GameManager.Instance.player[i].MaxHP < GameManager.Instance.player[i].CurHP + heal)
            GameManager.Instance.player[i].CurHP = GameManager.Instance.player[i].MaxHP;
        else
            GameManager.Instance.player[i].CurHP += heal;
    }
    private IEnumerator getResurrection(int i)
    {
        yield return new WaitForSeconds(0.5f);
        if (GameManager.Instance.player[i].onPlayerDead)
        {
            GameManager.Instance.player[i].CurHP = GameManager.Instance.player[i].MaxHP / 2;
            GameManager.Instance.player[i].onPlayerDead = false;
            playerManager.PlayerDeath(i, 0); // 일어서는 애니메이션 표시
            // 데드리스트에서 부활한 대상을 빼야 함.
            for(int j = 0; j <DeadList.Count; j++)
            {
                if (DeadList[j] == i)
                {
                    DeadList.RemoveAt(i);
                }
            }
            LiveList.Add(i); // 라이브리스트에 추가
            LiveList.Sort(); // 정렬
            turn.FindKeyAndAdd(i);

        }
        else
            Debug.Log("부활대상이 아닙니다.");
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
            fightUI.HPChange();
            dt.StartUp();
        }
    }

    //bool onCri = false;
    public int DamageSum(int dftDamage, int critical, int def, out bool onCri)
    //public int DamageSum(int dftDamage, int critical, int def)
    {
        int finalDmg = dftDamage - (def / 2);

        int ran = Random.Range(1, 101);
        Debug.Log(ran);
        if (10 + (critical * 2) >= ran)
        {
            //finalDmg *= 2;
            onCri = true;
        }
        else
            onCri = false;

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

    public int MTChar(GameObject obj)
    {
        for(int i = 0; i<MonsterPos.Length; i++)
        {
            if(obj.transform.parent == MonsterPos[i])
            {
                return i + 3;
            }
        }
        return -1;
    }


    public int AccuracyPercent(int AC)
    {
        return 15 + (AC * 4);
    }
}
