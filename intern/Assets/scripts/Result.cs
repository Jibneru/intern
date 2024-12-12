using UnityEngine;
using UnityEngine.UI;

// リザルト表示用スクリプト
public class Result : MonoBehaviour
{
    // スコア表示用のテキスト
    public Text scoreText;
    private int scoreResult;

    private void Start()
    {
        scoreResult = ScoreManager.Instance.GetScore();

        scoreText.text = "Score:" + scoreResult.ToString();
    }
}
