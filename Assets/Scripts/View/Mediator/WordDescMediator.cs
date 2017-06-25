using System;
using PureMVC;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using Model.Proxy;
using Model.VO;

public class WordDescMediator : ViewMediator, IMediator
{
	private BookMgr bookMgr;
	private WordVO curWordVO;
	private UI_Word_Desc uiWordDesc;
	private new List<string> listCareOfEvent = new List<string>() {EventEnum.COMMAND_WORD_ADDED };
	public WordDescMediator(string strUIName) : base(strUIName)
	{

	}

	public override void OnRegister()
	{
		base.OnRegister();
		uiWordDesc = UIObj.GetComponent<UI_Word_Desc>();
		uiWordDesc.btnSearch.onClick.AddListener(SearchWord);
		uiWordDesc.btnAddContext.onClick.AddListener(AddContext);
		bookMgr = ApplicationFacade.Instance.RetrieveProxy(ProxyEnum.BOOK_MGR) as BookMgr;
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
			case EventEnum.COMMAND_WORD_ADDED:
				{
					AddWordVO addWordVO = notification.Body as AddWordVO;
					if (curWordVO != null && addWordVO.Spell == curWordVO.Spell)
					{
						ShowWorld(curWordVO.Spell);
						uiWordDesc.CleanSearchWord();
						uiWordDesc.CleanContext();
					}
				}
				break;
			default:
				base.HandleNotification(notification);
				break;
		}
	}

	public void ShowWorld(string strWord)
	{
		curWordVO = bookMgr.GetWord(strWord);
		uiWordDesc.ShowWord(curWordVO);
	}

	private void SearchWord()
	{
		string strSearchWord = uiWordDesc.GetSearchWord();
		if (strSearchWord == null || strSearchWord == "")
		{
			curWordVO = null;
		}
		else
		{
			curWordVO = bookMgr.GetWord(strSearchWord);
			uiWordDesc.CleanSearchWord();
		}
		uiWordDesc.ShowWord(curWordVO);
	}

	private void AddContext()
	{
		string strAddContext = uiWordDesc.GetContext();
		AddWordVO addWordVO = new AddWordVO(curWordVO.Spell , strAddContext);
		SendNotification(EventEnum.COMMAND_ADD_WORD, addWordVO);
	}
}
