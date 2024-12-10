using UnityEngine;
using UnityEngine.SceneManagement;

// シーン切り替えを行うスクリプト
public class SceneLoad : MonoBehaviour
{
    // シーンの名前を入力する
    [SerializeField] string sceneName;

    private void Update()
    {
        // 仮でスペースを押したときにシーンを切り替える
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
