using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YS_Skills : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    public int playerHealth = 100;
    public List<Enemy> enemies = new List<Enemy>();

    void Start()
    {
         //�� ����Ʈ �ʱ�ȭ
        enemies.Add(new Enemy("Goblin", 100));
        enemies.Add(new Enemy("Orc", 100));
        enemies.Add(new Enemy("Dragon", 100));
    }

    // ��ų ��� ����
    public void UseSkill(string skillName)
    {
        switch (skillName)
        {
            case "Fireball":
                //enemies -= 20;
                messageText.text = "Enemies took 20 damage!";
                break;
            case "Heal":
                // �� ��ų�� �÷��̾��� ü���� ȸ��
                playerHealth += 20;
                messageText.text = "Recovered 20 health!";
                break;
            case "Explosion":
                ApplyDamageToEnemies(50);
                messageText.text = "Enemies took 50 damage!";
                break;
                
        }

        // ���� ���� ���� Ȯ��
        CheckGameOver();
    }

    void ApplyDamageToEnemies(int damage)
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.TakeDamage(damage);
            Debug.Log(enemy.name + " took " + damage + " damage. Remaining health: " + enemy.health);
        }
    }

    void CheckGameOver()
    {
        // ��� ���� ü���� 0 �������� Ȯ��
        bool allEnemiesDefeated = true;
        foreach (Enemy enemy in enemies)
        {
            if (enemy.health > 0)
            {
                allEnemiesDefeated = false;
                break;
            }
        }

        if (allEnemiesDefeated)
        {
            messageText.text = "Victory!";
            // ���� ���� ó��...
        }
        else if (playerHealth <= 0)
        {
            messageText.text = "Defeat...";
            // ���� ���� ó��...
        }
    }
}

public class Enemy
{
    public string name;
    public int health;

    public Enemy(string name, int health)
    {
        this.name = name;
        this.health = health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}

