using UnityEngine;

// ミノのホールド昨日を制御するスクリプト
public class HoldPiece : MonoBehaviour
{
    // ホールドされたミノ
    private GameObject holdTetomino;

    // ホールド位置
    [SerializeField] Transform hodlPosition;

    public void Hold(GameObject currentTetomino)
    {
        // グリッドから現在のミノの情報を削除
        Grid.Instance.GridClear(currentTetomino.transform);

        // ホールドが空の時
        if (holdTetomino == null)
        {
            holdTetomino = currentTetomino;
            currentTetomino.transform.position = hodlPosition.position;
            Destroy(currentTetomino.GetComponent<Tetomino>());

            // 新しいミノを生成
            FindAnyObjectByType<Spawner>().SpawnNext();
        }
        // ホールド済みのミノがある場合
        else
        {
            // 一時的にホールド中のミノを取り出す
            GameObject temp = holdTetomino;
            
            // 前のミノを削除
            Destroy(holdTetomino);

            holdTetomino = currentTetomino;
            currentTetomino.transform.position = hodlPosition.position;
            Destroy(currentTetomino.GetComponent<Tetomino>());

            // ホールドから出たミノを生成
            GameObject spawnedMino = Instantiate(temp, FindAnyObjectByType<Spawner>().transform.position, Quaternion.identity);
            spawnedMino.AddComponent<Tetomino>();
            Tetomino spawnedScript  = spawnedMino.GetComponent<Tetomino>();

            // ホールドから出たミノは再びホールドできない
            spawnedScript.canHold = false;
        }
    }
}
