using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基础数据类
/// </summary>
public class Basic : MonoBehaviour, IDamageable
{
    [SerializeField]
    private WeekPoint weekPoint;
    [SerializeField]
    private int hp = 1;
    [SerializeField]
    private int point = 0;

    private Renderer[] renderers;
    private PlaySceneManager manager;

    public bool IsInvincible { get; private set; }
    public bool IsDead { get; private set; }

    public WeekPoint WeekPoint
    {
        get
        {
            return weekPoint;
        }
    }

    private void Start()
    {
        manager = PlaySceneManager.Instance;
        renderers = GetComponentsInChildren<Renderer>();
        IsInvincible = false;
        IsDead = false;
    }

    /// <summary>
    /// 碰撞处理
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.CompareTag("Anchor"))
        //{
        //    Anchor anchor = collision.gameObject.GetComponent<Anchor>();

        //    switch (weekPoint)
        //    {
        //        case WeekPoint.Whole:
        //            {
        //                TakeDamage(1);
        //                break;
        //            }
        //        case WeekPoint.Top:
        //            {
        //                if(anchor.Direction.y < 0)
        //                {
        //                    TakeDamage(1);
        //                }
        //                else
        //                {

        //                }
        //                break;
        //            }
        //    }
            
        //}
    }

    /// <summary>
    /// 造成伤害
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if (IsInvincible)
        {
            return;
        }

        hp -= damage;

        if(damage < 0)
        {
            return;
        }

        //如果是玩家，受到伤害就掉一个宝箱
        if(CompareTag("Player"))
        {
            manager.RemoveTreasure();
        }

        if (hp <= 0)
        {
            // 执行死亡
            IsDead = true;
            StartCoroutine(Die());
            return;
        }

        // 进入无敌状态
        StartCoroutine(Invincible(1f));
    }

    /// <summary>
    /// 无敌状态，闪烁
    /// </summary>
    /// <param name="duration">持续时间</param>
    /// <returns></returns>
    private IEnumerator Invincible(float duration)
    {
        IsInvincible = true;

        float timer = 0;
        float flashTimer = 0.05f;

        while (timer < duration)
        {
            if (flashTimer >= 0.05f)
            {
                foreach (var r in renderers)
                {
                    r.enabled = !r.enabled;
                }
                flashTimer = 0;
            }

            timer += Time.deltaTime;
            flashTimer += Time.deltaTime;

            yield return null;
        }

        foreach (var r in renderers)
        {
            r.enabled = true;
        }

        IsInvincible = false;
    }

    /// <summary>
    /// 死亡过程
    /// </summary>
    /// <returns></returns>
    private IEnumerator Die()
    {
        manager.AddScore(point);
        yield return Invincible(1f);
        Destroy(gameObject);
    }
}

public enum WeekPoint
{
    Whole,
    Top,
    Bottom,
    None,
}
