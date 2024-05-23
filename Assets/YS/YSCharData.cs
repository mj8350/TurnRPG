using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "YSChar Data", menuName = "Scriptable Object/YSChar Data", order = int.MaxValue)]
public class YSCharData : ScriptableObject
{
    [SerializeField]
    private string charName;
    public string CharName { get { return charName; } }
    [SerializeField]
    private int hp;
    public int Hp { get { return hp; } }
    [SerializeField]
    private int damage;
    public int Damage { get { return damage; } }
    [SerializeField]
    private float sightRange;
    public float SightRange { get { return sightRange; } }
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }
}
