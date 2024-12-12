using UnityEngine;
using UnityEngine.SceneManagement;

// シーン切り替えを行うスクリプト
public class SceneLoad : MonoBehaviour
{
    // シーンの名前を入力する
    [SerializeField] string sceneName;

    [SerializeField] bool isPushKey = true;

    private void Update()
    {
        if (isPushKey)
        {
            // 仮でスペースを押したときにシーンを切り替える
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Return))
            {
                // 完了するまで実行を中断しながらロードする
                SceneLoading();
            }
        }
    }

    public async void SceneLoading()
    {
        await SceneManager.LoadSceneAsync(sceneName);
    }
}
