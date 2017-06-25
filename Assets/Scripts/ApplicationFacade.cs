using System;
using System.Collections.Generic;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using UnityEngine;


class ApplicationFacade : Facade
{
	public new static IFacade Instance
    {
        get
        {
            if (m_instance == null)
            {
                lock (m_staticSyncRoot)
                {
                    if (m_instance == null)
                    {
                        Debug.Log("ApplicationFacade");
                        m_instance = new ApplicationFacade();
                    }
                }
            }
            return m_instance;
        }
    }

	public void Startup()
	{
		Debug.Log("Startup() to SendNotification.");
		SendNotification(EventEnum.COMMAND_STARTUP);
	}

	public  ApplicationFacade()
	{
		
	}



	protected override void InitializeController()
	{
		base.InitializeController();
		RegisterCommand(EventEnum.COMMAND_STARTUP, typeof(StartupCommand));
		RegisterCommand(EventEnum.COMMAND_ADD_WORD, typeof(AddWordCommand));
	}

	protected override void InitializeView()
	{
		base.InitializeView();
		RegisterMediator(new TopBarMediator(UIEnum.TopBar));
		RegisterMediator(new MainMediator(UIEnum.Main));
		RegisterMediator(new SettingMediator(UIEnum.Setting));
		RegisterMediator(new WordDescMediator(UIEnum.WordDesc));
		
	}

	protected override void InitializeModel()
	{
		base.InitializeModel();
		RegisterProxy(new Model.Proxy.BookMgr());
	}
}
