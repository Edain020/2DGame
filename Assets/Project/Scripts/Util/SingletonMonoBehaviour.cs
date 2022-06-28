using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    // インスタンス
	private static T m_Instance;
	public static T Instance
    {
		get
		{
            // インスタンスが存在しない
			if (m_Instance == null)
			{
                // オブジェクトを検索
				m_Instance = (T)FindObjectOfType (typeof(T));

                // インスタンスが生成されていない
				if (m_Instance == null)
				{
					Debug.LogError (typeof(T) + "is nothing");
				}
			}

			return m_Instance;
		}
	}

	
	protected void Awake()
	{
        // シングルトン処理
		CheckInstance();
	}

    // 既にインスタンスが存在するか確認
	protected bool CheckInstance()
	{
        // 自身がインスタンス
		if( this == Instance)
        {
            return true;
        }

        // 既にインスタンスが存在するので自信を削除
		Destroy(this);
		return false;
	}
}
