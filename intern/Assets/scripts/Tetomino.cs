using UnityEngine;

// �~�m�̓����𑀍삷��X�N���v�g
public class Tetomino : MonoBehaviour
{
    // �����Ɋւ���ϐ�
    float fall = 0;
    public float fallSpeed = 1;

    private void Update()
    {
        CheckUserInput();
    }

    // ���[�U�[���̓`�F�b�N
    void CheckUserInput()
    {
        // A�L�[�������ꂽ�Ƃ�
        if (Input.GetKeyDown(KeyCode.A))
        {
            // �u���b�N�����Ɉړ�
            transform.position += new Vector3(-1, 0, 0);
        }
        // D�L�[�������ꂽ�Ƃ�
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // �u���b�N���E�Ɉړ�
            transform.position += new Vector3(1, 0, 0);
        }
        // E�L�[�������ꂽ�Ƃ�
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // �u���b�N����]
            transform.Rotate(0, 0, -90);
        }
        // S�L�[�������ꂽ�Ƃ�
        else if(Input.GetKeyDown(KeyCode.S))
        {
            // �u���b�N������
            transform.position += new Vector3(0, -1, 0);
        }
    }
}
