using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected int hp;

    public bool IsInvincible { get; protected set; }
    public bool IsDead { get; protected set; }

    /// <summary>
    /// 无敌状态，闪烁
    /// </summary>
    /// <param name="duration">持续时间</param>
    /// <returns></returns>
    protected abstract IEnumerator Invincible(float duration);
}
