using UnityEngine;

// �~�m�̓����𑀍삷��X�N���v�g
public class Tetomino : MonoBehaviour
{
    // �����Ɋւ���ϐ�
    float fall = 0;
    [SerializeField] float fallSpeed = 1;

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

            // �ʒu���L�����`�F�b�N
            if (!IsValidGridPos())
            {
                // �ʒu�������Ȃ�߂�
                transform.position += new Vector3(1, 0, 0);
            }
            else
            {
                // �ʒu���L���Ȃ�O���b�h���X�V
                Grid.Instance.UpdateGrid(transform);
            }
                
        }
        // D�L�[�������ꂽ�Ƃ�
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // �u���b�N���E�Ɉړ�
            transform.position += new Vector3(1, 0, 0);

            // �ʒu���L�����`�F�b�N
            if (!IsValidGridPos())
            {
                // �ʒu�������Ȃ�߂�
                transform.position += new Vector3(-1, 0, 0);
            }
            else
            {
                // �ʒu���L���Ȃ�O���b�h���X�V
                Grid.Instance.UpdateGrid(transform);
            }
        }
        // E�L�[�������ꂽ�Ƃ�
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // �u���b�N���E��]
            transform.Rotate(0, 0, -90);
        }
        // Q�L�[�������ꂽ�Ƃ�
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // �u���b�N������]
            transform.Rotate(0, 0, 90);
        }
        // S�L�[�������ꂽ�Ƃ�
        else if (Input.GetKeyDown(KeyCode.S))
        {
            // �u���b�N������
            transform.position += new Vector3(0, -1, 0);

            // �ʒu���L�����`�F�b�N
            if (!IsValidGridPos())
            {
                // �ʒu�������Ȃ�߂�
                transform.position += new Vector3(0, 1, 0);

                // �O���b�h�X�V
                Grid.Instance.UpdateGrid(transform);

                // ���S�ɖ��܂����s���폜
                Grid.Instance.DeleteFullRows();

                // �V�����~�m�𐶐�
                FindAnyObjectByType<Spawner>().SpawnNext();
                enabled = false;
            }
            else
            {
                // �ʒu���L���Ȃ�O���b�h���X�V
                Grid.Instance.UpdateGrid(transform);
            }

            // �������Ԃ����Z�b�g
            fall = Time.time;
        }
    }

    // �O���b�h���ňʒu���L�����ǂ����̔���
    bool IsValidGridPos()
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
