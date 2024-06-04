using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YS_Skills : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    // �÷��̾�� ���� ü��, ���⼭�� �ܼ�ȭ�� ���� ���� ���� �Ҵ�
    public int playerHealth = 100;
    public int enemyHealth = 100;

    // ��ų ��� ����
    public void UseSkill(string skillName)
    {
        switch (skillName)
        {
            case "Fireball":
                // Fireball ��ų�� ������ 20�� �������� �ݴϴ�.
                enemyHealth -= 20;
                messageText.text = "������ 20�� �������� �־����ϴ�!";
                break;
            case "Heal":
                // Heal ��ų�� �÷��̾��� ü���� 20 ȸ���մϴ�.
                playerHealth += 20;
                messageText.text = "ü���� 20 ȸ���߽��ϴ�!";
                break;
                // �ٸ� ��ų�鵵 ���⿡ �߰� ����
        }

        // ���� ���� ���� �˻�
        CheckGameOver();
    }

    void CheckGameOver()
    {
        if (enemyHealth <= 0)
        {
            messageText.text = "�¸��߽��ϴ�!";
            // ���� ���� ó��...
        }
        else if (playerHealth <= 0)
        {
            messageText.text = "�й��߽��ϴ�...";
            // ���� ���� ó��...
        }
    }
}
