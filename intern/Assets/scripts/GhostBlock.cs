using UnityEngine;

// ゴーストブロック制御用スクリプト
public class GhostBlock : MonoBehaviour
{
    // 操作中のミノを追跡
    [System.NonSerialized] Transform parentTetomino;

    private void Start()
    {
        UpdateChildOffest();
        UpdateGhostPosition();
    }

    // ゴーストブロックの相対位置を親のミノに基づき更新
    private void UpdateChildOffest()
    {
        // 子ブロックの位置をゴーストに反映
        for (int i = 0; i < parentTetomino.childCount; i++)
        {
            transform.GetChild(i).localPosition = parentTetomino.GetChild(i).localPosition;
        }
    }
    
    // ゴーストブロックを一番下まで移動させる
    public void UpdateGhostPosition()
    {
        // ミノの現在位置を基準
        transform.position = parentTetomino.transform.position;
        transform.rotation = parentTetomino.transform.rotation;

        while (IsValidGhostPosition())
        {
            transform.position += new Vector3(0, -1, 0);
        }

        transform.position += new Vector3(0, 1, 0);
    }

    // 移動した先が範囲外や設置されたミノがないか判定
    private bool IsValidGhostPosition()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.Instance.RoundVector2(child.position);

            if (!Grid.Instance.InsideBorder(v) ||
                Grid.grid[(int)v.x, (int)v.y] != null &&
                Grid.grid[(int)v.x, (int)v.y].parent != parentTetomino) return false;
        }

        return true;
    }

    // 親のミノが切り替わった時に呼び出す
    public void LinkToParent(Transform newParent)
    {
        parentTetomino = newParent;
        UpdateChildOffest();
        UpdateGhostPosition();
    }
}
