using UnityEngine;
using UnityEngine.Assertions;

// テトリスのマス目のスクリプト
public class Grid : MonoBehaviour
{
    // シングルトンインスタンス
    public static Grid Instance {  get; private set; }

    // グリッドの幅と高さ
    // Tetomino.csで使いたいため
    public const int width = 10;
    public const int height = 20;

    // グリッドを格納する2次元配列
    public static Transform[,] grid;
    private const float lineOffset = -0.5f;

    private void Awake()
    {
        //シングルトンがすでにある場合このオブジェクトを削除

        Assert.IsTrue(Instance.enabled);

        // インスタンスを設定し、DontDestroyOnLoadでシーン間で破棄されないようにする
        Instance = this;
        grid = new Transform[width, height];
        DontDestroyOnLoad(gameObject);
        
    }
}
