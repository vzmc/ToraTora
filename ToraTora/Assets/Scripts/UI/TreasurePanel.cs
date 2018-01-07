using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasurePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject treasurePrefab;
    [SerializeField]
    private Transform littlesPanel;
    [SerializeField]
    private int littleAmount = 8;           // 小宝箱数量

    private List<Transform> treasureList;   // 储存宝箱的List

    private void Start()
    {
        treasureList = new List<Transform>();
    }

    /// <summary>
    /// 添加一个小宝箱
    /// </summary>
    public void AddLittleTreasure()
    {
        GameObject treasure = Instantiate(treasurePrefab, littlesPanel);
        treasureList.Add(treasure.transform);
    }

    /// <summary>
    /// 添加大宝箱
    /// </summary>
    public void AddBigTreasure()
    {
        GameObject treasure = Instantiate(treasurePrefab, transform);
        treasure.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 60);
        treasureList.Add(treasure.transform);
    }

    /// <summary>
    /// 移除一个宝箱
    /// </summary>
    public void RemoveTreasure()
    {
        if(treasureList.Count == 0)
        {
            return;
        }

        Transform lastTreasure = treasureList[treasureList.Count - 1];

        lastTreasure.SetParent(transform, true);
        lastTreasure.GetComponent<TreasureUI>().GoDie();
        treasureList.Remove(lastTreasure);
    }
}
