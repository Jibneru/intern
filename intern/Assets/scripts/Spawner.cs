using UnityEngine;

public class Spawner : MonoBehaviour
{
    // �~�m��ۑ�����
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
