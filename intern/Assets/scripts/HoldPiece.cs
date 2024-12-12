using UnityEngine;

// ミノのホールド機能を制御するスクリプト
public class HoldPiece : MonoBehaviour
{
    // ホールドされたミノ
    private GameObject holdTetomino;

    // ホールド位置
    [SerializeField] Transform hodlPosition;

    public NextPiece nextPiece;

    public void Hold(GameObject currentTetomino)
    {
        // グリッドから現在のミノの情報を削除
        Grid.Instance.ClearGrid(currentTetomino.transform);

        // ホールドが空の時
        if (holdTetomino == null)
        {
            holdTetomino = currentTetomino;
            holdTetomino.transform.position = hodlPosition.position;
            Destroy(currentTetomino.GetComponent<Tetomino>());

            // 新しいミノを生成
            nextPiece.SpawnNextPiece();
        }
        // ホールド済みのミノがある場合
        else
        {
            // 一時的にホールド中のミノを取り出す
            GameObject temp = holdTetomino;
            
            // 前のミノを削除
            Destroy(holdTetomino);

            holdTetomino = currentTetomino;
            holdTetomino.transform.position = hodlPosition.position;
            Destroy(currentTetomino.GetComponent<Tetomino>());

            // ホールドから出たミノを操作ミノに登録
            nextPiece.CreatePiece(temp, true);

            nextPiece.UpdateGhostBlock();
        }
    }
}
