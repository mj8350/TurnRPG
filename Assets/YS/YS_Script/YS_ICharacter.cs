using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static YS_CharacterManager;

//����, �÷��̾��� ĳ���� �������̽�
public interface YS_ICharacter
{
    //�� ���ڰ� Ŭ���� ������ �ൿ
    void setTurn();
    float getTurn();


    //������
    void attack(CharacterManager target);
    //��ų ���, ��밡�ɿ� ���� true,false ��ȯ
    bool skill(CharacterManager target, int num);
    string skill_1_Info();
    //��ŵ, mpȸ��
    void skip();



    //���ݹ���
    void onDamage(int damage);
    
    //hpȸ��
    void recoverHP(int hp);
    //hp 
    void updateHpBar();
    

    //�����̻� ó��
    void checkEffect();


    //���ó��
    void dead();
    //�̸� ��ȯ
    string getName();
    //���� ��ȯ
    string getInfo();
    //���� ���� ��ȯ
    string getStatInfo();

    //Enemy����
    //AI, �ൿ ���� (Attack, Skill1, Skill2 ... )
    string enemyActionAI();
}
