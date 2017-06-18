using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;
public class ViewMediator : Mediator , IViewMediator
{
	protected List<string> listCareOfEvent = new List<string> { EventEnum.COMMAND_SELECT_FUN_PAGE };
	
	public virtual GameObject UIObj
	{
		get
		{
			return ViewComponent as GameObject;
		}
	}

	#region constructor
	public ViewMediator(string strUIName) : base(strUIName)
	{
		
	}
	#endregion

	public bool IsShow { get; set; }

	public virtual void DefaultProcess(INotification notification)
	{
		switch (notification.Name)
		{
			case EventEnum.COMMAND_SELECT_FUN_PAGE:
				if ((notification.Body as string) == MediatorName)
				{
					if (!IsShow)
					{
						Show();
					}
				}
				else
				{
					if (IsShow)
					{
						Hide();
					}
				}
				break;
			default:
				break;
		}
	}

	public virtual void Show()
	{
		IsShow = true;
		UIObj.SetActive(true);
	}

	public virtual void Hide()
	{
		IsShow = false;
		UIObj.SetActive(false);
	}

	public override IList<string> ListNotificationInterests()
	{
		return listCareOfEvent;
	}

	public override void HandleNotification(INotification notification)
	{
		DefaultProcess(notification);
	}

	public override void OnRegister()
	{
		Debug.Log(MediatorName + " is register");
		if (UIObj == null)
		{
			ViewComponent = UIManager.Instance.Get(MediatorName);
			if (ViewComponent == null)
			{
				Debug.LogWarning(MediatorName + "is not exist");
				return;
			}
		}
	}
}
