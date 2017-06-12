using System;
using System.Collections.Generic;
using PureMVC;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class UI_M_MainMenu : Mediator , IMediator
{
	public UI_Menu menuMain;

	public UI_M_MainMenu(UI_Menu menuMain)
	{
		this.menuMain = menuMain;
	}


}
