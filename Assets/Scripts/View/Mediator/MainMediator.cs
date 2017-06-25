using System;
using PureMVC;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using Model.Proxy;
using Model.VO;

public class MainMediator : ViewMediator, IMediator
{
	private BookMgr bookMgr;
	private List<WordVO> listWords;
	private UI_Main uiMain;
	private new List<string> listCareOfEvent = new List<string>();
	public MainMediator(string strUIName) : base(strUIName)
	{
		
	}
	#region override Mediator func
	public override void OnRegister()
	{
		base.OnRegister();
		uiMain = UIObj.GetComponent<UI_Main>();
		bookMgr = ApplicationFacade.Instance.RetrieveProxy(ProxyEnum.BOOK_MGR) as BookMgr;

		listWords = bookMgr.GetWordWithSort("Default");
		uiMain.RefreshWordList(listWords, 4);
	}

	public override IList<string> ListNotificationInterests()
	{
		List<string> temp = new List<string>(listCareOfEvent);
		temp.AddRange(base.listCareOfEvent);
		return temp;
	}

	public override void HandleNotification(INotification notification)
	{
		switch (notification.Name)
		{
			default:
				base.HandleNotification(notification);
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
