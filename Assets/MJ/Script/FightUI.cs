using Assets.HeroEditor.FantasyHeroes.TestRoom.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FightUI : MonoBehaviour
{
    public Image[] caractor;
    public TextMeshProUGUI[] plyName;
    public Image[] TurnImg;
    public Sprite[] TurnSp;

    public TextMeshProUGUI PrimarySkill;
    public TextMeshProUGUI SecondarySkill;

    public Slider[] HPSlider;
    public Slider[] EXPSlider;

    public TextMeshProUGUI[] HPText;
    public TextMeshProUGUI[] EXPText;

    public GameObject[] MonsterUI;
    public Slider[] M_HPSlider;
    public TextMeshProUGUI[] M_HPText;
    public TextMeshProUGUI[] M_Level;
    public TextMeshProUGUI[] M_PD;
    public TextMeshProUGUI[] M_MD;

    public MonsterAttack[] monsterAttacks;
    public DotDamageSkill dotDamage;


    private void Awake()
    {
        monsterAttacks = FindObjectsOfType<MonsterAttack>();
        MonsterAttack temp = monsterAttacks[0];
        monsterAttacks[0] = monsterAttacks[2];
        monsterAttacks[2] = temp;
        dead = false;
    }
    private void Start()
    {
        for (int i = 0; i < MonsterUI.Length; i++)
        {
            //MonsterUI[i].SetActive(false);
        }
        EXPChange();
        MonsterUISetting();
    }

    public void ProfileUIChange(int num, string name, string PlayerName)
    {
        caractor[num].gameObject.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            if (TurnSp[i].name == name)
            {

                caractor[num].sprite = TurnSp[i];
                plyName[num].text = PlayerName;
                break;
            }
        }
    }


    public void TurnUIChange(int imgnum, string spname)
    {
        for (int i = 0; i < TurnSp.Length; i++)
        {
            if (TurnSp[i].name == spname)
            {
                TurnImg[imgnum].sprite = TurnSp[i];
                TurnImg[imgnum].gameObject.SetActive(true);
                break;
            }
        }
    }
    public int count = -1;
    public void TurnOut()
    {
        TurnImg[count].gameObject.SetActive(false);
    }

    private int[] dotCount = {0, 0, 0};
    private int[] dotCount_2 = { 0, 0, 0 };

    private bool dead;
    public void DrawTurn()
    {
        
        if (FightManager.Instance.TurnQueue.Count == 0)
        {
            FightManager.Instance.NewTurn();
            NewTurn();
            count = -1;
            Debug.Log("라운드 리셋");
            
        }

        int pos = FightManager.Instance.TurnQueue.Peek();
        if (pos < 3)
        {
            if (FightManager.Instance.DeadList.Count > 0)
            {
                for (int i = 0; i < FightManager.Instance.DeadList.Count; i++)
                {
                    if (pos == FightManager.Instance.DeadList[i])
                        dead = true;
                }

            }
            if (!dead)
            {
                Debug.Log(FightManager.Instance.PlayerPos[pos].GetChild(0).gameObject.name); // 큐에서 빼기 전에 이름 확인
                SkillTextInfo(pos); // 큐에서 빼온 숫자를 인자로 넘겨줌
            }
            count++;
            Debug.Log("턴 드로우");
            HPChange(); 
            if (dead)
            {
                FightManager.Instance.TurnQueue.Dequeue();
                dead = false;
                FightManager.Instance.TurnDraw();
            }
        }
        else
        {
            Debug.Log("몬스터 공격턴");
            //monsterAi.MonsterStart(pos-3);
            //FightManager.Instance.MonsterTurn(pos - 3);

            //if (!dead)
            StartCoroutine("Dot", pos);

            //if (FightManager.Instance.DeadList.Count > 0)
            //{
            //    for(int i = 0; i<FightManager.Instance.DeadList.Count; i++)
            //    {
            //        if (pos == FightManager.Instance.DeadList[i])
            //            dead = true;
            //    }
            //}

            //if (!dead)
            //StartCoroutine("monTurn", pos);
            HPChange();
        }
        
        //if (dead)
        //{
        //    FightManager.Instance.TurnQueue.Dequeue();
        //    dead = false;
        //    FightManager.Instance.TurnDraw();
        //}
    }

    IEnumerator Dot(int pos)
    {
        if (monsterAttacks[pos - 3].onDotDamage)
        {
            dotCount[pos - 3] += 1;
            FightManager.Instance.Damage(monsterAttacks[pos - 3].gameObject, 3 * dotCount[pos - 3], false);
        }
        yield return new WaitForSeconds(0.2f);
        if (monsterAttacks[pos - 3].onDotDamage_2)
        {
            dotCount_2[pos - 3] += 1;
            FightManager.Instance.Damage(monsterAttacks[pos - 3].gameObject, 5, false);
        }

        if (FightManager.Instance.DeadList.Count > 0)
        {
            for (int i = 0; i < FightManager.Instance.DeadList.Count; i++)
            {
                if (pos == FightManager.Instance.DeadList[i])
                    dead = true;
            }
        }
        if (!dead)
            monsterAttacks[pos - 3].MonsterTurn(pos - 3);
        yield return new WaitForSeconds(0.5f);
        count++;
        HPChange();
        if (dead)
        {
            FightManager.Instance.TurnQueue.Dequeue();
            dead = false;
            FightManager.Instance.TurnDraw();
        }
    }

    IEnumerator monTurn(int pos)
    {
        yield return new WaitForSeconds(1);
        monsterAttacks[pos - 3].MonsterTurn(pos - 3);
    }

    public void NewTurn()
    {
        for (int i = 0; i < FightManager.Instance.TurnQueue.Count; i++)
            TurnImg[i].gameObject.SetActive(true);

    }

    public void SkillTextInfo(int pos)
    {
        if (pos < 3)
        {

            Transform playerTransform = FightManager.Instance.PlayerPos[pos];
            PHM_CharStat playerStats = playerTransform.GetComponentInChildren<PHM_CharStat>();

            if (playerStats != null)
            {
                PrimarySkill.text = playerStats.GetPrimarySkill().ToString();
                SecondarySkill.text = playerStats.GetSecondarySkill().ToString();
            }
            else
            {
                Debug.LogError("PHM_CharStat 참조실패");
            }
        }
        else
        {
            PrimarySkill.text = "몬스터 턴";
            SecondarySkill.text = "몬스터 턴";
        }
    }


    public void HPChange()
    {
        int Maxhp, Curhp;
        for (int i = 0; i < 3; i++)
        {
            Maxhp = GameManager.Instance.player[i].MaxHP;
            Curhp = GameManager.Instance.player[i].CurHP;
            HPSlider[i].value = ((float)Curhp/Maxhp);
            HPText[i].text = $"{Curhp}/{Maxhp}";
        }
        for(int i = 0;i < 3;i++)
        {
            GameObject obj = FightManager.Instance.MonsterPos[i].GetChild(0).gameObject;
            if(obj.TryGetComponent<PHM_MonsterStat>(out PHM_MonsterStat monsterStat))
            {
                Maxhp = monsterStat.maxHP;
                Curhp = monsterStat.curHP;
                M_HPSlider[i].value = ((float)Curhp/Maxhp) ;
                M_HPText[i].text = $"{Curhp}/{Maxhp}";
            }
        }
    }

    public void EXPChange()
    {
        int MaxEXP, CurEXP;
        for(int i = 0;i<3 ; i++)
        {
            if (GameManager.Instance.player[i].Level < 10) {
                MaxEXP = GameManager.Instance.MaxEXP[GameManager.Instance.player[i].Level-1];
                CurEXP = GameManager.Instance.player[i].EXP;
                EXPSlider[i].value = ((float)CurEXP/MaxEXP);
                EXPText[i].text = $"{CurEXP}/{MaxEXP}";
            }
        }
    }

    public void MonsterUISetting()
    {

        for(int i = 0;i<3 ; i++)
        {
            MonsterUI[i].SetActive(true);
            GameObject obj = FightManager.Instance.MonsterPos[i].GetChild(0).gameObject;
            if (obj.TryGetComponent<PHM_MonsterStat>(out PHM_MonsterStat monsterStat))
            {
                M_Level[i].text = monsterStat.Level.ToString();
                M_PD[i].text = (monsterStat.P_Defense/2).ToString();
                M_MD[i].text = (monsterStat.M_Defense/2).ToString();
            }
        }
    }
}
