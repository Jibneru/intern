using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// シーン切り替えを行うスクリプト
public class SceneLoad : MonoBehaviour
{
    // シーンの名前を入力する
    [SerializeField] string sceneName;

    [SerializeField] bool isPushKey = true;

    private async void Update()
    {
        if (isPushKey)
        {
            // 仮でスペースを押したときにシーンを切り替える
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 完了するまで実行を中断しながらロードする
                await SceneManager.LoadSceneAsync(sceneName);
            }
        }
    }

    public async void SceneLoading()
    {
        await SceneManager.LoadSceneAsync(sceneName);
    }
}
