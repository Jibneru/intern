using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Assertions;

// テトリスのマス目のスクリプト
public class Grid : MonoBehaviour
{
    // シングルトンインスタンス
    public static Grid Instance {  get; private set; }

    // 壁用のゲームオブジェクト
    [SerializeField] GameObject cubeBlock;

    // グリッドの幅と高さ
    // Tetomino.csで使いたいため
    public const int width = 10;
    public const int height = 20;

    // グリッドを格納する2次元配列
    public static Transform[,] grid;
    private const float lineOffset = -0.5f;

    private void Awake()
    {
        // AssertでInstanceがあるとエラーを出す
        Assert.IsTrue(Instance == null);

        // インスタンスを設定し、DontDestroyOnLoadでシーン間で破棄されないようにする
        Instance = this;
        grid = new Transform[width, height];
        DontDestroyOnLoad(gameObject);
        
    }

    private void Start()
    {
        // 横一列に壁生成
        for (int i = 0; i < width; i++)
        {
            Instantiate(cubeBlock, new Vector3(i, -1.0f, 0), Quaternion.identity);
        }

        // 縦一列に生成（左右）
        for (int i = 0; i < height; i++)
        {
            Instantiate(cubeBlock, new Vector3(-1.0f, i - 1.0f, 0), Quaternion.identity);
        }
        for (int i = 0; i < height; i++)
        {
            Instantiate(cubeBlock, new Vector3(width, i - 1.0f, 0), Quaternion.identity);
        }
    }

    // ベクトルを整数にする
    public Vector2 RoundVector2(Vector2 v)
    {
        // 数値は四捨五入しておく
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    // 指定された位置がグリッド内にあるかをチェック
    public bool InsideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0);
    }

    // 行が埋まっているかチェック
    private bool IsRowFull(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null) return false;
        }

        return true;
    }

    // 指定した行を削除
    private void DeleteRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    // 指定された行を下に降ろす
    private void DescendRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] != null)
            {
                // ブロックを一段下げる
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    // 指定された行から上を下に降ろす
    private void DescendRowsAbove(int y)
    {
        for (int i = y; i < height; i++)
        {
            DescendRow(i);
        }
    }

    // 完全に埋まった行を削除し、上の行を一段下げる
    public void DeleteFullRows()
    {
        for (int y = 0; y < height; y++)
        {
            if (IsRowFull(y))
            {
                DeleteRow(y);
                DescendRowsAbove(y);
            }
        }
    }

    // グリッド情報の更新
    public void UpdateGrid(Transform t)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y].parent == t)
                    {
                        grid[x, y] = null;
                    }
                }
            }
        }

        foreach (Transform child in t)
        {
            Vector2 v = RoundVector2(child.position);
            grid[(int)v.x, (int)v.y] = child;
        }
    }

    // 境界線を描画するメソッド
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(new Vector3(lineOffset, lineOffset, 0), new Vector3(lineOffset, height + lineOffset, 0));
        Gizmos.DrawLine(new Vector3(width + lineOffset, lineOffset, 0), new Vector3(width + lineOffset, height + lineOffset, 0));
        Gizmos.DrawLine(new Vector3(lineOffset, lineOffset, 0), new Vector3(width + lineOffset, lineOffset, 0));
    }
}
