using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;

    private Vector2 velocity;
    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        velocity.Set(h, v);
        velocity.Normalize();
        velocity *= moveSpeed;
    }

    private void FixedUpdate()
    {
        if(velocity.sqrMagnitude <= float.Epsilon)
        {
            return;
        }

        rigidbody2D.MovePosition(rigidbody2D.position + velocity * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[1];
        int contactCount;
        contactCount = collision.GetContacts(contacts);
        transform.Translate(contacts[0].normal * 0.5f);
    }
}
