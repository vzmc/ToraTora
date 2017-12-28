using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 最初的界面的控制
/// </summary>
public class CheckProgress : MonoBehaviour
{
    [SerializeField]
    private Text romCheck;
    [SerializeField]
    private Text ramCheck;
    [SerializeField]
    private Text backup;

    private const string okStr = "OK";

    private void Start()
    {
        StartCoroutine(Loading());
    }

    /// <summary>
    /// 显示的流程
    /// </summary>
    /// <returns></returns>
    private IEnumerator Loading()
    {
        int num = 0;
        string numStr;

        while (num < 16)
        {
            numStr = Convert.ToString(num, 16).ToUpper();
            romCheck.text = numStr + numStr;
            num++;
            yield return new WaitForSeconds(0.01f);
        }
        romCheck.text = okStr;
        yield return new WaitForSeconds(0.1f);

        num = 0;
        while (num < 16)
        {
            numStr = Convert.ToString(num, 16).ToUpper();
            ramCheck.text = numStr + numStr;
            num++;
            yield return new WaitForSeconds(0.03f);
        }
        ramCheck.text = okStr;
        yield return new WaitForSeconds(0.5f);

        backup.text += okStr;
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(1);
    }
}
