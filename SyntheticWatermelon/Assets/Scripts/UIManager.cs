using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    // 分数UI文本组件
    private Text scoreText;
    private int score;

    public int Score { get => score;
        set {
            score = value;
            UpdateScoreText(score);
        }
    }
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        scoreText = transform.Find("ScoreText").GetComponent<Text>();
        Score = 0;
    }
    /// <summary>
    /// 更新分数文本
    /// </summary>
    void UpdateScoreText(int score )
    {
        scoreText.text = score.ToString();
    }
}
