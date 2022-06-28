using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [Header("�ړ����x")]
    [SerializeField] private float m_MoveSpeed;
    [Header("�ړ����t���O")]
    [SerializeField] private bool m_Move;
    [Header("�ڕW���W")]
    [SerializeField] private Vector3 m_TargetPos;

    [Header("�ڕW���W�̖ڈ�")]
    [SerializeField] private MoveTarget m_MoveTargetPrefab;
    [Header("�ڕW���W�ւ̃��C��")]
    [SerializeField] private TargetLine m_TargetLinePrefab;


    private MoveTarget m_MoveTarget;    // �ڕW���W�̖ڈ�
    private TargetLine m_TargetLine;    // �ڕW���W�ւ̃��C��
    private CharacterAnimator m_Anim;   // �A�j���[�V����

    void Start()
    {
        // �ϐ��̏�����
        m_Move = false;
        m_MoveTarget = null;

        // �R���|�[�l���g���擾
        m_Anim = GetComponentInChildren<CharacterAnimator>();
    }

    void Update()
    {
        // �E�N���b�N�����ʒu�ɖڕW���W���Z�b�g����
        if(Input.GetMouseButtonDown(1))
        {
            // �}�E�X���W���擾����
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0f;
            // �}�E�X���W���X�N���[�����W���烏�[���h���W�ɕϊ�
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);

            // �N���b�N�������W�ɒn�ʃ^�C�������݂��邩�m�F����
            GroundMapManager ground = GroundMapManager.Instance;
            if (ground.CheckGroundTile(targetPos))
            {
                // �N���b�N�������W���^�C���̍��W�ɕϊ�
                targetPos = ground.GetTilePos(targetPos);

                // �����ꏊ�Ɉړ����悤�Ƃ��Ă����珈�����Ȃ�(���Ă����Ȃ��Ă�����)
                if (targetPos != m_TargetPos)
                {
                    // �ڕW���W��ݒ�
                    m_TargetPos = targetPos;
                    m_Move = true;

                    // �^�[�Q�b�g�𐶐�
                    CreateTarget();
                }
            }
        }

        // �ړ����̏���
        if (m_Move)
        {
            // �ړ���̍��W���v�Z
            Vector3 movePos = Vector3.MoveTowards(transform.position, m_TargetPos, m_MoveSpeed * Time.deltaTime);
            // ���ݍ��W�ƈړ���̍��W����ړ��ʂ����߂�
            Vector3 moveVec = movePos - transform.position;
            // �ړ��ʂ��獶�E�̌������l���A�K�v�ɉ����Ĕ��]
            if (moveVec.x != 0f && moveVec.x / Mathf.Abs(moveVec.x) != transform.localScale.x / Mathf.Abs(transform.localScale.x))
            {
                // ���E����
                Vector3 scale = transform.localScale;
                scale.x *= -1f;
                transform.localScale = scale;
            }
            // �ړ�
            transform.position = movePos;

            // �A�j���[�V����
            m_Anim.State = CharacterAnimator.CharacterAnimState.walk;

            // ���C�����X�V
            m_TargetLine.SetLinePos(0, transform.position);

            // �ڕW���W�ɓ���
            if(transform.position == m_TargetPos)
            {
                // �ړ��I��
                m_Move = false;

                // �^�[�Q�b�g���폜
                DestroyTarget();

                // �A�j���[�V����
                m_Anim.State = CharacterAnimator.CharacterAnimState.idle;
            }
        }
    }


    // �^�[�Q�b�g�𐶐�����
    private void CreateTarget()
    {
        // �^�[�Q�b�g����������Ă��Ȃ�
        if (!m_MoveTarget)
        {
            // �ڈ�𐶐�
            m_MoveTarget = Instantiate(m_MoveTargetPrefab, m_TargetPos, Quaternion.identity).GetComponent<MoveTarget>();
        }

        // �^�[�Q�b�g��ڕW���W�Ɉړ���������
        m_MoveTarget.transform.position = m_TargetPos;
        m_MoveTarget.transform.rotation = Quaternion.identity;


        // ���C������������Ă��Ȃ�
        if (!m_TargetLine)
        {
            // ���C���𐶐�
            m_TargetLine = Instantiate(m_TargetLinePrefab).GetComponent<TargetLine>();
        }

        // ���C���̍��W��ݒ肷��
        m_TargetLine.SetLinePos(0, transform.position);
        m_TargetLine.SetLinePos(1, m_TargetPos);
    }

    // �^�[�Q�b�g���폜
    private void DestroyTarget()
    {
        // �^�[�Q�b�g���폜
        Destroy(m_MoveTarget.gameObject);
        m_MoveTarget = null;

        // ���C�����폜
        Destroy(m_TargetLine.gameObject);
        m_TargetLine = null;
    }
}
