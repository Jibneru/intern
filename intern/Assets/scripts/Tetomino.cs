using UnityEngine;

// ミノの動きを操作するスクリプト
public class Tetomino : MonoBehaviour
{
    // 落下に関する変数
    float fall = 0;
    public float fallSpeed = 1;

    private void Update()
    {
        CheckUserInput();
    }

    // ユーザー入力チェック
    void CheckUserInput()
    {
        // Aキーが押されたとき
        if (Input.GetKeyDown(KeyCode.A))
        {
            // ブロックを左に移動
            transform.position += new Vector3(-1, 0, 0);
        }
        // Dキーが押されたとき
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // ブロックを右に移動
            transform.position += new Vector3(1, 0, 0);
        }
        // Eキーが押されたとき
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // ブロックを回転
            transform.Rotate(0, 0, -90);
        }
        // Sキーが押されたとき
        else if(Input.GetKeyDown(KeyCode.S))
        {
            // ブロックを下に
            transform.position += new Vector3(0, -1, 0);
        }
    }
}
