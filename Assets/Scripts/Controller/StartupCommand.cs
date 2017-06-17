using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Model.Proxy;

public class StartupCommand : SimpleCommand, ICommand
{
	public override void Execute(INotification notification)
	{
		Debug.Log("StartupCommand.Execute()");
		//register mode
		Facade.RegisterProxy(new BookMgr());

		//register view
		//MainUI mainUI = notification.Body as MainUI;
		//Facade.RegisterMediator(new UserListMediator(mainUI.userList));
		//Facade.RegisterMediator(new UserFormMediator(mainUI.userForm));


		//register controll
		//Facade.RegisterCommand(EventEnum.COMMAND_SELECT_FUN_PAGE , typeof())
	}
}
