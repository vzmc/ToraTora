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
    private Collider2D _collider2D;

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
        _collider2D = GetComponent<Collider2D>();

        Direction = Vector2.right;
        velocity = Direction * speed;
        SetTransform(Direction);

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
                    _collider2D.enabled = false;
                    _transform.localPosition = Direction * radius;
                    break;
                }
            case State.Shoot:
                {
                    _collider2D.enabled = true;
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
        line.SetPosition(0, new Vector3(-(radius + length), 0, 0));
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
    }

    /// <summary>
    /// 发射钩爪
    /// </summary>
    public void Shoot()
    {
        if (state != State.Idle)
        {
            return;
        }

        UpdateState(State.Shoot);
    }

    public void Back()
    {
        if (state != State.Idle)
        {
            return;
        }

        UpdateState(State.Back);
    }

    /// <summary>
    /// 接受方向，设置位置和旋转
    /// </summary>
    /// <param name="direction">方向</param>
    public void SetTransform(Vector2 direction)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state != State.Shoot)
        {
            return;
        }

        switch (collision.transform.tag)
        {
            case "Enemy":
                {
                    break;
                }
            case "Block":
                {
                    UpdateState(State.Back);
                    break;
                }
            case "Wall":
                {
                    UpdateState(State.Back);
                    break;
                }
        }
    }
}