using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLine : MonoBehaviour
{
    [Header("線の幅")]
    [SerializeField] private float m_LineWidth;

    private LineRenderer m_Line;

    private void Awake()
    {
        // コンポーネントを取得
        m_Line = GetComponent<LineRenderer>();

        // 線の幅を設定
        m_Line.startWidth = m_Line.endWidth = m_LineWidth;

        // 頂点の数を設定
//        m_Line.positionCount = 2;
    }

    void Start()
    {
    }

    void Update()
    {
    }

    // 線の頂点を設定
    public void SetLinePos(int idx, Vector3 pos)
    {
        m_Line.SetPosition(idx, pos);
    }
}
