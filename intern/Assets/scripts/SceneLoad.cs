using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// シーン切り替えを行うスクリプト
public class SceneLoad : MonoBehaviour
{
    // シーンの名前を入力する
    [SerializeField] string sceneName;

    // キーが押された時に反映するか
    [SerializeField] bool isPushKey = true;

    // フェード用のUIイメージ
    [SerializeField] Image fadeImage;

    // フェード時間
    [SerializeField] float fadeDuration = 1.0f;

    private void Start()
    {
        // 開始時にフェードイン
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            _ = FadeInAsync();
        }
    }

    private void Update()
    {
        if (isPushKey)
        {
            // エンターキーか左クリックを押したときにシーンを切り替える
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Return))
            {
                // 完了するまで実行を中断しながらロードする
                SceneLoading();
            }
        }
    }

    public async void SceneLoading()
    {
        if (fadeImage != null)
        {
            await FadeOutAsync();
        }

        await SceneManager.LoadSceneAsync(sceneName);
    }

    // フェードイン
    private async Task FadeInAsync()
    {
        float elapsedTime = 0.0f;
        Color color = fadeImage.color;
        // 不透明からスタート
        color.a = 1.0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // 徐々に透明に
            color.a = Mathf.Lerp(1.0f, 0.0f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            // 次のフレームまで待機
            await Task.Yield();
        }

        // 透明にする
        color.a = 0.0f;
        fadeImage.color = color;
        // フェードイメージ非表示
        fadeImage.gameObject.SetActive(false);
    }

    // フェードアウト
    private async Task FadeOutAsync()
    {
        // フェードイメージを表示
        fadeImage?.gameObject.SetActive(true);
        float elapsedTime = 0.0f;
        Color color = fadeImage.color;
        // 透明からスタート
        color.a = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // 徐々に透明に
            color.a = Mathf.Lerp(0.0f, 1.0f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            // 次のフレームまで待機
            await Task.Yield();
        }

        // 不透明にする
        color.a = 1.0f;
        fadeImage.color = color;
    }
}
