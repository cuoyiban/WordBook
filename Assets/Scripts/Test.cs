using System;
using System.Collections.Generic;
using Model.Proxy;
using Model.VO;
using UnityEngine;
using LitJson;

public class Test
{

	public static void TestWord()
	{
		//WordVO word = new WordVO("hello");
		//word.AddContext("Hello Han meimei");
		//word.AddContext("Hello Li Lei");
		//word.AddContext("Hello Liu han gang");

		//Debug.Log(word.DebugInfo());
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
		//BookMgr bookMgr = new BookMgr();
		//bookMgr.DB_AddBookIfNotExist("TestBook1");
		//bookMgr.DB_AddBookIfNotExist("TestBook2");
		//bookMgr.DB_AddBookIfNotExist("TestBook2");
		//bookMgr.Exit();
	}

	public static void TestJson()
	{
		Class c1 = new Class();
		c1.ClassName = "c1";
		c1.Num = 10;
		Student s1 = new Student();
		s1.ID = 1;
		s1.StudentName = "s1";
		Student s2 = new Student();
		s2.ID = 2;
		s2.StudentName = "s2";
		c1.dic_st = new Dictionary<string, List<Student>>();
		c1.dic_st.Add(s1.StudentName, new List<Student>() {s2});
		c1.dic_st.Add("s2", new List<Student>() { s1});
		string json = JsonMapper.ToJson(c1);
		Class c2 = JsonMapper.ToObject<Class>(json);
		Debug.Log(c2.ClassName);
		Debug.Log(c2.Num);
		foreach(var item in c2.dic_st)
		{
			var na = item.Key;
			var list = item.Value;
			for (int i = 0; i < list.Count; i++)
			{
				Debug.Log(list[i].ID + list[i].StudentName);
			}
		}
		Debug.Log(json);
	}

	public static void TestLoad()
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
		bookMgr.Save();
		bookMgr.Load();
	}
}


class Class
{
	public string ClassName { get; set; }
	public int Num { get; set; }
	public Dictionary<string, List<Student>> dic_st;
}

class Student
{
	public int ID { get; set; }
	public string StudentName { get; set; }

}
