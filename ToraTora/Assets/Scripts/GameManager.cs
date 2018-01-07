using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏数据管理
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public int SelectedMode
    {
        get
        {
            return selectedMode;
        }
    }

    public int SelectedStage
    {
        get
        {
            return selectedStage;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }
    }

    private int selectedMode;   // 难易度选择，1普通，2简单
    private int selectedStage;  // 选择的关卡编号，0 ~ 4

    private int score;          // 获得的分数

    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 设置选择的难易度
    /// </summary>
    /// <param name="mode"></param>
    public void SelectMode(int mode)
    {
        selectedMode = mode;
    }

    /// <summary>
    /// 设置选择关卡
    /// </summary>
    /// <param name="stage"></param>
    public void SelectStage(int stage)
    {
        selectedStage = stage;
    }

    /// <summary>
    /// 设置通关时分数
    /// </summary>
    /// <param name="score"></param>
    public void SetScore(int score)
    {
        this.score = score;
    }
}
