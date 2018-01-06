using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 相机移动控制
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D followTarget;

    private Vector3 delta;
    private Transform _transform;

    public Vector2 fixedVelocity;

    private void Start()
    {
        delta = transform.localPosition;
        _transform = transform;
    }

    private void Update()
    {
        _transform.Translate(fixedVelocity * Time.deltaTime);
    }

    //void FixedUpdate()
    //{
    //    //_transform.localPosition = followTarget.position;
    //    //_transform.localPosition += delta;

    //    //rigidbody2D.MovePosition(rigidbody2D.position + fixedVelocity * Time.fixedDeltaTime);
    //}
}
