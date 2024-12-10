using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Assertions;

// �e�g���X�̃}�X�ڂ̃X�N���v�g
public class Grid : MonoBehaviour
{
    // �V���O���g���C���X�^���X
    public static Grid Instance {  get; private set; }

    // �Ǘp�̃Q�[���I�u�W�F�N�g
    [SerializeField] GameObject cubeBlock;

    // �O���b�h�̕��ƍ���
    // Tetomino.cs�Ŏg����������
    public const int width = 10;
    public const int height = 20;

    // �O���b�h���i�[����2�����z��
    public static Transform[,] grid;
    private const float lineOffset = -0.5f;

    private void Awake()
    {
        // Assert��Instance������ƃG���[���o��
        Assert.IsTrue(Instance == null);

        // �C���X�^���X��ݒ肵�ADontDestroyOnLoad�ŃV�[���ԂŔj������Ȃ��悤�ɂ���
        Instance = this;
        grid = new Transform[width, height];
        DontDestroyOnLoad(gameObject);
        
    }

    private void Start()
    {
        // �����ɕǐ���
        for (int i = 0; i < width; i++)
        {
            Instantiate(cubeBlock, new Vector3(i, -1.0f, 0), Quaternion.identity);
        }

        // �c���ɐ����i���E�j
        for (int i = 0; i < height; i++)
        {
            Instantiate(cubeBlock, new Vector3(-1.0f, i - 1.0f, 0), Quaternion.identity);
        }
        for (int i = 0; i < height; i++)
        {
            Instantiate(cubeBlock, new Vector3(width, i - 1.0f, 0), Quaternion.identity);
        }
    }

    // �x�N�g���𐮐��ɂ���
    public Vector2 RoundVector2(Vector2 v)
    {
        // ���l�͎l�̌ܓ����Ă���
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    // �w�肳�ꂽ�ʒu���O���b�h���ɂ��邩���`�F�b�N
    public bool InsideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0);
    }

    // �s�����܂��Ă��邩�`�F�b�N
    private bool IsRowFull(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null) return false;
        }

        return true;
    }

    // �w�肵���s���폜
    private void DeleteRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    // �w�肳�ꂽ�s�����ɍ~�낷
    private void DescendRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] != null)
            {
                // �u���b�N����i������
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    // �w�肳�ꂽ�s���������ɍ~�낷
    private void DescendRowsAbove(int y)
    {
        for (int i = y; i < height; i++)
        {
            DescendRow(i);
        }
    }

    // ���S�ɖ��܂����s���폜���A��̍s����i������
    public void DeleteFullRows()
    {
        for (int y = 0; y < height; y++)
        {
            if (IsRowFull(y))
            {
                DeleteRow(y);
                DescendRowsAbove(y);
            }
        }
    }

    // �O���b�h���̍X�V
    public void UpdateGrid(Transform t)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y].parent == t)
                    {
                        grid[x, y] = null;
                    }
                }
            }
        }

        foreach (Transform child in t)
        {
            Vector2 v = RoundVector2(child.position);
            grid[(int)v.x, (int)v.y] = child;
        }
    }

    // ���E����`�悷�郁�\�b�h
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(new Vector3(lineOffset, lineOffset, 0), new Vector3(lineOffset, height + lineOffset, 0));
        Gizmos.DrawLine(new Vector3(width + lineOffset, lineOffset, 0), new Vector3(width + lineOffset, height + lineOffset, 0));
        Gizmos.DrawLine(new Vector3(lineOffset, lineOffset, 0), new Vector3(width + lineOffset, lineOffset, 0));
    }
}
