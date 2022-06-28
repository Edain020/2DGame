using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundMapManager : SingletonMonoBehaviour<GroundMapManager>
{
    [Header("地面タイルの検索ワード")]
    [SerializeField] private string m_GroundKeyword;
    [Header("地面のタイルマップ")]
    [SerializeField] private Tilemap m_Ground;

    void Start()
    {
    }

    void Update()
    {
    }


    // 指定座標に地面のタイルが存在するかどうか調べる
    public bool CheckGroundTile(Vector3 pos)
    {
        // ワールド座標をタイルの座標に変換
        Vector3Int cellPos = m_Ground.WorldToCell(pos);

        // 指定座標にタイルが存在するか確認
        if(m_Ground.HasTile(cellPos))
        {
            // 指定座標のタイルが地面タイルか判定
            if (m_Ground.GetSprite(cellPos).name.Contains(m_GroundKeyword))
            {
                return true;
            }
        }

        return false;
    }

    // 指定座標に存在するタイルの座標を取得
    public Vector3 GetTilePos(Vector3 pos)
    {
        // 指定座標をセルの中心座標に変換
        return m_Ground.GetCellCenterWorld(m_Ground.WorldToCell(pos));
    }
}
