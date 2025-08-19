using UnityEngine;

public class ChangePoint : MonoBehaviour
{
    [Header("��K�̊K��"), SerializeField]
    public int upperFloor;

    [Header("���K�̊K��"), SerializeField]
    public int lowerFloor;

    private Collider2D myCollider = null;
    private Transform player = null;

    // �����ݒ�
    private void Awake()
    {
        // ���g�̃R���C�_�[���擾
        myCollider = GetComponent<Collider2D>();

        // �v���C���[�̃g�����X�t�H�[�����擾
        player = GameObject.FindWithTag("Player").transform;
    }

    // �t���[�����Ƃ̓���
    private void Update()
    {
        if (player != null)
        {
            if (player.position.z >= lowerFloor * 2)
            {
                // �v���C���[���u���K�̊K���v�ȏ�̈ʒu�ɂ���ꍇ��
                // ���g�̃R���C�_�[��L���ɂ���
                myCollider.enabled = true;
            }
            else
            {
                // �v���C���[���u���K�̊K���v�����̈ʒu�ɂ���ꍇ��
                // ���g�̃R���C�_�[�𖳌��ɂ���
                myCollider.enabled = false;
            }
        }
    }
}