using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class SlimeController : MonoBehaviour
{
    [Header("�ړ��̑���"), SerializeField]
    private float mySpeed = 800;

    [Header("�e�K�w�̃g�����X�t�H�[��"), SerializeField]
    private List<Transform> floors;

    private Rigidbody2D myRigidbody;
    private Vector2 inputMove;

    // �����ݒ�
    private void Awake()
    {
        // ���g�̃��W�b�h�{�f�B���擾
        myRigidbody = GetComponent<Rigidbody2D>();

        // �e�K�w�̃g�����X�t�H�[�����擾
        Transform parent = GameObject.Find("Floors").transform;
        foreach (Transform child in parent)
        {
            floors.Add(child);
        }

        // �v���C���[�Ɠ����K�w�̃R���C�_�[�^�C���}�b�v��L����
        int z = (int)transform.position.z;
        floors[z / 2].GetChild(0).gameObject.SetActive(true);
    }

    // ��莞�Ԃ��Ƃ̏���
    private void FixedUpdate()
    {
        // �v���C���[���ړ�
        float moveX = inputMove.x * mySpeed * Time.deltaTime;
        float moveY = inputMove.y * mySpeed * Time.deltaTime;
        myRigidbody.linearVelocity = new Vector2(moveX, moveY);
    }

    // �ړ��A�N�V�����������̏���
    public void OnMove(InputValue value)
    {
        // �v���C���[�ړ�������ݒ�
        inputMove = value.Get<Vector2>();
        if (inputMove.x > 0 && inputMove.y > 0)
        {
            inputMove = new Vector2(1.16f, 0.58f);
        }
        else if (inputMove.x < 0 && inputMove.y > 0)
        {
            inputMove = new Vector2(-1.16f, 0.58f);
        }
        else if (inputMove.x < 0 && inputMove.y < 0)
        {
            inputMove = new Vector2(-1.16f, -0.58f);
        }
        else if (inputMove.x > 0 && inputMove.y < 0)
        {
            inputMove = new Vector2(1.16f, -0.58f);
        }
    }

    // �g���K�[�Փˎ��̏���
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // �K�w�؂�ւ��|�C���g�ƏՓ˂����ꍇ
        if (collision.tag.Contains("ChangePoint"))
        {
            Vector3 pPos = transform.position;
            int upper = collision.GetComponent<ChangePoint>().upperFloor;
            int lower = collision.GetComponent<ChangePoint>().lowerFloor;

            // ���K�̃R���C�_�[�^�C���}�b�v�𖳌����������
            // ��K�̃R���C�_�[�^�C���}�b�v��L����
            floors[lower].GetChild(0).gameObject.SetActive(false);
            floors[upper].GetChild(0).gameObject.SetActive(true);

            // ���K�̃^�C���}�b�v�\�[�e�B���O�I�[�_�[��-1��ݒ�
            // �v���C���[�ɂƂ��ĉ��K�̃^�C���}�b�v���n�ʂɑ������邽��
            // Pivot�ɂ��\�������؂�ւ����s�v�ƂȂ�
            floors[lower].GetComponent<TilemapRenderer>().sortingOrder = -1;

            // �v���C���[�̍���(Z)����K�ɍ��킹��
            pPos.z = upper * 2;
            transform.position = pPos;
        }
    }

    // �g���K�[���E���̏���
    public void OnTriggerExit2D(Collider2D collision)
    {
        // �K�w�؂�ւ��|�C���g���痣�E�����ꍇ
        if (collision.tag.Contains("ChangePoint"))
        {
            Vector3 pPos = transform.position;
            Vector3 cPos = collision.transform.position;
            int upper = collision.GetComponent<ChangePoint>().upperFloor;
            int lower = collision.GetComponent<ChangePoint>().lowerFloor;

            // �K�w�؂�ւ��|�C���g�̉������痣�E�����ꍇ
            if (pPos.y < cPos.y)
            {
                // ��K�̃R���C�_�[�^�C���}�b�v�𖳌����������
                // ���K�̃R���C�_�[�^�C���}�b�v��L����
                floors[upper].GetChild(0).gameObject.SetActive(false);
                floors[lower].GetChild(0).gameObject.SetActive(true);

                // ���K�̃^�C���}�b�v�\�[�e�B���O�I�[�_�[��0��ݒ�
                // �v���C���[�����K�̃^�C���}�b�v�Ɠ��������ֈړ����邽��
                // Pivot�ɂ��\�������؂�ւ����K�v�ƂȂ�
                floors[lower].GetComponent<TilemapRenderer>().sortingOrder = 0;

                // �v���C���[�̍���(Z)�����K�ɍ��킹��
                pPos.z = lower * 2;
                transform.position = pPos;
            }
            else
            {
                // �g���K�[�Փˎ��ɏ�K�Ɉړ����鏈�������{���Ă��邽��
                // �K�w�؂�ւ��|�C���g�̏�����痣�E�����ꍇ�͉������Ȃ�
            }
        }
    }
}