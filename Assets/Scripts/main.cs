using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns;


public class main : MonoBehaviour {
	public Canvas uiRoot;
	public UI_TopBar uiTopBar;
	public UI_Main uiMain;
	public UI_Setting uiSetting;
	public UI_Word_Desc uiWordDesc;
	
	// Use this for initialization
	void Start () {
		//Startup();
		TT();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Startup()
	{
		//Init UIManger
		UIManager.Instance.UIRoot = uiRoot;
		UIManager.Instance.RegisterUI(UIEnum.TopBar, uiTopBar.gameObject);
		UIManager.Instance.RegisterUI(UIEnum.Main, uiMain.gameObject);
		UIManager.Instance.RegisterUI(UIEnum.Setting, uiSetting.gameObject);
		UIManager.Instance.RegisterUI(UIEnum.WordDesc, uiWordDesc.gameObject);
		ApplicationFacade facade = ApplicationFacade.Instance as ApplicationFacade;
		facade.Startup();
	}

	void TT()
	{
		Test.TestDB();
	}
}
