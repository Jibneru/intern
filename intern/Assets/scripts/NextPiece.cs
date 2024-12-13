using System.Collections.Generic;
using UnityEngine;

public class NextPiece : MonoBehaviour
{
    // ミノのプレファブリスト
    public GameObject[] tetominoes;

    // ゴーストのプレファブ
    public GameObject ghostPrefab;

    // 後から追加されて前から取り出したいためQueueを使用
    Queue<GameObject> nextQueue = new Queue<GameObject>();

    // 次のミノを表示する位置の配列
    public Transform[] previewPositions;

    // 表示中のプレビューを管理
    private List<GameObject> currentPreviews = new List<GameObject>();

    // プレビュー用のスケール
    private Vector3 previewScale = new Vector3(0.5f, 0.5f, 1.0f);

    // 現在の操作ミノ
    private GameObject currentTetomino;

    // ゴーストブロック
    private GameObject ghostTetomino;

    private void Start()
    {
        // 初めは表示する個数分だけ生成
        for (int i = 0; i < previewPositions.Length; i++)
        {
            AddNextPieceToQueue();
        }
        
        // 最初の操作ミノを生成
        SpawnNextPiece();
    }

    // 次の操作ミノを生成
    public void SpawnNextPiece()
    {
        // キューから次のミノを取得
        GameObject nextTetomino = nextQueue.Dequeue();
        // 新しいNextミノをキューに追加
        AddNextPieceToQueue();

        // 操作ミノを生成
        CreatePiece(nextTetomino);

        // ゴーストブロックの更新
        UpdateGhostBlock();

        UpdatePreviews();
    }

    // 操作するミノを生成(ホールドから出てきた時はtureにする)
    public void CreatePiece(GameObject tetomino, bool isHold = false)
    {
        currentTetomino = Instantiate(tetomino, transform.position, Quaternion.identity);

        if (!IsValidGridPos(currentTetomino.transform))
        {
            FindAnyObjectByType<SceneLoad>().SceneLoading();
            Destroy(currentTetomino);
            Destroy(ghostTetomino);
            return;
        }

        if (isHold)
        {
            currentTetomino.AddComponent<Tetomino>();

            // ホールドから出たミノは再びホールドできない
            currentTetomino.GetComponent<Tetomino>().canHold = false;
        }
    }

    // キューにミノを追加
    private void AddNextPieceToQueue()
    {
        int randomIndex = Random.Range(0, tetominoes.Length);
        nextQueue.Enqueue(tetominoes[randomIndex]);
    }

    // NextPieceのプレビュー更新
    private void UpdatePreviews()
    {
        // 古いプレビュー削除
        foreach (GameObject preview in currentPreviews)
        {
            Destroy(preview);
        }
        currentPreviews.Clear();

        // 新しいプレビュー生成
        int index = 0;
        foreach (GameObject tetomino in nextQueue)
        {
            if (index >= previewPositions.Length) break;

            GameObject preview = Instantiate(tetomino, previewPositions[index].position, Quaternion.identity);
            // UI専用レイヤーに変更
            preview.layer = LayerMask.NameToLayer("UI");
            preview.transform.localScale = previewScale;

            // 操作に反映されないようにスクリプトを削除
            Destroy(preview.GetComponent<Tetomino>());

            currentPreviews.Add(preview);
            index++;
        }
    }

    // ゴーストブロックの更新
    public void UpdateGhostBlock()
    {
        // ゴーストブロックを生成または更新
        if (ghostTetomino == null)
        {
            ghostTetomino = Instantiate(ghostPrefab, transform.position, Quaternion.identity);
            ghostTetomino.AddComponent<GhostBlock>();
        }
        ghostTetomino.GetComponent<GhostBlock>().LinkToParent(currentTetomino.transform);
    }

    // 新しいミノの場所にミノが設置されているか
    private bool IsValidGridPos(Transform newTransform)
    {
        foreach (Transform child in newTransform)
        {
            Vector2 v = Grid.Instance.RoundVector2(child.position);

            if (!Grid.Instance.InsideBorder(v)) return false;

            if (Grid.grid[(int)v.x, (int)v.y] != null) return false;
        }

        return true;
    }
}
