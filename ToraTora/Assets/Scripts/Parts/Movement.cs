using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Basic))]
public class Movement : MonoBehaviour, IMovable
{
    [SerializeField]
    private float moveSpeed = 5;

    private Basic basic;
    private Transform _transform;
    private Vector2 velocity;

    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
    }

    void Start()
    {
        _transform = transform;
        basic = GetComponent<Basic>();
    }

    void Update()
    {
        if (!CompareTag("Player"))
        {
            if (basic.IsInvincible || basic.IsDead)
            {
                return;
            }
        }

        if (velocity.sqrMagnitude < float.Epsilon)
        {
            return;
        }

        _transform.Translate(velocity * Time.deltaTime, Space.Self);
    }

    /// <summary>
    /// 移动操作
    /// </summary>
    /// <param name="dir"></param>
    public void Move(Vector2 dir)
    {
        Debug.Log(dir);
        velocity = dir * moveSpeed;
    }
}
