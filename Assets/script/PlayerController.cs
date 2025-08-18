using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] Transform _camera;

    void Update()
    {
        // ���L�[�ňړ��i�����Ă���Ԃ����ƈړ��j
        if (Input.GetKey(KeyCode.LeftArrow)) _player.MoveLeft();
        if (Input.GetKey(KeyCode.UpArrow)) _player.MoveUp();
        if (Input.GetKey(KeyCode.RightArrow)) _player.MoveRight();
        if (Input.GetKey(KeyCode.DownArrow)) _player.MoveDown();
    }

    void LateUpdate()
    {
        // �J�����Ǐ]�F���t���[���v���C���[���W��
        // �i���R�[�h�� x �� y �̔�r�����Ⴆ���Ă��܂����j
        var p = _player.transform.position;
        _camera.position = new Vector3(p.x, p.y, -10f);
    }
}
