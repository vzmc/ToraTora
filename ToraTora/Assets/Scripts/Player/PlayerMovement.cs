using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int hp = 100;
    [SerializeField]
    private float moveSpeed = 5;
    [SerializeField]
    private Anchor anchor;

    private Vector2 direction;
    private Vector2 velocity;

    private Transform _transform;
    private Renderer[] renderers;

    public bool IsInvincible { get; private set; }
    public bool IsDead { get; private set; }

    private void Start()
    {
        _transform = transform;
        renderers = GetComponentsInChildren<Renderer>();
        IsInvincible = false;
        IsDead = false;
    }

    private void Update()
    {
        if (velocity.sqrMagnitude < float.Epsilon)
        {
            return;
        }

        _transform.Translate(velocity * Time.deltaTime, Space.Self);
    }

    /// <summary>
    /// 碰撞处理
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[1];
        int contactCount;
        contactCount = collision.GetContacts(contacts);
        _transform.Translate(contacts[0].normal * 0.4f);

        TakeDamage(0);
    }

    /// <summary>
    /// 发射钩爪
    /// </summary>
    public void Shoot()
    {
        //anchor.Shoot(direction);
    }

    /// <summary>
    /// 接受移动操作
    /// </summary>
    /// <param name="dir"></param>
    public void Move(Vector2 dir)
    {
        direction = dir.normalized;
        velocity = direction * moveSpeed;
        anchor.SetTransform(direction);
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if (IsInvincible)
        {
            return;
        }

        hp -= damage;

        // 进入无敌状态
        StartCoroutine(Invincible(0.5f));

        if (hp <= 0)
        {
            // 执行死亡
            IsDead = true;
            return;
        }
    }

    /// <summary>
    /// 无敌状态，闪烁
    /// </summary>
    /// <param name="duration">持续时间</param>
    /// <returns></returns>
    public IEnumerator Invincible(float duration)
    {
        IsInvincible = true;

        float timer = 0;
        float flashTimer = 0;

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
}
