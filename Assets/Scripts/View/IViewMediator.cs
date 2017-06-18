using System;
using System.Collections.Generic;
using PureMVC.Interfaces;

public interface IViewMediator
{
	bool IsShow { get; set; }
	void DefaultProcess(INotification notification);
	void Show();
	void Hide();
}
