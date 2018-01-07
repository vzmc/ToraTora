using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 追踪范围
/// </summary>
public class Track : MonoBehaviour
{
    private MoveAI moveAI;

    private void Start()
    {
        moveAI = GetComponentInParent<MoveAI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        moveAI.OnTrackEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        moveAI.OnTrackExit(collision);
    }
}
