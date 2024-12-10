using UnityEngine;

// ミノをランダムに生成するスクリプト
public class Spawner : MonoBehaviour
{
    // ミノを保存する
    public GameObject[] tetominoes;

    private void Start()
    {
        SpawnNext();
    }

    public void SpawnNext()
    {
        int i = Random.Range(0, tetominoes.Length);
        Instantiate(tetominoes[i], transform.position, Quaternion.identity);
    }
}
