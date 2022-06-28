using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    [Header("��]���x")]
    [SerializeField] private float m_RotateSpeed;
    [Header("�g�k���x(�b)")]
    [SerializeField] private float m_ScaleCycle;
    [Header("�g�k��")]
    [SerializeField, Range(0f, 1f)] private float m_ScaleRange;


    private Vector3 m_DefaultScale; // �ʏ�̃T�C�Y
    private float m_ScaleTimer; // �g�k�p�^�C�}�[


    void Start()
    {
        // �ʏ�̃T�C�Y��ۑ�
        m_DefaultScale = transform.localScale;
    }
    

    void Update()
    {
        // �����ŉ�]������
        Vector3 objAngle = transform.eulerAngles;
        objAngle.z += m_RotateSpeed * Time.deltaTime;
        transform.eulerAngles = objAngle;

        // �g�k������
        m_ScaleTimer += Time.deltaTime;
        float f = 1f / m_ScaleCycle;
        transform.localScale = m_DefaultScale + (m_DefaultScale * Mathf.Sin(2 * Mathf.PI * f * m_ScaleTimer) * m_ScaleRange);
    }
}
