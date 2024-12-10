using System.Collections;
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
            // �R���[�`�����g���ăV�[�������[�h
            StartCoroutine(LoadAsyncScene());
        }
    }

    private IEnumerator LoadAsyncScene()
    {
        // �o�b�N�O���E���h�ŃV�[�������[�h
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // �񓯊��V�[�������S�Ƀ��[�h�����܂ő҂�
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
