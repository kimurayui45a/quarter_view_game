using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �K�i�̒i���|�C���g�p�g���K�[�B
/// Player �����̃g���K�[�̈�ɓ������u�ԂɁA
/// �v���C���[�́uZ���W�v�� zPos �ɋ����Z�b�g���ĉ��s���𒲐��B
/// �i2D�ł� Z ���g���đO��֌W���o�������A�C�\�����Ŏg�p�j
/// </summary>
public class StairPoint : MonoBehaviour
{
    // ���̃g���K�[�ɓ������Ƃ��ɐݒ肵���� Z �ʒu
    // ��F0 �� ��ʒ����� / �����E�����̓J�����ʒu�ƃ\�[�g�ݒ�Ɉˑ�
    [SerializeField] float zPos;

    /// <summary>
    /// 2D�R���C�_�[�̃g���K�[�ɓ������u�ԂɌĂ΂��B
    /// �� ���Ώ����F
    ///   - ���̃I�u�W�F�N�g�� Collider2D �� isTrigger = true
    ///   - �N�����iPlayer�j�� Collider2D �� Rigidbody2D �̂����ꂩ���t���Ă���
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Player �^�O�̃I�u�W�F�N�g������Ώۂɂ���
        // �i�����Fcollision.CompareTag("Player") �̕��������ň��S�j
        if (collision.transform.tag == "Player")
        {
            // X/Y �͂��̂܂܁AZ ������ zPos �ɕύX���ĉ��s���𒲐�
            // �� 2D�v���W�F�N�g�� Z ��`�揇�Ɏg���ꍇ�́A
            //   Project Settings > Graphics �� Transparency Sort Mode / Axis ��
            //   Z �������ݒ�ɂ��Ă������ƁiOrthographic �Ȃǁj�B
            collision.transform.position = new Vector3(
                collision.transform.position.x,
                collision.transform.position.y,
                zPos
            );
        }
    }
}
