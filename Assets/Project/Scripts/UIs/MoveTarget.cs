using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    [Header("回転速度")]
    [SerializeField] private float m_RotateSpeed;
    [Header("拡縮速度(秒)")]
    [SerializeField] private float m_ScaleCycle;
    [Header("拡縮幅")]
    [SerializeField, Range(0f, 1f)] private float m_ScaleRange;


    private Vector3 m_DefaultScale; // 通常のサイズ
    private float m_ScaleTimer; // 拡縮用タイマー


    void Start()
    {
        // 通常のサイズを保存
        m_DefaultScale = transform.localScale;
    }
    

    void Update()
    {
        // 等速で回転させる
        Vector3 objAngle = transform.eulerAngles;
        objAngle.z += m_RotateSpeed * Time.deltaTime;
        transform.eulerAngles = objAngle;

        // 拡縮させる
        m_ScaleTimer += Time.deltaTime;
        float f = 1f / m_ScaleCycle;
        transform.localScale = m_DefaultScale + (m_DefaultScale * Mathf.Sin(2 * Mathf.PI * f * m_ScaleTimer) * m_ScaleRange);
    }
}
