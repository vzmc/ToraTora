using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneManager : MonoBehaviour
{
    private static PlaySceneManager instance;
    public static PlaySceneManager Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType<PlaySceneManager>();
            }
            if(!instance)
            {
                Debug.LogError("场景中不存在PlaySceneManager");
            }
            return instance;
        }
    }

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private TreasurePanel treasurePanel;

    private int score;                      // 分数
    private int gemConut;                   // 宝石数量
    private List<Transform> treasureList;   // 储存宝箱的List

    /// <summary>
    /// 增加分数
    /// </summary>
    /// <param name="add"></param>
    public void AddScore(int add)
    {
        score += add;
        scoreText.text = "Score:" + score;
    }

    /// <summary>
    /// 获得一个宝箱
    /// </summary>
    /// <param name="isBig">是否为大宝箱</param>
    public void AddTreasure(bool isBig)
    {
        if (isBig)
        {
            treasurePanel.AddBigTreasure();
        }
        else
        {
            treasurePanel.AddLittleTreasure();
        }
    }

    /// <summary>
    /// 丢失一个宝箱
    /// </summary>
    public void RemoveTreasure()
    {
        treasurePanel.RemoveTreasure();
    }
}
