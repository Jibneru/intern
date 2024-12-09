using UnityEngine;

public class Spawner : MonoBehaviour
{
    // ƒ~ƒm‚ð•Û‘¶‚·‚é
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
