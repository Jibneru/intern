using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // シングルトンインスタンス
    public static ScoreManager Instance { get; private set; }

    public Text scoreText;

    private int score;

    private void Awake()
    {
        // AssertでInstanceがあるとエラーを出す
        Assert.IsTrue(Instance == null);

        // インスタンスを設定し、DontDestroyOnLoadでシーン間で破棄されないようにする
        Instance = this;
        DontDestroyOnLoad(gameObject);
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
            scoreText.text = "Score: " + score;
        }
    }

    // 現在のスコアを取得
    public int GetScore()
    {
        return score;
    }
}
