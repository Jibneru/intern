using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // シングルトンインスタンス
    public static ScoreManager Instance { get; private set; }

    // スコア表示用のテキスト
    public Text scoreText;
    private int score;

    private void Awake()
    {
        // インスタンスを設定
        Instance = this;
    }

    private void Start()
    {
        UpdateScoreText();
    }

    // スコアを加算
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    // スコアの表示更新
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    // 現在のスコアを取得
    public int GetScore()
    {
        return score;
    }
}
