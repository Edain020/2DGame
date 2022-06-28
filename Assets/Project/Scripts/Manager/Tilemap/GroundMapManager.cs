using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundMapManager : SingletonMonoBehaviour<GroundMapManager>
{
    [Header("�n�ʃ^�C���̌������[�h")]
    [SerializeField] private string m_GroundKeyword;
    [Header("�n�ʂ̃^�C���}�b�v")]
    [SerializeField] private Tilemap m_Ground;

    void Start()
    {
    }

    void Update()
    {
    }


    // �w����W�ɒn�ʂ̃^�C�������݂��邩�ǂ������ׂ�
    public bool CheckGroundTile(Vector3 pos)
    {
        // ���[���h���W���^�C���̍��W�ɕϊ�
        Vector3Int cellPos = m_Ground.WorldToCell(pos);

        // �w����W�Ƀ^�C�������݂��邩�m�F
        if(m_Ground.HasTile(cellPos))
        {
            // �w����W�̃^�C�����n�ʃ^�C��������
            if (m_Ground.GetSprite(cellPos).name.Contains(m_GroundKeyword))
            {
                return true;
            }
        }

        return false;
    }

    // �w����W�ɑ��݂���^�C���̍��W���擾
    public Vector3 GetTilePos(Vector3 pos)
    {
        // �w����W���Z���̒��S���W�ɕϊ�
        return m_Ground.GetCellCenterWorld(m_Ground.WorldToCell(pos));
    }
}
