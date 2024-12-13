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

    private void Start()
    {
        scoreText.text = "Score:" + PlayerPrefs.GetInt(ScoreKey).ToString();
        highScoreText.text = "High Score:" + PlayerPrefs.GetInt(HighScoreKey).ToString();
    }
}
