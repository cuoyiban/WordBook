using System;
using System.Collections.Generic;
using PureMVC;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class MainMenuMediator : Mediator , IMediator
{
	#region property about Mediator
	private List<string> listCareOfEvent = new List<string> { EventEnum.COMMAND_SELECT_FUN_PAGE };
	#endregion
	public UI_Menu menuMain;

	public MainMenuMediator(UI_Menu menuMain)
	{
		this.menuMain = menuMain;
	}

	private void SelectFunPage(string strUIName)
	{
		SendNotification(EventEnum.COMMAND_SELECT_FUN_PAGE, strUIName);
	}


	#region override func
	public override IList<string> ListNotificationInterests()
	{
		return base.ListNotificationInterests();
	}

	public override void HandleNotification(INotification notification)
	{
		base.HandleNotification(notification);
	}
	#endregion
}
