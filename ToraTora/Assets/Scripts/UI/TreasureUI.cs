using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureUI : MonoBehaviour
{
    [SerializeField]
    private float downSpeed = 10;

    public void GoDie()
    {
        StartCoroutine(ToDie());
    }

    /// <summary>
    /// 移除宝箱时，宝箱下落消失
    /// </summary>
    /// <returns></returns>
    private IEnumerator ToDie()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 velocity = new Vector2(0, -downSpeed);

        while(rectTransform.anchoredPosition.y > -440)
        {
            rectTransform.anchoredPosition += velocity * Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
