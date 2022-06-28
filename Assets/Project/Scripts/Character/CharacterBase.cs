using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] private float m_MoveSpeed;
    [Header("移動中フラグ")]
    [SerializeField] private bool m_Move;
    [Header("目標座標")]
    [SerializeField] private Vector3 m_TargetPos;

    [Header("目標座標の目印")]
    [SerializeField] private MoveTarget m_MoveTargetPrefab;
    [Header("目標座標へのライン")]
    [SerializeField] private TargetLine m_TargetLinePrefab;


    private MoveTarget m_MoveTarget;    // 目標座標の目印
    private TargetLine m_TargetLine;    // 目標座標へのライン
    private CharacterAnimator m_Anim;   // アニメーション

    void Start()
    {
        // 変数の初期化
        m_Move = false;
        m_MoveTarget = null;

        // コンポーネントを取得
        m_Anim = GetComponentInChildren<CharacterAnimator>();
    }

    void Update()
    {
        // 右クリックした位置に目標座標をセットする
        if(Input.GetMouseButtonDown(1))
        {
            // マウス座標を取得する
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0f;
            // マウス座標をスクリーン座標からワールド座標に変換
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);

            // クリックした座標に地面タイルが存在するか確認する
            GroundMapManager ground = GroundMapManager.Instance;
            if (ground.CheckGroundTile(targetPos))
            {
                // クリックした座標をタイルの座標に変換
                targetPos = ground.GetTilePos(targetPos);

                // 同じ場所に移動しようとしていたら処理しない(してもしなくても同じ)
                if (targetPos != m_TargetPos)
                {
                    // 目標座標を設定
                    m_TargetPos = targetPos;
                    m_Move = true;

                    // ターゲットを生成
                    CreateTarget();
                }
            }
        }

        // 移動中の処理
        if (m_Move)
        {
            // 移動先の座標を計算
            Vector3 movePos = Vector3.MoveTowards(transform.position, m_TargetPos, m_MoveSpeed * Time.deltaTime);
            // 現在座標と移動先の座標から移動量を求める
            Vector3 moveVec = movePos - transform.position;
            // 移動量から左右の向きを考え、必要に応じて反転
            if (moveVec.x != 0f && moveVec.x / Mathf.Abs(moveVec.x) != transform.localScale.x / Mathf.Abs(transform.localScale.x))
            {
                // 左右判定
                Vector3 scale = transform.localScale;
                scale.x *= -1f;
                transform.localScale = scale;
            }
            // 移動
            transform.position = movePos;

            // アニメーション
            m_Anim.State = CharacterAnimator.CharacterAnimState.walk;

            // ラインを更新
            m_TargetLine.SetLinePos(0, transform.position);

            // 目標座標に到着
            if(transform.position == m_TargetPos)
            {
                // 移動終了
                m_Move = false;

                // ターゲットを削除
                DestroyTarget();

                // アニメーション
                m_Anim.State = CharacterAnimator.CharacterAnimState.idle;
            }
        }
    }


    // ターゲットを生成する
    private void CreateTarget()
    {
        // ターゲットが生成されていない
        if (!m_MoveTarget)
        {
            // 目印を生成
            m_MoveTarget = Instantiate(m_MoveTargetPrefab, m_TargetPos, Quaternion.identity).GetComponent<MoveTarget>();
        }

        // ターゲットを目標座標に移動し初期化
        m_MoveTarget.transform.position = m_TargetPos;
        m_MoveTarget.transform.rotation = Quaternion.identity;


        // ラインが生成されていない
        if (!m_TargetLine)
        {
            // ラインを生成
            m_TargetLine = Instantiate(m_TargetLinePrefab).GetComponent<TargetLine>();
        }

        // ラインの座標を設定する
        m_TargetLine.SetLinePos(0, transform.position);
        m_TargetLine.SetLinePos(1, m_TargetPos);
    }

    // ターゲットを削除
    private void DestroyTarget()
    {
        // ターゲットを削除
        Destroy(m_MoveTarget.gameObject);
        m_MoveTarget = null;

        // ラインを削除
        Destroy(m_TargetLine.gameObject);
        m_TargetLine = null;
    }
}
