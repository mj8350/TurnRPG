using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YS_Skills : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    // 플레이어와 적의 체력, 여기서는 단순화를 위해 직접 값을 할당
    public int playerHealth = 100;
    public int enemyHealth = 100;

    // 스킬 사용 예시
    public void UseSkill(string skillName)
    {
        switch (skillName)
        {
            case "Fireball":
                // Fireball 스킬은 적에게 20의 데미지를 줍니다.
                enemyHealth -= 20;
                messageText.text = "적에게 20의 데미지를 주었습니다!";
                break;
            case "Heal":
                // Heal 스킬은 플레이어의 체력을 20 회복합니다.
                playerHealth += 20;
                messageText.text = "체력을 20 회복했습니다!";
                break;
                // 다른 스킬들도 여기에 추가 가능
        }

        // 게임 오버 조건 검사
        CheckGameOver();
    }

    void CheckGameOver()
    {
        if (enemyHealth <= 0)
        {
            messageText.text = "승리했습니다!";
            // 게임 오버 처리...
        }
        else if (playerHealth <= 0)
        {
            messageText.text = "패배했습니다...";
            // 게임 오버 처리...
        }
    }
}
