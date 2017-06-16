using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
	private static UIManager m_Instance;
	private Canvas m_canvasUIRoot;
	public UIManager Instance
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
	private GameObject m_objCurUI;
	private GameObject m_objMainMenu;

	private void Init()
	{
		m_canvasUIRoot = Transform.FindObjectOfType<Canvas>();
	}

	private void RegisterAllUI()
	{
		m_dicUIContainer.Add(UIEnum.Main, UIPathEnum.Main);
		m_dicUIContainer.Add(UIEnum.Main_Menu, UIPathEnum.Main_Menu);
	}

	private void RegisterUI(string strName , string strPath)
	{
		if (m_dicUIContainer.ContainsKey(strName))
		{
			Debug.Log("The ui named " + strName + " is exist");
			return;
		}
		m_dicUIContainer.Add(strName, strPath);
	}

	public T ShowUI<T>(string strUIName)
	{
		if (!m_dicUIContainer.ContainsKey(strUIName))
		{
			Debug.Log("The ui named " + strUIName + " is not exist");
			return default(T);
		}
		GameObject obj;
		if (!m_dicUICache.ContainsKey(strUIName))
		{
			obj = Resources.Load(m_dicUIContainer[strUIName]) as GameObject;
			obj.transform.SetParent(m_canvasUIRoot.transform, false);
			obj.SetActive(false);
			m_dicUICache.Add(strUIName, obj);
		}
		obj = m_dicUICache[strUIName];
		obj.SetActive(true);
		if (m_objCurUI != null)
		{
			m_objCurUI.SetActive(false);
		}
		m_objCurUI = obj;
		return obj.GetComponent<T>();
	}

	public T GetMainMenu()
	{
		GameObject obj;
		if (m_objMainMenu == null)
		{
			if (!m_dicUICache.ContainsKey(UIEnum.Main_Menu))
			{
				obj = Resources.Load(m_dicUIContainer[UIEnum.Main_Menu]) as GameObject;
				obj.transform.SetParent(m_canvasUIRoot.transform, false);
				obj.SetActive(true);
				m_dicUICache.Add(UIEnum.Main_Menu, obj);
			}
		}
		obj = m_dicUICache[UIEnum.Main_Menu];
		obj.SetActive(true);
		return obj.GetComponent<UI_Menu>();
	}
}
