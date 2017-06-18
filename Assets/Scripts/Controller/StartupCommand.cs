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
		SendNotification(EventEnum.COMMAND_SELECT_FUN_PAGE, UIEnum.Main);
	}
}
