using System;
using System.Collections.Generic;
using PureMVC;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class SettingMediator : ViewMediator, IMediator
{
	private UI_Setting uiSetting;
	public SettingMediator(string strName):base(strName)
	{
		
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

	public override void OnRegister()
	{
		base.OnRegister();
		uiSetting = UIObj.GetComponent<UI_Setting>();
	}
	#endregion
}
