using UnityEngine;
using UnityEngine.UI;

// リザルト表示用スクリプト
public class Result : MonoBehaviour
{
    // スコア表示用のテキスト
    [SerializeField] Text scoreText;
    [SerializeField] Text highScoreText;

    private void Start()
    {
        scoreText.text = "Score:" + Score.score.ToString();
        highScoreText.text = "High Score:" + Score.highScore.ToString();
    }
}
