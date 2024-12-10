using UnityEngine;
using UnityEngine.SceneManagement;

// �V�[���؂�ւ����s���X�N���v�g
public class SceneLoad : MonoBehaviour
{
    // �V�[���̖��O����͂���
    [SerializeField] string sceneName;

    private void Update()
    {
        // ���ŃX�y�[�X���������Ƃ��ɃV�[����؂�ւ���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
