using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 階段の段差ポイント用トリガー。
/// Player がこのトリガー領域に入った瞬間に、
/// プレイヤーの「Z座標」を zPos に強制セットして奥行きを調整。
/// （2Dでも Z を使って前後関係を出したいアイソメ等で使用）
/// </summary>
public class StairPoint : MonoBehaviour
{
    // このトリガーに入ったときに設定したい Z 位置
    // 例：0 → 画面中央面 / 正数・負数はカメラ位置とソート設定に依存
    [SerializeField] float zPos;

    /// <summary>
    /// 2Dコライダーのトリガーに入った瞬間に呼ばれる。
    /// ※ 発火条件：
    ///   - このオブジェクトの Collider2D が isTrigger = true
    ///   - 侵入側（Player）に Collider2D と Rigidbody2D のいずれかが付いている
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Player タグのオブジェクトだけを対象にする
        // （推奨：collision.CompareTag("Player") の方が高速で安全）
        if (collision.transform.tag == "Player")
        {
            // X/Y はそのまま、Z だけを zPos に変更して奥行きを調整
            // ※ 2Dプロジェクトで Z を描画順に使う場合は、
            //   Project Settings > Graphics の Transparency Sort Mode / Axis を
            //   Z が効く設定にしておくこと（Orthographic など）。
            collision.transform.position = new Vector3(
                collision.transform.position.x,
                collision.transform.position.y,
                zPos
            );
        }
    }
}
