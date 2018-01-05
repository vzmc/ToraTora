using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    [SerializeField]
    private Renderer _renderer;
    [SerializeField]
    private LineRenderer line;
    [SerializeField]
    private float maxLength = 3;
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float radius = 0.5f;

    private Transform parentTransform;
    private Transform _transform;
    private Rigidbody2D rigidbody2D;
    private Collider2D collider2D;

    private float length;
    private Vector2 velocity;

    /// <summary>
    /// 钩抓的方向
    /// </summary>
    public Vector2 Direction { get; private set; }

    /// <summary>
    /// 定义状态枚举
    /// </summary>
    private enum State
    {
        None,
        Idle,
        Shoot,
        Back,
    }

    private State state;

    private void Start()
    {
        _transform = transform;
        parentTransform = _transform.parent;
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();

        Direction = Vector2.right;
        velocity = Direction * speed;
        SetPositionAndRotation(Direction);

        UpdateState(State.Idle);     
    }

    private void Update()
    {
        UpdateLine();
    }

    private void FixedUpdate()
    {
        if (state != State.Idle)
        {
            UpdateAnchor();
        }
    }

    /// <summary>
    /// 更新状态
    /// </summary>
    /// <param name="s"></param>
    private void UpdateState(State s)
    {
        if(state == s)
        {
            return;
        }

        state = s;

        switch (state)
        {
            case State.Idle:
                {
                    length = 0;
                    collider2D.enabled = false;
                    _transform.localPosition = Direction * radius;
                    break;
                }
            case State.Shoot:
                {
                    collider2D.enabled = true;
                    break;
                }
            case State.Back:
                {
                    break;
                }
        }
    }

    /// <summary>
    /// 更新Line的状态
    /// </summary>
    private void UpdateLine()
    {
        line.SetPosition(0, parentTransform.position);
        line.SetPosition(1, _transform.position);
    }

    /// <summary>
    /// 更新钩爪的状态
    /// </summary>
    private void UpdateAnchor()
    {
        switch (state)
        {
            case State.Shoot:
                {
                    _transform.Translate(velocity * Time.fixedDeltaTime, Space.Self);
                    length += speed * Time.deltaTime;
                    if (length >= maxLength)
                    {
                        UpdateState(State.Back);
                    }
                    break;
                }
            case State.Back:
                {
                    _transform.Translate(-velocity * Time.fixedDeltaTime, Space.Self);
                    length -= speed * Time.deltaTime;
                    if (length <= 0)
                    {
                        UpdateState(State.Idle);
                    }
                    break;
                }
        }

        Debug.Log(length);
    }

    /// <summary>
    /// 发射钩爪
    /// </summary>
    public void Shoot(Vector2 direction)
    {
        if (state != State.Idle)
        {
            return;
        }

        UpdateState(State.Shoot);
    }

    

    /// <summary>
    /// 接受方向，设置位置和旋转
    /// </summary>
    /// <param name="direction">方向</param>
    public void SetPositionAndRotation(Vector2 direction)
    {
        if(state != State.Idle)
        {
            return;
        }

        if (direction.sqrMagnitude > float.Epsilon)
        {
            Direction = direction;
            _transform.localRotation = Quaternion.FromToRotation(Vector2.right, direction);
            _transform.localPosition = direction * radius;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state != State.Shoot)
        {
            return;        
        }

        switch(collision.tag)
        {
            case "Player":
                {
                    break;
                }
            default:
                {
                    UpdateState(State.Back);
                    break;
                }
        }
    }
}