using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class SlimeController : MonoBehaviour
{
    [Header("移動の速さ"), SerializeField]
    private float mySpeed = 800;

    [Header("各階層のトランスフォーム"), SerializeField]
    private List<Transform> floors;

    private Rigidbody2D myRigidbody;
    private Vector2 inputMove;

    // 初期設定
    private void Awake()
    {
        // 自身のリジッドボディを取得
        myRigidbody = GetComponent<Rigidbody2D>();

        // 各階層のトランスフォームを取得
        Transform parent = GameObject.Find("Floors").transform;
        foreach (Transform child in parent)
        {
            floors.Add(child);
        }

        // プレイヤーと同じ階層のコライダータイルマップを有効化
        int z = (int)transform.position.z;
        floors[z / 2].GetChild(0).gameObject.SetActive(true);
    }

    // 一定時間ごとの処理
    private void FixedUpdate()
    {
        // プレイヤーを移動
        float moveX = inputMove.x * mySpeed * Time.deltaTime;
        float moveY = inputMove.y * mySpeed * Time.deltaTime;
        myRigidbody.linearVelocity = new Vector2(moveX, moveY);
    }

    // 移動アクション発生時の処理
    public void OnMove(InputValue value)
    {
        // プレイヤー移動方向を設定
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

    // トリガー衝突時の処理
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // 階層切り替えポイントと衝突した場合
        if (collision.tag.Contains("ChangePoint"))
        {
            Vector3 pPos = transform.position;
            int upper = collision.GetComponent<ChangePoint>().upperFloor;
            int lower = collision.GetComponent<ChangePoint>().lowerFloor;

            // 下階のコライダータイルマップを無効化した後に
            // 上階のコライダータイルマップを有効化
            floors[lower].GetChild(0).gameObject.SetActive(false);
            floors[upper].GetChild(0).gameObject.SetActive(true);

            // 下階のタイルマップソーティングオーダーに-1を設定
            // プレイヤーにとって下階のタイルマップが地面に相当するため
            // Pivotによる表示順序切り替えが不要となる
            floors[lower].GetComponent<TilemapRenderer>().sortingOrder = -1;

            // プレイヤーの高さ(Z)を上階に合わせる
            pPos.z = upper * 2;
            transform.position = pPos;
        }
    }

    // トリガー離脱時の処理
    public void OnTriggerExit2D(Collider2D collision)
    {
        // 階層切り替えポイントから離脱した場合
        if (collision.tag.Contains("ChangePoint"))
        {
            Vector3 pPos = transform.position;
            Vector3 cPos = collision.transform.position;
            int upper = collision.GetComponent<ChangePoint>().upperFloor;
            int lower = collision.GetComponent<ChangePoint>().lowerFloor;

            // 階層切り替えポイントの下方から離脱した場合
            if (pPos.y < cPos.y)
            {
                // 上階のコライダータイルマップを無効化した後に
                // 下階のコライダータイルマップを有効化
                floors[upper].GetChild(0).gameObject.SetActive(false);
                floors[lower].GetChild(0).gameObject.SetActive(true);

                // 下階のタイルマップソーティングオーダーに0を設定
                // プレイヤーが下階のタイルマップと同じ高さへ移動するため
                // Pivotによる表示順序切り替えが必要となる
                floors[lower].GetComponent<TilemapRenderer>().sortingOrder = 0;

                // プレイヤーの高さ(Z)を下階に合わせる
                pPos.z = lower * 2;
                transform.position = pPos;
            }
            else
            {
                // トリガー衝突時に上階に移動する処理を実施しているため
                // 階層切り替えポイントの上方から離脱した場合は何もしない
            }
        }
    }
}