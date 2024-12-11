using System.Collections.Generic;
using UnityEngine;

public class NextPiece : MonoBehaviour
{
    // ミノのプレファブリスト
    public GameObject[] tetominoes;

    // 後から追加されて前から取り出したいためQueueを使用
    Queue<GameObject> nextQueue = new Queue<GameObject>();

    // 次のミノを表示する位置の配列
    public Transform[] previewPositions;

    // 表示中のプレビューを管理
    private List<GameObject> currentPreviews = new List<GameObject>();

    // プレビューのスケール
    private Vector3 previewScale = new Vector3(0.5f, 0.5f, 0.5f);

    private void Start()
    {
        // 初めは表示する個数分だけ生成
        for (int i = 0; i < previewPositions.Length; i++)
        {
            AddNextPieceToQueue();
        }
    }

    // キューの先頭からミノを取得
    public GameObject GetNextPiece()
    {
        GameObject next = nextQueue.Dequeue();
        AddNextPieceToQueue();
        UpdatePreviews();
        return next;
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
}
