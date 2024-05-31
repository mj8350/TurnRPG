using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YS_CharacterManager : MonoBehaviour
{
    public enum STATE
    {
        // 기본 상태
        WAIT, //턴을 받는 것을 대기
        ACTION, //턴을 받아 행동을 취함
        END, // 턴이 끝남
        DEAD, // 죽음
    }

    public enum EFFECT
    {
        //기본, 상태이상 없음
        NONE,

        // 특수 상태이상
        STUN
    }

    public class CharacterManager : MonoBehaviour, YS_ICharacter
    {
        private STATE state = STATE.END;
        private EFFECT effect = EFFECT.NONE;
        public float turnSpeed { get; private set; }

        // 스텟, 능력치
        public float turn = 0f;
        public int maxHp;
        public int curHp;
        public int power;
        public int magic;
        public int hide;
        public int speed;
        public int lucky;
        public int wisdom;
        // Hp bar, NowTurn
        public Transform hpBar;
        public GameObject nowTurn;

        // -------------메서드----------------
        // state 관련
        public STATE State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
        // effect 관련
        public EFFECT Effect
        {
            get
            {
                return effect;
            }
            set
            {
                effect = value;
            }
        }
        // 턴 관련
        public virtual void setTurn()
        {
            turnSpeed = Random.Range(0f, 3f);
        }
        public virtual float getTurn()
        {
            return turnSpeed + speed;
        }

        public void attack(CharacterManager target)
        {
            throw new System.NotImplementedException();
        }

        public bool skill(CharacterManager target, int num)
        {
            throw new System.NotImplementedException();
        }

        public string skill_1_Info()
        {
            throw new System.NotImplementedException();
        }

        public void skip()
        {
            throw new System.NotImplementedException();
        }

        public void onDamage(int damage)
        {
            throw new System.NotImplementedException();
        }
               
        public void recoverHP(int hp)
        {
            throw new System.NotImplementedException();
        }

        public void updateHpBar()
        {
            throw new System.NotImplementedException();
        }

        public void checkEffect()
        {
            throw new System.NotImplementedException();
        }

        public void dead()
        {
            throw new System.NotImplementedException();
        }

        public string getName()
        {
            throw new System.NotImplementedException();
        }

        public string getInfo()
        {
            throw new System.NotImplementedException();
        }

        public string getStatInfo()
        {
            throw new System.NotImplementedException();
        }

        public string enemyActionAI()
        {
            throw new System.NotImplementedException();
        }
    }
}
