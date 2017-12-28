using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneManager : MonoBehaviour
{
    public static PlaySceneManager instance;
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

    private int score;

    public void AddScore(int add)
    {
        score += add;
    }
}
