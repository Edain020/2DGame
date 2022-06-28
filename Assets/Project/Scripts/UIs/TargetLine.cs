using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLine : MonoBehaviour
{
    [Header("���̕�")]
    [SerializeField] private float m_LineWidth;

    private LineRenderer m_Line;

    private void Awake()
    {
        // �R���|�[�l���g���擾
        m_Line = GetComponent<LineRenderer>();

        // ���̕���ݒ�
        m_Line.startWidth = m_Line.endWidth = m_LineWidth;

        // ���_�̐���ݒ�
//        m_Line.positionCount = 2;
    }

    void Start()
    {
    }

    void Update()
    {
    }

    // ���̒��_��ݒ�
    public void SetLinePos(int idx, Vector3 pos)
    {
        m_Line.SetPosition(idx, pos);
    }
}
