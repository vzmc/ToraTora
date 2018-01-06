using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int hp = 1;

    public void TakeDamage(int damage)
    {
        hp -= damage;

    }
}
