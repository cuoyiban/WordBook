using System;
using System.Collections.Generic;
using PureMVC;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class TopBarMediator : ViewMediator , IMediator
{
	private UI_TopBar uiTopBar;

	private List<string> topUIName = new List<string> { UIEnum.Main,UIEnum.WordDesc , UIEnum.Setting };
	private new List<string> listCareOfEvent = new List<string> { EventEnum.COMMAND_STARTUP };
	public TopBarMediator(string strName):base(strName)
	{
		
	}

	public override void OnRegister()
	{
		base.OnRegister();
		uiTopBar = UIObj.GetComponent<UI_TopBar>();
		for (int i = 0; i < topUIName.Count; i++)
		{
			uiTopBar.SetCallBack(i, topUIName[i], SelectFunPage);
		}
		
	}

	private void SelectFunPage(int iIndex)
	{
		SendNotification(EventEnum.COMMAND_SELECT_FUN_PAGE, topUIName[iIndex]);
	}


	#region override func
	public override IList<string> ListNotificationInterests()
	{
		return listCareOfEvent;
	}

	public override void HandleNotification(INotification notification)
	{
		switch (notification.Name)
		{
			case EventEnum.COMMAND_STARTUP:
				Show();
				break;
			default:
				break;
		}
	}

	#endregion
}
