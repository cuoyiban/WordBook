using System;
using System.Collections.Generic;
using Model.Proxy;
using Model.VO;
using UnityEngine;

public class Test
{

	public static void TestWord()
	{
		WordVO word = new WordVO("hello");
		word.AddContext("Hello Han meimei");
		word.AddContext("Hello Li Lei");
		word.AddContext("Hello Liu han gang");

		Debug.Log(word.DebugInfo());
	}

	//public static void TestBook()
	//{

	//}

	public static void TestBookMgr()
	{
		BookMgr bookMgr = new BookMgr();
		bookMgr.AddWord("Book1", "Hello", "Hello Han meimei");
		bookMgr.AddWord("Book1", "Hello", "Hello Li Lei");
		bookMgr.AddWord("Book1", "Hello", "Hello Liu han gang");
		bookMgr.AddWord("Book1", "Fine", "Fine ,and you?");

		bookMgr.AddBook("Book2");

		bookMgr.AddWord("condition", "CG condition something about C");
		bookMgr.AddWord("condition", "The Book condition Gold");
		bookMgr.AddWord("container", "1container hahahah");
		bookMgr.AddWord("container", "2container hahahah");
		for (int i = 0; i < 10; i++)
		{
			bookMgr.AddWord("word1", "word1 context" + (i + 1).ToString());
		}

		for (int i = 0; i < 7; i++)
		{
			bookMgr.AddWord("word2", "word2 context" + (i + 1).ToString());
		}

		for (int i = 0; i < 5; i++)
		{
			bookMgr.AddWord("word3", "word3 context" + (i + 1).ToString());
		}

		bookMgr.AddWord("Book2", "boss", "boss liu");
		List<WordVO> sortWord = bookMgr.GetWordWithSort("Default");


		string str = "";
		for (int i = 0; i < sortWord.Count; i++)
		{
			str += sortWord[i].DebugInfo();
		}
		Debug.Log(str);

		//Debug.Log(bookMgr.DebugInfo());
	}

	public static void TestGUID()
	{
		Debug.Log(System.Guid.NewGuid().ToString("N"));
		Debug.Log(System.Guid.NewGuid().ToString("N"));
		Debug.Log(System.Guid.NewGuid().ToString("N"));
	}

	public static void TestDB()
	{
		BookMgr bookMgr = new BookMgr();
		bookMgr.DB_AddBookIfNotExist("TestBook1");
		bookMgr.DB_AddBookIfNotExist("TestBook2");
		bookMgr.DB_AddBookIfNotExist("TestBook2");
		bookMgr.Exit();
	}
}
