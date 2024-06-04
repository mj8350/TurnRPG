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
         //적 리스트 초기화
        enemies.Add(new Enemy("Goblin", 100));
        enemies.Add(new Enemy("Orc", 100));
        enemies.Add(new Enemy("Dragon", 100));
    }

    // 스킬 사용 예시
    public void UseSkill(string skillName)
    {
        switch (skillName)
        {
            case "Fireball":
                //enemies -= 20;
                messageText.text = "Enemies took 20 damage!";
                break;
            case "Heal":
                // 힐 스킬은 플레이어의 체력을 회복
                playerHealth += 20;
                messageText.text = "Recovered 20 health!";
                break;
            case "Explosion":
                ApplyDamageToEnemies(50);
                messageText.text = "Enemies took 50 damage!";
                break;
                
        }

        // 게임 오버 조건 확인
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
        // 모든 적의 체력이 0 이하인지 확인
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
            // 게임 오버 처리...
        }
        else if (playerHealth <= 0)
        {
            messageText.text = "Defeat...";
            // 게임 오버 처리...
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

