using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可造成伤害
/// </summary>
public interface IDamageable
{
    void TakeDamage(int damage);
}
