using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Anchor anchor;

    private Vector2 direction;
    private Vector2 velocity;
    
    private Rigidbody2D rigidbody2D;
    private Transform _transform;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        _transform = transform;
    }

    //private void Update()
    //{
    //    if(velocity.sqrMagnitude > float.Epsilon)
    //    {
    //        _transform.Translate(velocity * Time.deltaTime);
    //    }
    //}

    /// <summary>
    /// 物理更新
    /// </summary>
    private void FixedUpdate()
    {
        if (velocity.magnitude < float.Epsilon)
        {
            return;
        }

        rigidbody2D.MovePosition(rigidbody2D.position + velocity * Time.deltaTime);
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
        _transform.Translate(contacts[0].normal * 0.3f);
    }

    /// <summary>
    /// 接受移动操作
    /// </summary>
    /// <param name="dir"></param>
    public void Move(Vector2 dir)
    {
        direction = dir.normalized;
        velocity = direction * moveSpeed;
        anchor.SetPositionAndRotation(direction);
    }

    /// <summary>
    /// 发射钩爪
    /// </summary>
    public void Shoot()
    {
        anchor.Shoot(direction);
    }
}
