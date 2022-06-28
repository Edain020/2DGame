using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    // �A�j���[�V�����̏��
    public enum CharacterAnimState
    {
        idle,   // �ҋ@
        walk,   // ���s
    }

    [Header("�A�j���̃X�e�[�^�X")]
    [SerializeField] private CharacterAnimState m_State;
    public CharacterAnimState State
    {
        get
        {
            return m_State;
        }

        set
        {
            // ������Ԃɂ͑J�ڂ��Ȃ�
            if(value == m_State)
            {
                return;
            }

            // ��Ԃɂ���ď������s��
            switch (value)
            {
                // �ҋ@
                case CharacterAnimState.idle:
                    m_Animator.SetBool("walk", false);
                    break;

                // ���s
                case CharacterAnimState.walk:
                    m_Animator.SetBool("walk", true);
                    break;
            }

            m_State = value;
        }
    }

    private Animator m_Animator;  // �A�j���[�V�����Ǘ�


    private void Awake()
    {
        // �R���|�[�l���g���擾
        m_Animator = GetComponent<Animator>();
    }

    void Start()
    {
    }

    void Update()
    {
    }
}
