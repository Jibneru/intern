using UnityEngine;

public class Grid : MonoBehaviour
{
    // �V���O���g���C���X�^���X
    public static Grid Instance {  get; private set; }

    // �O���b�h�̕��ƍ���
    public static int width = 10;
    public static int height = 20;

    // �O���b�h���i�[����2�����z��
    public static Transform[,] grid;
    private const float lineOffset = -0.5f;

    private void Awake()
    {
        //�V���O���g�������łɂ���ꍇ���̃I�u�W�F�N�g���폜
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // �C���X�^���X��ݒ肵�ADontDestroyOnLoad�ŃV�[���ԂŔj������Ȃ��悤�ɂ���
            Instance = this;
            grid = new Transform[width, height];
            DontDestroyOnLoad(gameObject);
        }
    }
}
