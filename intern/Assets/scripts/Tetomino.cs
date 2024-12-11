using UnityEngine;

// ミノの動きを操作するスクリプト
public class Tetomino : MonoBehaviour
{
    // 落下に関する変数
    float fall = 0;
    [SerializeField] float fallSpeed = 1;

    private void Update()
    {
        CheckUserInput();

        // 自動落下処理
        if (Time.time - fall >= fallSpeed)
        {
            if (!Fall())
            {
                // 完全に埋まった行を削除
                Grid.Instance.DeleteFullRows();

                // 新しいミノを生成
                FindAnyObjectByType<Spawner>().SpawnNext();
                enabled = false;
            }
        }
    }

    // 自動または手動での落下処理
    // ブロックを下に移動させる時に下に何もなければ:true
    // 移動させる時にグリッドの範囲外や設置されているミノがあれば:false
    private bool Fall()
    {
        // ブロックを下に
        transform.position += new Vector3(0, -1, 0);

        // 位置が有効かチェック
        if (!IsValidGridPos())
        {
            // 位置が無効なら戻す
            transform.position += new Vector3(0, 1, 0);

            // グリッド更新
            Grid.Instance.UpdateGrid(transform);

            return false;
        }
        else
        {
            // 位置が有効ならグリッドを更新
            Grid.Instance.UpdateGrid(transform);
        }

        // 落下時間をリセット
        fall = Time.time;
        return true;
    }

    // ハードドロップ処理
    void HardDrop()
    {
        while (IsValidGridPos())
        {
            transform.position += new Vector3(0, -1, 0);
        }

        // 位置が無効なら戻す
        transform.position += new Vector3(0, 1, 0);

        // グリッド更新
        Grid.Instance.UpdateGrid(transform);

        // 完全に埋まった行を削除
        Grid.Instance.DeleteFullRows();

        // 新しいミノを生成
        FindAnyObjectByType<Spawner>().SpawnNext();
        enabled = false;
    }

    // ユーザー入力チェック
    void CheckUserInput()
    {
        // Aキーが押されたとき
        if (Input.GetKeyDown(KeyCode.A))
        {
            // ブロックを左に移動
            transform.position += new Vector3(-1, 0, 0);

            // 位置が有効かチェック
            if (!IsValidGridPos())
            {
                // 位置が無効なら戻す
                transform.position += new Vector3(1, 0, 0);
            }
            else
            {
                // 位置が有効ならグリッドを更新
                Grid.Instance.UpdateGrid(transform);
            }
                
        }
        // Dキーが押されたとき
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // ブロックを右に移動
            transform.position += new Vector3(1, 0, 0);

            // 位置が有効かチェック
            if (!IsValidGridPos())
            {
                // 位置が無効なら戻す
                transform.position += new Vector3(-1, 0, 0);
            }
            else
            {
                // 位置が有効ならグリッドを更新
                Grid.Instance.UpdateGrid(transform);
            }
        }
        // Eキーが押されたとき
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // ブロックを右回転
            transform.Rotate(0, 0, -90);

            // 位置が有効かチェック
            if (!IsValidGridPos())
            {
                // 位置が無効なら戻す
                transform.Rotate(0, 0, 90);
            }
            else
            {
                // 位置が有効ならグリッドを更新
                Grid.Instance.UpdateGrid(transform);
            }
        }
        // Qキーが押されたとき
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // ブロックを左回転
            transform.Rotate(0, 0, 90);

            // 位置が有効かチェック
            if (!IsValidGridPos())
            {
                // 位置が無効なら戻す
                transform.Rotate(0, 0, -90);
            }
            else
            {
                // 位置が有効ならグリッドを更新
                Grid.Instance.UpdateGrid(transform);
            }
        }
        // Sキーが押されたとき（ソフトドロップ）
        else if (Input.GetKeyDown(KeyCode.S))
        {
            fallSpeed = 0.1f; // 落下速度を早くする
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            fallSpeed = 1.0f; // 落下速度を元に戻す
        }
        // LeftShiftキーが押されたとき（ハードドロップ）
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            HardDrop();
        }
    }

    // 移動した先が範囲外や設置されたミノがないか判定
    bool IsValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.Instance.RoundVector2(child.position);

            if (!Grid.Instance.InsideBorder(v)) return false;

            if (Grid.grid[(int)v.x, (int)v.y] != null &&
                Grid.grid[(int)v.x, (int)v.y].parent != transform) return false;
        }

        return true;
    }
}
