using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : MonoBehaviour, IDamageable
{
    [SerializeField]
    private GuardType guardType;
    [SerializeField]
    private int hp = 1;

    private Renderer[] renderers;

    public bool IsInvincible { get; private set; }
    public bool IsDead { get; private set; }

    private void Start()
    {
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
        //TakeDamage(0);
        //collision.gameObject.GetComponent<IDamageable>().TakeDamage(1)
    }

    public void TakeDamage(int damage)
    {
        if (IsInvincible)
        {
            return;
        }

        hp -= damage;

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
        float flashTimer = 0.1f;

        while (timer < duration)
        {
            if (flashTimer > 0.1f)
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
        yield return Invincible(1f);

        Destroy(gameObject);
    }
}

public enum GuardType
{
    None,
    Top,
    Bottom,
    Whole,
}
