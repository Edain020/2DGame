using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    [Header("âÒì]ë¨ìx")]
    [SerializeField] private float m_RotateSpeed;
    [Header("ägèkë¨ìx(ïb)")]
    [SerializeField] private float m_ScaleCycle;
    [Header("ägèkïù")]
    [SerializeField, Range(0f, 1f)] private float m_ScaleRange;


    private Vector3 m_DefaultScale; // í èÌÇÃÉTÉCÉY
    private float m_ScaleTimer; // ägèkópÉ^ÉCÉ}Å[


    void Start()
    {
        // í èÌÇÃÉTÉCÉYÇï€ë∂
        m_DefaultScale = transform.localScale;
    }
    

    void Update()
    {
        // ìôë¨Ç≈âÒì]Ç≥ÇπÇÈ
        Vector3 objAngle = transform.eulerAngles;
        objAngle.z += m_RotateSpeed * Time.deltaTime;
        transform.eulerAngles = objAngle;

        // ägèkÇ≥ÇπÇÈ
        m_ScaleTimer += Time.deltaTime;
        float f = 1f / m_ScaleCycle;
        transform.localScale = m_DefaultScale + (m_DefaultScale * Mathf.Sin(2 * Mathf.PI * f * m_ScaleTimer) * m_ScaleRange);
    }
}
