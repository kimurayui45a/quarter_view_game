using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{



    public static TilemapController instance;
    [SerializeField] Tilemap defaultTilemap;
    private Vector3Int beforePlayerCellPos;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    public void CheckCloseDoor(Vector3 playerPos)
    {
        Vector3Int playerCellPos = defaultTilemap.WorldToCell(playerPos);
        if (beforePlayerCellPos != playerCellPos)
        {
            OpenDoor(playerCellPos - Vector3Int.up);
            OpenDoor(playerCellPos - Vector3Int.down);
            OpenDoor(playerCellPos - Vector3Int.right);
            OpenDoor(playerCellPos - Vector3Int.left);
            beforePlayerCellPos = playerCellPos;
        }
    }
    private void OpenDoor(Vector3Int cellPos)
    {
        var tile = defaultTilemap.GetTile(cellPos);
        if (tile && tile.name == "door_close_0")
        {
            defaultTilemap.SetTile(cellPos, null);
        }
    }







    // ---ひよこのたまご⑫の内容----------------------
    // タイルの自動配置
    //[SerializeField] Tilemap defaultTilemap;
    //[SerializeField] Tilemap moveTilemap;
    //[SerializeField] Tile blockTile;
    //private Tile selectTile;
    //private Vector3Int originCellPos;
    //private Vector3Int selectCellPos;
    //private void Start()
    //{
    //    for (int y = 0; y < 5; y++)
    //    {
    //        for (int x = 0; x < 5; x++)
    //        {
    //            defaultTilemap.SetTile(new Vector3Int(x, y, 0), blockTile);
    //        }
    //    }
    //}
    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        SelectTile();
    //    }
    //    else if (Input.GetMouseButton(0) && selectTile)
    //    {
    //        MoveTile();
    //    }
    //    else if (Input.GetMouseButtonUp(0) && selectTile)
    //    {
    //        DeployTile();
    //    }
    //}
    //private void SelectTile()
    //{
    //    var pos = Input.mousePosition;
    //    pos.z = 10f;
    //    selectCellPos = defaultTilemap.WorldToCell(Camera.main.ScreenToWorldPoint(pos));
    //    originCellPos = selectCellPos;
    //    var getTile = defaultTilemap.GetTile<Tile>(selectCellPos);
    //    if (getTile)
    //    {
    //        moveTilemap.SetTile(selectCellPos, getTile);
    //        defaultTilemap.SetTile(selectCellPos, null);
    //        selectTile = getTile;
    //    }
    //}
    //private void MoveTile()
    //{
    //    var pos = Input.mousePosition;
    //    pos.z = 10f;
    //    Vector3Int nextPos = defaultTilemap.WorldToCell(Camera.main.ScreenToWorldPoint(pos));
    //    if (selectCellPos != nextPos)
    //    {
    //        moveTilemap.SetTile(nextPos, selectTile);
    //        moveTilemap.SetTile(selectCellPos, null);
    //        selectCellPos = nextPos;
    //    }
    //}
    //private void DeployTile()
    //{
    //    moveTilemap.SetTile(selectCellPos, null);
    //    if (defaultTilemap.HasTile(selectCellPos))
    //    {
    //        defaultTilemap.SetTile(originCellPos, selectTile);
    //    }
    //    else
    //    {
    //        defaultTilemap.SetTile(selectCellPos, selectTile);
    //    }

    //    selectTile = null;
    //}







    // ---ひよこのたまご⑪の内容----------------------

    //// Inspector で割り当てる Tilemap（タイルを敷く先）
    //[SerializeField] Tilemap tilemap;

    //// 敷き詰めたいタイル（Tile または RuleTile など）
    //[SerializeField] Tile blockTile;

    //[SerializeField] Tile fieldTile;

    //// コンポーネントが有効化された最初のフレームで呼ばれる
    //private void Start()
    //{
    //    // タイルを1フレームずつ描いていくコルーチンを開始
    //    StartCoroutine(SetTile());
    //}

    //// タイルを 5x5 の範囲に順に配置するコルーチン
    //IEnumerator SetTile()
    //{
    //    // z 行の指定
    //    // Tilemap の原点(Origin)設定と座標の取り方に依存
    //    for (int z = 0; z < 5; z++)
    //    {
    //        // y 行を上から/下からどちら向きに進めるかは
    //        // Tilemap の原点(Origin)設定と座標の取り方に依存
    //        for (int y = 0; y < 5; y++)
    //        {
    //            // x 列を左→右へループ
    //            for (int x = 0; x < 5; x++)
    //            {
    //                // グリッド座標 (x, y, z=0) に blockTile を配置
    //                // ※ SetTile は即時反映。既存タイルがあれば置き換え
    //                tilemap.SetTile(new Vector3Int(x, y, z * 2), z % 2 == 0 ? blockTile : fieldTile);

    //                // 1枚置くごとに「次のフレームの終わり」まで待つ
    //                // → アニメ的に徐々に敷き詰めたい時に有効
    //                yield return new WaitForEndOfFrame();
    //            }
    //        }
    //    }
    //}

    // -------------------------

}
