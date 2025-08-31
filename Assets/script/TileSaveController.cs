using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileSaveController : MonoBehaviour
{
    // 盤面のタイル配置を読み取る対象の Tilemap
    [SerializeField] Tilemap tilemap;

    // タイル資産(Tile) → ヘッダ文字列(head) へ対応付ける自作 ScriptableObject
    // 例：tileSB.tileDataList[n].tile と head を関連付け、保存時に「どのタイルか」を文字列化する
    [SerializeField] TileScriptableObject tileSB;

    // 保存ファイル名
    const string SAVE_FILE = "tilemap.json";

    // 出力先ディレクトリ（Editor 実行を想定。※ビルド後は StreamingAssets は基本“読取専用”）
    const string DATA_DIR = "Assets/StreamingAssets/data/";

    // 実際の保存パス（Path.Combine に1要素だけ渡しているので実質そのまま連結した文字列）
    static string saveDataPath = Path.Combine(DATA_DIR + SAVE_FILE);

    private void Start()
    {
        // 起動時に即保存（必要に応じて任意タイミングで呼び出す）
        //Save();

        Load();
    }

    public void Save()
    {
        // JSON に吐き出すデータ入れ物
        var data = new SaveTilemapData();
        tilemap.CompressBounds();

        // 実際にタイルが置かれている最小領域に Bounds を圧縮（無駄な走査を避ける）
        var b = tilemap.cellBounds;

        // セル座標系での範囲（min は含む / max は“超えるまで”ループ）
        string str = "";

        // Y（行）方向ループ：b.min.y ～ b.max.y-1
        for (int y = b.min.y; y < b.max.y; y++)
        {
            // X（列）方向ループ：b.min.x ～ b.max.x-1
            for (int x = b.min.x; x < b.max.x; x++)
            {
                // 今見ているセル座標にタイルがあるか
                if (tilemap.HasTile(new Vector3Int(x, y, 0)))
                {
                    // ScriptableObject 上の対応表から「同じタイル」を一意に検索
                    // 見つかった要素の head を書き出す（末尾にカンマ）
                    // ※ Single は該当 0件/2件以上で例外 → 想定外データを早期に気づける
                    str += tileSB.tileDataList.Single(t => t.tile == tilemap.GetTile(new Vector3Int(x, y, 0))).head + ",";
                }
                else
                {
                    // 何も置かれていないセルは空白を表す（半角スペース）＋カンマ
                    // 例：" ," のようなトークンになる
                    str += " ,";
                }
            }

            // 行末の余分なカンマを削除して 1 行確定
            str = str.TrimEnd(',');

            // 行を追加（例：["a,b, ,c", " , ,d, "] のように行ごと保管）
            data.mapData.Add(str);

            // 次の行用にバッファをクリア
            str = "";
        }

        // JSON へ変換（prettyPrint = true）
        string json = JsonUtility.ToJson(data, true);

        // 出力先フォルダが無ければ作成（存在していれば何もしない）
        if (!Directory.Exists(DATA_DIR))
        {
            Directory.CreateDirectory(DATA_DIR);
        }

        // ファイルへ書き込み（上書き）
        // ※ using を使うと自動 Close できるが、ここでは明示 Flush/Close
        StreamWriter writer = new StreamWriter(saveDataPath, false);
        writer.WriteLine(json);
        writer.Flush();
        writer.Close();
    }

    public void Load()
    {
        tilemap.ClearAllTiles();
        FileStream stream = File.Open(saveDataPath, FileMode.Open);
        StreamReader reader = new StreamReader(stream);
        var json = reader.ReadToEnd();
        reader.Close();
        stream.Close();
        SaveTilemapData data = JsonUtility.FromJson<SaveTilemapData>(json);
        for (int y = 0; y < data.mapData.Count; y++)
        {
            string[] xlist = data.mapData[y].Split(',');
            for (int x = 0; x < xlist.Length; x++)
            {
                if (xlist[x] == " ") continue;
                tilemap.SetTile(new Vector3Int(x, y, 0), tileSB.tileDataList.Single(t => t.head == xlist[x]).tile);
            }
        }
    }

    // JSON 化するためのシリアライズ用クラス
    [Serializable]
    public class SaveTilemapData
    {
        // 各要素が 1 行分の CSV 風文字列（例："A,B, ,C"）
        public List<string> mapData = new List<string>();
    }
}