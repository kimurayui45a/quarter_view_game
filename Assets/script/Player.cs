using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[���㉺���E�ɏ������ړ�������R���|�[�l���g�B
/// �� �����ł́u1��ĂԂ��Ƃ� 0.05 ���j�b�g�v�����i�t���[���ˑ��j�B
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// ��ֈړ��iy �� +0.05�j
    /// </summary>
    public void MoveUp()
    {
        // ���݈ʒu����ɁAy �����𑝂₵���V�����ʒu��ݒ�
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f);
    }

    /// <summary>
    /// ���ֈړ��iy �� -0.05�j
    /// </summary>
    public void MoveDown()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f);
    }

    /// <summary>
    /// ���ֈړ��ix �� -0.05�j
    /// </summary>
    public void MoveLeft()
    {
        transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y);
    }

    /// <summary>
    /// �E�ֈړ��ix �� +0.05�j
    /// </summary>
    public void MoveRight()
    {
        transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y);
    }

    public void SetZPos(float zPos)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
    }
}
