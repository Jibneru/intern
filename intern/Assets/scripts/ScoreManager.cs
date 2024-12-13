using System.Diagnostics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // PlayerPrefsで使用するキー
    private const string ScoreKey = "Score";
    private const string HighScoreKey = "HighScore";

    // スコア表示用のテキスト
    [SerializeField] Text scoreText;
    [SerializeField] Text highScoreText;

    public int score;
    public int highScore;

    private void Start()
    {
        score = 0;
        LoadHighScore();
        UpdateScoreText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ResetHighScore();
        }
    }

    // スコアを加算
    public void AddScore(int points)
    {
        score += points;
        PlayerPrefs.SetInt(ScoreKey, score);
        PlayerPrefs.Save();

        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
        }

        UpdateScoreText();
    }

    // スコアの表示更新
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    // 最大スコアを保存
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt(HighScoreKey, highScore);
        PlayerPrefs.Save();
    }

    // 最大スコアを読み込む
    private void LoadHighScore()
    {
        if (PlayerPrefs.HasKey(HighScoreKey))
        {
            PlayerPrefs.GetInt(HighScoreKey, Score.highScore);
        }
        else
        {
            Score.highScore = 0;
        }
    }

    // 最大スコアをリセット
    [Conditional("UNITY_EDITOR")]
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey(HighScoreKey);
        Score.highScore = 0;
    }
}
