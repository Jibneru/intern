using System.Collections;
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
            // コルーチンを使ってシーンをロード
            StartCoroutine(LoadAsyncScene());
        }
    }

    private IEnumerator LoadAsyncScene()
    {
        // バックグラウンドでシーンをロード
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // 非同期シーンが完全にロードされるまで待つ
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
