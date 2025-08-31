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







    // ---�Ђ悱�̂��܂��K�̓��e----------------------
    // �^�C���̎����z�u
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







    // ---�Ђ悱�̂��܂��J�̓��e----------------------

    //// Inspector �Ŋ��蓖�Ă� Tilemap�i�^�C����~����j
    //[SerializeField] Tilemap tilemap;

    //// �~���l�߂����^�C���iTile �܂��� RuleTile �Ȃǁj
    //[SerializeField] Tile blockTile;

    //[SerializeField] Tile fieldTile;

    //// �R���|�[�l���g���L�������ꂽ�ŏ��̃t���[���ŌĂ΂��
    //private void Start()
    //{
    //    // �^�C����1�t���[�����`���Ă����R���[�`�����J�n
    //    StartCoroutine(SetTile());
    //}

    //// �^�C���� 5x5 �͈̔͂ɏ��ɔz�u����R���[�`��
    //IEnumerator SetTile()
    //{
    //    // z �s�̎w��
    //    // Tilemap �̌��_(Origin)�ݒ�ƍ��W�̎����Ɉˑ�
    //    for (int z = 0; z < 5; z++)
    //    {
    //        // y �s���ォ��/������ǂ�������ɐi�߂邩��
    //        // Tilemap �̌��_(Origin)�ݒ�ƍ��W�̎����Ɉˑ�
    //        for (int y = 0; y < 5; y++)
    //        {
    //            // x ��������E�փ��[�v
    //            for (int x = 0; x < 5; x++)
    //            {
    //                // �O���b�h���W (x, y, z=0) �� blockTile ��z�u
    //                // �� SetTile �͑������f�B�����^�C��������Βu������
    //                tilemap.SetTile(new Vector3Int(x, y, z * 2), z % 2 == 0 ? blockTile : fieldTile);

    //                // 1���u�����ƂɁu���̃t���[���̏I���v�܂ő҂�
    //                // �� �A�j���I�ɏ��X�ɕ~���l�߂������ɗL��
    //                yield return new WaitForEndOfFrame();
    //            }
    //        }
    //    }
    //}

    // -------------------------

}
