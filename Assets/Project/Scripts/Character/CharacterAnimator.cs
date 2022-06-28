using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    // アニメーションの状態
    public enum CharacterAnimState
    {
        idle,   // 待機
        walk,   // 歩行
    }

    [Header("アニメのステータス")]
    [SerializeField] private CharacterAnimState m_State;
    public CharacterAnimState State
    {
        get
        {
            return m_State;
        }

        set
        {
            // 同じ状態には遷移しない
            if(value == m_State)
            {
                return;
            }

            // 状態によって処理を行う
            switch (value)
            {
                // 待機
                case CharacterAnimState.idle:
                    m_Animator.SetBool("walk", false);
                    break;

                // 歩行
                case CharacterAnimState.walk:
                    m_Animator.SetBool("walk", true);
                    break;
            }

            m_State = value;
        }
    }

    private Animator m_Animator;  // アニメーション管理


    private void Awake()
    {
        // コンポーネントを取得
        m_Animator = GetComponent<Animator>();
    }

    void Start()
    {
    }

    void Update()
    {
    }
}
