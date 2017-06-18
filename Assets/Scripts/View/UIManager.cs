using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
	private static UIManager m_Instance;
	public static UIManager Instance
	{
		get
		{
			if (m_Instance == null)
			{
				m_Instance = new UIManager();
			}
			return m_Instance;
		}
	}


	private Dictionary<string, string> m_dicUIContainer = new Dictionary<string, string>();
	private Dictionary<string, GameObject> m_dicUICache = new Dictionary<string, GameObject>();
	private GameObject m_objMainMenu;

	public Canvas UIRoot
	{
		get;set;
	}

	private void RegisterUI(string strName, string strPath)
	{
		if (m_dicUIContainer.ContainsKey(strName))
		{
			Debug.LogWarning("The ui named " + strName + " is exist");
			return;
		}
		m_dicUIContainer.Add(strName, strPath);
	}

	public void RegisterUI(string strName , GameObject obj)
	{
		if (m_dicUICache.ContainsKey(strName))
		{
			Debug.LogWarning("The ui named " + strName + " is exist");
		}
		m_dicUICache[strName] = obj;
	}

	public T Get<T>(string strUIName)
	{
		GameObject obj = Get(strUIName);
		if (obj == null)
		{
			Debug.Log("The ui named " + strUIName + " is not exist");
			return default(T);
		}
		return obj.GetComponent<T>();
	}

	public GameObject Get(string strUIName)
	{
		GameObject obj;
		if (!m_dicUICache.ContainsKey(strUIName))
		{
			if (!m_dicUIContainer.ContainsKey(strUIName))
			{
				Debug.Log("The ui named " + strUIName + " is not exist");
				return null;
			}
			obj = Resources.Load(m_dicUIContainer[strUIName]) as GameObject;
			obj.transform.SetParent(UIRoot.transform, false);
			obj.SetActive(false);
			m_dicUICache.Add(strUIName, obj);
		}
		obj = m_dicUICache[strUIName];
		return obj;
	}
}
