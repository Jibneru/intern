using UnityEngine;
using UnityEngine.Assertions;

// �e�g���X�̃}�X�ڂ̃X�N���v�g
public class Grid : MonoBehaviour
{
    // �V���O���g���C���X�^���X
    public static Grid Instance {  get; private set; }

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
}
