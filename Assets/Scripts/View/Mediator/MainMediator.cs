using System;
using PureMVC;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class MainMediator : Mediator, IMediator
{
	private UI_Main uiMain;
	private List<string> listCareOfEvent = new List<string> { EventEnum.COMMAND_SELECT_FUN_PAGE };
	public new const string NAME = UIEnum.Main;
	public MainMediator() : base(MainMediator.NAME)
	{
		Debug.Log("MainMediator()");
	}
	#region override Mediator func
	public override void OnRegister()
	{
		base.OnRegister();
	}

	public override IList<string> ListNotificationInterests()
	{
		return listCareOfEvent;
	}

	public override void HandleNotification(INotification notification)
	{
		switch (notification.Name)
		{
			case EventEnum.COMMAND_SELECT_FUN_PAGE:
				if (notification.Type == NAME)
				{

				}
				break;
			default:
				break;
		}
	}

	#endregion
}

//using UnityEngine;
//using System.Collections;
//using PureMVC.Patterns;
//using PureMVC.Interfaces;
//using System.Collections.Generic;

//public class UserFormMediator : Mediator, IMediator
//{
//	private UserProxy userProxy;

//	public new const string NAME = "UserFormMediator";

//	private UserForm View
//	{
//		get { return (UserForm)ViewComponent; }
//	}

//	public UserFormMediator(UserForm viewComponent)
//		: base(NAME, viewComponent)
//	{
//		Debug.Log("UserFormMediator()");

//		View.AddUser += UserForm_AddUser;
//		View.UpdateUser += UserForm_UpdateUser;
//		View.CancelUser += UserForm_CancelUser;
//	}

//	public override void OnRegister()
//	{
//		base.OnRegister();
//		userProxy = Facade.RetrieveProxy(UserProxy.NAME) as UserProxy;
//	}

//	void UserForm_AddUser()
//	{
//		UserVO user = View.User;
//		userProxy.AddItem(user);
//		SendNotification(EventsEnum.USER_ADDED, user);
//		View.ClearForm();
//	}

//	void UserForm_UpdateUser()
//	{
//		UserVO user = View.User;
//		userProxy.UpdateItem(user);
//		SendNotification(EventsEnum.USER_UPDATED, user);
//		View.ClearForm();
//	}

//	void UserForm_CancelUser()
//	{
//		SendNotification(EventsEnum.CANCEL_SELECTED);
//		View.ClearForm();
//	}

//	public override IList<string> ListNotificationInterests()
//	{
//		IList<string> list = new List<string>();
//		list.Add(EventsEnum.NEW_USER);
//		list.Add(EventsEnum.USER_DELETED);
//		list.Add(EventsEnum.USER_SELECTED);
//		return list;
//	}

//	public override void HandleNotification(INotification note)
//	{
//		UserVO user;
//		switch (note.Name)
//		{
//			case EventsEnum.NEW_USER:
//				user = (UserVO)note.Body;
//				View.ShowUser(user, UserFormMode.ADD);
//				break;

//			case EventsEnum.USER_DELETED:
//				View.ClearForm();
//				break;

//			case EventsEnum.USER_SELECTED:
//				user = (UserVO)note.Body;
//				View.ShowUser(user, UserFormMode.EDIT);
//				break;

//		}
//	}
//}
