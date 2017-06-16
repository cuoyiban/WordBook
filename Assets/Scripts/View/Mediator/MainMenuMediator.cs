using System;
using System.Collections.Generic;
using PureMVC;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class MainMenuMediator : Mediator , IMediator
{
	public UI_Menu menuMain;

	public MainMenuMediator(UI_Menu menuMain)
	{
		this.menuMain = menuMain;
	}


}
