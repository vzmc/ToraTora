using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 选择控制
/// </summary>
public class SelectController : MonoBehaviour
{
    [SerializeField]
    private RectTransform arrow;        // 选择箭头
    [SerializeField]
    private AudioClip selectSE;         // 切换选项时的音效
    [SerializeField]
    private AudioClip submitSE;         // 确定时的音效

    private RectTransform[] selects;    // 选项的位置数据

    private int selectIndex;            // 选择的index, 1 ~ 3;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        selectIndex = 1;
        selects = GetComponentsInChildren<RectTransform>();
        SetArrowPosition();
    }

    void Update()
    {
        SelectHandle();
        ConfirmSelect();
    }

    /// <summary>
    /// 上下操控选项
    /// </summary>
    private void SelectHandle()
    {
        bool v = Input.GetButtonDown("Vertical");
        float ve = Input.GetAxisRaw("Vertical");

        if (v)
        {
            if (ve < 0)
            {
                selectIndex++;
                if (selectIndex > selects.Length - 1)
                {
                    selectIndex = 1;
                }
                
            }
            else if (ve > 0)
            {
                selectIndex--;
                if (selectIndex < 1)
                {
                    selectIndex = selects.Length - 1;
                }
            }
            audioSource.Play();
            SetArrowPosition();
        }
    }

    /// <summary>
    /// 设置箭头的位置
    /// </summary>
    private void SetArrowPosition()
    {
        Vector2 pos = arrow.anchoredPosition;
        pos.y = selects[selectIndex].anchoredPosition.y;
        arrow.anchoredPosition = pos;
    }

    /// <summary>
    /// 确认选择
    /// </summary>
    private void ConfirmSelect()
    {
        if(Input.GetButtonDown("Submit"))
        {
            if(selectIndex == selects.Length-1)
            {
                return;
            }

            audioSource.clip = submitSE;
            audioSource.Play();

            Invoke("GotoPlayScene", 1f);
        }
    }

    /// <summary>
    /// 进入下一个场景
    /// </summary>
    private void GotoPlayScene()
    {
        GameManager.Instance.SelectMode(selectIndex);
        SceneManager.LoadScene(2);
    }
}
