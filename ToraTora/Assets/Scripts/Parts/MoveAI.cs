using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI控制移动
/// </summary>
[RequireComponent(typeof(Basic), typeof(Movement))]
public class MoveAI : MonoBehaviour
{
    [SerializeField]
    private float moveDistance;
    [SerializeField]
    private bool isTrackable;
    [SerializeField]
    private float trackDistance;

    private Basic basic;
    private Movement movement;

    private float distance;
    private Vector2 startTrackPos;
    private Quaternion startTrackRotation;
    private bool isTracking;

    private Transform target;

    private void Start()
    {
        basic = GetComponent<Basic>();
        movement = GetComponent<Movement>();
        movement.Move(Vector2.right);
    }

    private void Update()
    {
        if (basic.IsInvincible || basic.IsDead)
        {
            return;
        }

        if (isTracking)
        {
            if (target)
            {
                Track(target);
            }
            else
            {
                Track(startTrackPos);
            }
            return;
        }

        distance += movement.MoveSpeed * Time.deltaTime;
        if (distance >= moveDistance)
        {
            transform.Rotate(0, 180, 0);
            distance = 0;
        }
    }

    /// <summary>
    /// 进入追踪模式
    /// </summary>
    private void EnterTracking()
    {
        if (!isTracking)
        {
            startTrackPos = transform.localPosition;
            startTrackRotation = transform.localRotation;
            movement.enabled = false;
            isTracking = true;
        }
    }

    /// <summary>
    /// 离开追踪模式
    /// </summary>
    private void ExitTracking()
    {
        transform.localPosition = startTrackPos;
        transform.localRotation = startTrackRotation;
        movement.enabled = true;
        isTracking = false;
    }

    /// <summary>
    /// 追踪移动目标
    /// </summary>
    /// <param name="target">目标</param>
    private void Track(Transform target)
    {
        Vector3 dir = (target.transform.position - transform.localPosition).normalized;

        if (Vector3.Dot(transform.right, dir) < 0)
        {
            transform.Rotate(0, 180, 0);
        }

        transform.Translate(transform.InverseTransformDirection(dir) * (movement.MoveSpeed * Time.deltaTime));
    }

    /// <summary>
    /// 追踪固定位置
    /// </summary>
    /// <param name="target">位置</param>
    private void Track(Vector3 target)
    {
        Vector3 dir = (target - transform.localPosition).normalized;

        if (Vector3.Dot(transform.right, dir) < 0)
        {
            transform.Rotate(0, 180, 0);
        }

        transform.Translate(transform.InverseTransformDirection(dir) * (movement.MoveSpeed * Time.deltaTime));

        if(Vector3.Distance(target, transform.localPosition) < 0.1f)
        { 
            ExitTracking();
        }
    }

    /// <summary>
    /// 玩家进入追踪范围
    /// </summary>
    /// <param name="collision"></param>
    public void OnTrackEnter(Collider2D collision)
    {
        if (!isTrackable)
            return;
        if(collision.CompareTag("Player"))
        {
            target = collision.transform;
            EnterTracking();
        }
    }

    /// <summary>
    /// 玩家离开追踪范围
    /// </summary>
    /// <param name="collision"></param>
    public void OnTrackExit(Collider2D collision)
    {
        if (!isTrackable)
            return;
        if (collision.CompareTag("Player"))
        {
            target = null;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //if(collision.gameObject.CompareTag("Anchor"))
    //    //{
    //    //    return;
    //    //}

    //    //ContactPoint2D[] contacts = new ContactPoint2D[1];
    //    //int contactCount;
    //    //contactCount = collision.GetContacts(contacts);
    //    //transform.Translate(contacts[0].normal * 0.4f);
    //}
}