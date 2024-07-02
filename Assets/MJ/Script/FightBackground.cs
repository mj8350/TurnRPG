using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBackground : MonoBehaviour
{
    public Sprite[] sp;
    public SpriteRenderer sr;



    private void Awake()
    {
        TryGetComponent<SpriteRenderer>(out sr);

        sr.sprite = sp[GameManager.Instance.MonsterStage];
    }
}
