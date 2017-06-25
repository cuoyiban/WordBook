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
	public const string COMMAND_STARTUP = "Startup";
	public const string COMMAND_WORD_ADDED = "AddedWord";
	public const string COMMAND_ADD_WORD = "AddWord";
	public const string COMMAND_WORD_REFRESH = "WordRefresh";
	public const string COMMAND_SELECT_FUN_PAGE = "Select_fun_page";//选择功能界面
	public const string COMMAND_SHOW_DIALOG = "Show_Dialog";//显示对话框
}

class ProxyEnum
{
	public static string BOOK_MGR = "BookMgr";
}

class UIEnum
{
	public const string TopBar = "TopBar";
	public const string Main = "Main";
	public const string Setting = "Setting";
	public const string WordDesc = "WordDesc";
}

class UIPathEnum
{
	public static string Main = "Assets\\UIPrefab\\UI_AddWord.prefab";
	public static string Main_Menu = "Assets\\UIPrefab\\UI_Main_Menu.prefab";
	public static string Setting = "Assets\\UIPrefab\\UI_Setting.prefab";
	public static string Add_Word = "Assets\\UIPrefab\\UI_Book.prefab";
}