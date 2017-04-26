﻿using System;
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

    public  ApplicationFacade()
	{
		
	}



	protected override void InitializeController()
	{
		base.InitializeController();
		RegisterCommand(EventEnum.COMMAND_STARTUP, typeof(StartupCommand));
	}
}
