using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileSaveController : MonoBehaviour
{
    // �Ֆʂ̃^�C���z�u��ǂݎ��Ώۂ� Tilemap
    [SerializeField] Tilemap tilemap;

    // �^�C�����Y(Tile) �� �w�b�_������(head) �֑Ή��t���鎩�� ScriptableObject
    // ��FtileSB.tileDataList[n].tile �� head ���֘A�t���A�ۑ����Ɂu�ǂ̃^�C�����v�𕶎��񉻂���
    [SerializeField] TileScriptableObject tileSB;

    // �ۑ��t�@�C����
    const string SAVE_FILE = "tilemap.json";

    // �o�͐�f�B���N�g���iEditor ���s��z��B���r���h��� StreamingAssets �͊�{�g�ǎ��p�h�j
    const string DATA_DIR = "Assets/StreamingAssets/data/";

    // ���ۂ̕ۑ��p�X�iPath.Combine ��1�v�f�����n���Ă���̂Ŏ������̂܂ܘA������������j
    static string saveDataPath = Path.Combine(DATA_DIR + SAVE_FILE);

    private void Start()
    {
        // �N�����ɑ��ۑ��i�K�v�ɉ����ĔC�Ӄ^�C�~���O�ŌĂяo���j
        //Save();

        Load();
    }

    public void Save()
    {
        // JSON �ɓf���o���f�[�^���ꕨ
        var data = new SaveTilemapData();
        tilemap.CompressBounds();

        // ���ۂɃ^�C�����u����Ă���ŏ��̈�� Bounds �����k�i���ʂȑ����������j
        var b = tilemap.cellBounds;

        // �Z�����W�n�ł͈̔́imin �͊܂� / max �́g������܂Łh���[�v�j
        string str = "";

        // Y�i�s�j�������[�v�Fb.min.y �` b.max.y-1
        for (int y = b.min.y; y < b.max.y; y++)
        {
            // X�i��j�������[�v�Fb.min.x �` b.max.x-1
            for (int x = b.min.x; x < b.max.x; x++)
            {
                // �����Ă���Z�����W�Ƀ^�C�������邩
                if (tilemap.HasTile(new Vector3Int(x, y, 0)))
                {
                    // ScriptableObject ��̑Ή��\����u�����^�C���v����ӂɌ���
                    // ���������v�f�� head �������o���i�����ɃJ���}�j
                    // �� Single �͊Y�� 0��/2���ȏ�ŗ�O �� �z��O�f�[�^�𑁊��ɋC�Â���
                    str += tileSB.tileDataList.Single(t => t.tile == tilemap.GetTile(new Vector3Int(x, y, 0))).head + ",";
                }
                else
                {
                    // �����u����Ă��Ȃ��Z���͋󔒂�\���i���p�X�y�[�X�j�{�J���}
                    // ��F" ," �̂悤�ȃg�[�N���ɂȂ�
                    str += " ,";
                }
            }

            // �s���̗]���ȃJ���}���폜���� 1 �s�m��
            str = str.TrimEnd(',');

            // �s��ǉ��i��F["a,b, ,c", " , ,d, "] �̂悤�ɍs���ƕۊǁj
            data.mapData.Add(str);

            // ���̍s�p�Ƀo�b�t�@���N���A
            str = "";
        }

        // JSON �֕ϊ��iprettyPrint = true�j
        string json = JsonUtility.ToJson(data, true);

        // �o�͐�t�H���_��������΍쐬�i���݂��Ă���Ή������Ȃ��j
        if (!Directory.Exists(DATA_DIR))
        {
            Directory.CreateDirectory(DATA_DIR);
        }

        // �t�@�C���֏������݁i�㏑���j
        // �� using ���g���Ǝ��� Close �ł��邪�A�����ł͖��� Flush/Close
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

    // JSON �����邽�߂̃V���A���C�Y�p�N���X
    [Serializable]
    public class SaveTilemapData
    {
        // �e�v�f�� 1 �s���� CSV ��������i��F"A,B, ,C"�j
        public List<string> mapData = new List<string>();
    }
}