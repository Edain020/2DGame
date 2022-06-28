using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    // �C���X�^���X
	private static T m_Instance;
	public static T Instance
    {
		get
		{
            // �C���X�^���X�����݂��Ȃ�
			if (m_Instance == null)
			{
                // �I�u�W�F�N�g������
				m_Instance = (T)FindObjectOfType (typeof(T));

                // �C���X�^���X����������Ă��Ȃ�
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
        // �V���O���g������
		CheckInstance();
	}

    // ���ɃC���X�^���X�����݂��邩�m�F
	protected bool CheckInstance()
	{
        // ���g���C���X�^���X
		if( this == Instance)
        {
            return true;
        }

        // ���ɃC���X�^���X�����݂���̂Ŏ��M���폜
		Destroy(this);
		return false;
	}
}
