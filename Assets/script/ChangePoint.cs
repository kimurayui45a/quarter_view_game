using UnityEngine;

public class ChangePoint : MonoBehaviour
{
    [Header("上階の階数"), SerializeField]
    public int upperFloor;

    [Header("下階の階数"), SerializeField]
    public int lowerFloor;

    private Collider2D myCollider = null;
    private Transform player = null;

    // 初期設定
    private void Awake()
    {
        // 自身のコライダーを取得
        myCollider = GetComponent<Collider2D>();

        // プレイヤーのトランスフォームを取得
        player = GameObject.FindWithTag("Player").transform;
    }

    // フレームごとの動作
    private void Update()
    {
        if (player != null)
        {
            if (player.position.z >= lowerFloor * 2)
            {
                // プレイヤーが「下階の階数」以上の位置にいる場合は
                // 自身のコライダーを有効にする
                myCollider.enabled = true;
            }
            else
            {
                // プレイヤーが「下階の階数」未満の位置にいる場合は
                // 自身のコライダーを無効にする
                myCollider.enabled = false;
            }
        }
    }
}