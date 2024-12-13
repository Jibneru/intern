using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public static class Score
{
    public static int score;
    public static int highScore;
}

public class ScoreManager : MonoBehaviour
{
    // PlayerPrefsで使用するキー
    private const string HighScoreKey = "HighScore";

    // スコア表示用のテキスト
    [SerializeField] Text scoreText;
    [SerializeField] Text highScoreText;

    private void Start()
    {
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
        Score.score += points;

        if (Score.score > Score.highScore)
        {
            Score.highScore = Score.score;
            SaveHighScore();
        }

        UpdateScoreText();
    }

    // スコアの表示更新
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + Score.score.ToString();
        highScoreText.text = "High Score: " + Score.highScore.ToString();
    }

    // 最大スコアを保存
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt(HighScoreKey, Score.highScore);
        PlayerPrefs.Save();
    }

    // 最大スコアを読み込む
    private void LoadHighScore()
    {
        if (PlayerPrefs.HasKey(HighScoreKey))
        {
            Score.highScore = PlayerPrefs.GetInt(HighScoreKey);
        }
        else
        {
            Score.highScore = 0;
        }
    }

    // 最大スコアをリセット
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey(HighScoreKey);
        Score.highScore = 0;
    }
}
