using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Text scoreText;
    private int scoreResult;

    private void Start()
    {
        scoreResult = ScoreManager.Instance.GetScore();

        scoreText.text = "Score:" + scoreResult.ToString();
    }
}
