using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 相机移动控制
/// </summary>
public class CameraController : MonoBehaviour
{
    private Transform _transform;

    public Vector2 fixedVelocity;

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.Translate(fixedVelocity * Time.deltaTime);
    }
}