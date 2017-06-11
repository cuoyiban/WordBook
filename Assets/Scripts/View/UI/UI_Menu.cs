using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class UI_Menu : MonoBehaviour {
	public Dropdown menuDropDown;
	public Text labelTitle;

	List<System.Action> m_listAction = new List<System.Action>();
	// Use this for initialization
	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Init()
	{
		menuDropDown.ClearOptions();
		menuDropDown.onValueChanged.AddListener(OnChangeValue);
		menuDropDown.value = 0;
	}

	public void OnChangeValue(int iMenuIndex)
	{
		m_listAction[iMenuIndex]();
	}

	public void AddItemCallBack(params object[] objects)
	{
		for (int i = 0; i < objects.Length / 2; i += 2)
		{
			string strItemName = objects[i] as string;
			System.Action action = objects[i + 1] as System.Action;
			m_listAction.Add(action);
			Dropdown.OptionData option = new Dropdown.OptionData();
			option.text = strItemName;
			menuDropDown.options.Add(option);
		}
		menuDropDown.captionText.text = menuDropDown.options[0].text;
		menuDropDown.value = 0;
	}
}
