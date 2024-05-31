using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YS_CharacterManager : MonoBehaviour
{
    public enum STATE
    {
        // �⺻ ����
        WAIT, //���� �޴� ���� ���
        ACTION, //���� �޾� �ൿ�� ����
        END, // ���� ����
        DEAD, // ����
    }

    public enum EFFECT
    {
        //�⺻, �����̻� ����
        NONE,

        // Ư�� �����̻�
        STUN
    }

    public class CharacterManager : MonoBehaviour, YS_ICharacter
    {
        private STATE state = STATE.END;
        private EFFECT effect = EFFECT.NONE;
        public float turnSpeed { get; private set; }

        // ����, �ɷ�ġ
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

        // -------------�޼���----------------
        // state ����
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
        // effect ����
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
        // �� ����
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
