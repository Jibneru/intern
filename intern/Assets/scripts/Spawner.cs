using UnityEngine;

// ミノをランダムに生成するスクリプト
public class Spawner : MonoBehaviour
{
    // NextPieceコンポーネントへの参照
    public NextPiece nextPiece;

    private void Start()
    {
        SpawnNext();
    }

    public void SpawnNext()
    {
        GameObject next = nextPiece.GetNextPiece();
        Instantiate(next, transform.position, Quaternion.identity);

        GameObject ghost = nextPiece.GetGhostPiece();
        Instantiate(ghost, next.transform.position, Quaternion.identity);
    }
}
