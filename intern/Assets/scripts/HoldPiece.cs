using System;
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
        // ホールドが空の時
        if (holdTetomino == null)
        {
            holdTetomino = currentTetomino;
            holdTetomino.transform.position = hodlPosition.position;
            Destroy(currentTetomino.GetComponent<Tetomino>());
        }
        // ホールド済みのミノがある場合
        else
        {
            GameObject temp = holdTetomino;
            holdTetomino = currentTetomino;
            holdTetomino.transform.position = hodlPosition.position;
            Destroy(currentTetomino.GetComponent<Tetomino>());
            Instantiate(temp, currentTetomino.transform.position, Quaternion.identity);
        }
    }
}
