using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Model.VO;
using Model.Proxy;

public class AddWordCommand : SimpleCommand, ICommand
{

	public override void Execute(INotification notification)
	{
		AddWordVO addWordVO = notification.Body as AddWordVO;
		BookMgr bookMgr = Facade.RetrieveProxy(ProxyEnum.BOOK_MGR) as BookMgr;
		bookMgr.AddWord(addWordVO.BookName, addWordVO.Spell, addWordVO.Context);
		SendNotification(EventEnum.COMMAND_WORD_ADDED , addWordVO);
	}
}
