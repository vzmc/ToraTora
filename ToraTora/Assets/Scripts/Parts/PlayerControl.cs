using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private Basic basic;
    [SerializeField]
    private Movement movement;
    [SerializeField]
    private Anchor anchor;

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        if(basic.IsDead)
        {
            return;
        }

        movement.Move(dir);
        anchor.SetTransform(dir);
    }

    /// <summary>
    /// 发射钩爪
    /// </summary>
    public void Shoot()
    {
        if (basic.IsDead)
        {
            return;
        }

        anchor.Shoot();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[1];
        int contactCount;
        contactCount = collision.GetContacts(contacts);
        transform.Translate(contacts[0].normal * 0.4f);
    }
}
