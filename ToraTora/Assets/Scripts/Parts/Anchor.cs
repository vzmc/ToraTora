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
        UpdateLine();
    }

    private void Update()
    {
        if (state != State.Idle)
        {
            UpdateAnchorTransform();
            UpdateLine();
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
    private void UpdateAnchorTransform()
    {
        switch (state)
        {
            case State.Shoot:
                {
                    _transform.Translate(velocity * Time.deltaTime, Space.Self);
                    length += speed * Time.deltaTime;
                    if (length >= maxLength)
                    {
                        UpdateState(State.Back);
                    }
                    break;
                }
            case State.Back:
                {
                    _transform.Translate(-velocity * Time.deltaTime, Space.Self);
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

    /// <summary>
    /// 收回勾爪
    /// </summary>
    public void Back()
    {
        if (state != State.Shoot)
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

    /// <summary>
    /// 碰撞检测，根据tag判断对方类型，执行不同操作
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == State.Idle)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Enemy":
                {
                    Basic basic = collision.gameObject.GetComponent<Basic>();
                    AttackEnemy(basic);
                    break;
                }
            case "Block":
                {
                    Back();
                    break;
                }
            case "Wall":
                {
                    Back();
                    break;
                }
        }
    }

    /// <summary>
    /// 攻击敌人
    /// </summary>
    /// <param name="basic">敌人的基本数据</param>
    private void AttackEnemy(Basic basic)
    {
        switch (basic.WeekPoint)
        {
            case WeekPoint.Whole:
                {
                    basic.TakeDamage(1);
                    break;
                }
            case WeekPoint.Bottom:
                {
                    if (Direction.y > 0)
                    {
                        basic.TakeDamage(1);
                    }
                    else
                    {
                        Back();
                    }
                    break;
                }
            case WeekPoint.Top:
                {
                    if (Direction.y < 0)
                    {
                        basic.TakeDamage(1);
                    }
                    else
                    {
                        Back();
                    }
                    break;
                }
            case WeekPoint.None:
                {
                    Back();
                    break;
                }
        }
    }
}