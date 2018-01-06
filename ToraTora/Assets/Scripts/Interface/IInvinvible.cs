using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可无敌状态
/// </summary>
public interface IInvinvible
{
    IEnumerator Invincible(float duration);
}
