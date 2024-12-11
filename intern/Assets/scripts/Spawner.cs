using UnityEngine;

// ミノをランダムに生成するスクリプト
public class Spawner : MonoBehaviour
{
    // NextPieceコンポーネントへの参照
    public NextPiece nextPiece;

    public GameObject ghostPrefab;

    private void Start()
    {
        SpawnNext();
    }

    public void SpawnNext()
    {
        GameObject next = nextPiece.GetNextPiece();
        Instantiate(next, transform.position, Quaternion.identity);

        GameObject ghost = nextPiece.GetGhostPiece();
        GameObject ghostObject = Instantiate(ghost, transform.position, Quaternion.identity);
        ghostObject.GetComponent<GhostBlock>().parentTetomino = next.transform;
    }
}
