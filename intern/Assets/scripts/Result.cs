using UnityEngine;
using UnityEngine.UI;

// リザルト表示用スクリプト
public class Result : MonoBehaviour
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
        PlayerPrefs.GetInt(ScoreKey, score);
        PlayerPrefs.GetInt(HighScoreKey, highScore);
        scoreText.text = "Score:" + score.ToString();
        highScoreText.text = "High Score:" + highScore.ToString();
    }
}
