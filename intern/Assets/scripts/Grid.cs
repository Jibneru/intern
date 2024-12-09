using UnityEngine;

public class Grid : MonoBehaviour
{
    // シングルトンインスタンス
    public static Grid Instance {  get; private set; }

    // グリッドの幅と高さ
    public static int width = 10;
    public static int height = 20;

    // グリッドを格納する2次元配列
    public static Transform[,] grid;
    private const float lineOffset = -0.5f;

    private void Awake()
    {
        //シングルトンがすでにある場合このオブジェクトを削除
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // インスタンスを設定し、DontDestroyOnLoadでシーン間で破棄されないようにする
            Instance = this;
            grid = new Transform[width, height];
            DontDestroyOnLoad(gameObject);
        }
    }
}
