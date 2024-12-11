using UnityEngine;

// ゴーストブロック制御用スクリプト
public class GhostBlock : MonoBehaviour
{
    public Transform parentTetomino;
    private Vector3[] childOffsets;

    private void Start()
    {
        childOffsets = new Vector3[parentTetomino.childCount];
        for (int i = 0; i < parentTetomino.childCount; i++)
        {
            childOffsets[i] = parentTetomino.GetChild(i).position;
        }
    }

    private void Update()
    {
        UpdateGhostPosition();
    }

    private void UpdateGhostPosition()
    {
        transform.position = parentTetomino.transform.position;
        transform.rotation = parentTetomino.transform.rotation;

        while (IsValidGhostPosition())
        {
            transform.position += new Vector3(0, -1, 0);
        }

        transform.position += new Vector3(0, 1, 0);
    }

    private bool IsValidGhostPosition()
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
