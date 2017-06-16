using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Enum
{
	public static string FACADE_APPLICATION = "FACADE_APPLICATION";
	
}

class EventEnum
{
	public static string COMMAND_STARTUP = "Startup";
	public static string COMMAND_WORD_ADDED = "WordAdded";
}

class ProxyEnum
{
	public static string BOOK_MGR = "BookMgr";
}

class UIEnum
{
	public static string Main = "MainUI";
	public static string Main_Menu = "MainMenu";
}

class UIPathEnum
{
	public static string Main = "Assets\\UIPrefab\\UI_AddWord.prefab";
	public static string Main_Menu = "Assets\\UIPrefab\\UI_Main_Menu.prefab";
	public static string Add_Word = "Assets\\UIPrefab\\UI_Book.prefab";
}
