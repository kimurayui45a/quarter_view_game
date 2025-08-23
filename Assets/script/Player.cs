using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーを上下左右に少しずつ移動させるコンポーネント。
/// ※ ここでは「1回呼ぶごとに 0.05 ユニット」動く（フレーム依存）。
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// 上へ移動（y を +0.05）
    /// </summary>
    public void MoveUp()
    {
        // 現在位置を基に、y だけを増やした新しい位置を設定
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f);
    }

    /// <summary>
    /// 下へ移動（y を -0.05）
    /// </summary>
    public void MoveDown()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f);
    }

    /// <summary>
    /// 左へ移動（x を -0.05）
    /// </summary>
    public void MoveLeft()
    {
        transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y);
    }

    /// <summary>
    /// 右へ移動（x を +0.05）
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
