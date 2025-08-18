using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] Transform _camera;

    void Update()
    {
        // 矢印キーで移動（押している間ずっと移動）
        if (Input.GetKey(KeyCode.LeftArrow)) _player.MoveLeft();
        if (Input.GetKey(KeyCode.UpArrow)) _player.MoveUp();
        if (Input.GetKey(KeyCode.RightArrow)) _player.MoveRight();
        if (Input.GetKey(KeyCode.DownArrow)) _player.MoveDown();
    }

    void LateUpdate()
    {
        // カメラ追従：毎フレームプレイヤー座標へ
        // （元コードは x と y の比較が取り違えられていました）
        var p = _player.transform.position;
        _camera.position = new Vector3(p.x, p.y, -10f);
    }
}
