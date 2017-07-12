using System.Collections.Generic;
using System;
using Model.VO;
using PureMVC.Interfaces;
using UnityEngine;


namespace Model.Proxy {
    public class BookMgr :IProxy
    {
		private DbAccess db;
        private Dictionary<string, BookVO> m_dicBooks;

		#region IProxy
		public string ProxyName { get { return ProxyEnum.BOOK_MGR; } }
		public object Data { get; set; }

		public void OnRegister()
		{
			Debug.Log("BookMgr Register");
		}

		public void OnRemove()
		{
			Debug.Log("BookMgr Remove");
		}
		#endregion

		#region INotifier
		public void SendNotification(string notificationName)
		{

		}

		void SendNotification(string notificationName, object body, string type)
		{

		}
		#endregion

		#region Global Data
		private string m_strDefaultBook = "Default";
		#endregion
		public BookMgr()
        {
            m_dicBooks = new Dictionary<string, BookVO>();

			AddWord("sapling", "Sepling is a baby true");
			AddWord("sapling", "Sepling is so cute");
			AddWord("sapling", "I like sapling");
			AddWord("sapling", "Sepling is so cute");
			AddWord("glass", "He need some glass");
			AddWord("glass", "I have got glass");
			AddWord("apple", "Apple is so rich");
		}

        public BookVO AddBook(string strBookName)
        {
            BookVO book = null;
            if (!m_dicBooks.ContainsKey(strBookName))
            {
                book = new BookVO(strBookName);
                m_dicBooks.Add(strBookName, book);
            }
            return book;
        }

        public void AddWord(string strBookName , string strWord , string strContext)
        {
            BookVO book = null;
            if (!m_dicBooks.ContainsKey(strBookName))
            {
                book = AddBook(strBookName);
			}
			else
			{
				book = m_dicBooks[strBookName];
			}
			book.AddWord(strWord, strContext);
        }

		public void AddWord(string strWord , string strContext)
		{
			AddWord(m_strDefaultBook, strWord, strContext);
		}

		public WordVO GetWord(string strWord)
		{
			if (!m_dicBooks.ContainsKey(m_strDefaultBook))
			{
				return new WordVO(strWord);
			}
			return m_dicBooks[m_strDefaultBook].GetWord(strWord);
		}

		

		public List<WordVO> GetWordWithSort(string strBookName , Comparison<WordVO> sortFunc = null)
		{
			List<WordVO> values = new List<WordVO>(m_dicBooks[strBookName].Words.Values);
			values.Sort(sortFunc == null ? sortWithWordCount : sortFunc);
			return values;
		}

		//Default Sort Func
		private static int sortWithWordCount(WordVO word1 , WordVO word2)
		{
			if (word1.Count > word2.Count)
			{
				return -1;
			}
			else if (word1.Count == word2.Count)
			{
				return 0;
			}
			else
			{
				return 1;
			}
		}

		private void InitDB()
		{
			db = new DbAccess("data source=wordbook.db");
			//Check Table is exist
			
			bool bIsInit = db.IsTableExist("Book");
			if (!bIsInit)
			{
				//create Table "Book"
				string cmd = @"CREATE TABLE Book
								(
									 ID INTEGER PRIMARY KEY AUTOINCREMENT,
									 NAME TEXT,
									 TAG TEXT,
									 CREATETIME INTEGER,
								)
								";

				//create Table "Word"

				//create Table "AddInfo"

				//create Table "Tag"

				//create Table "Word_LearnState"
			}

		}

		#region Debug Func
		public string DebugInfo()
		{
			string str = "";
			//str += string.Format("Book Count {0}\n", m_dicBooks.Count);
			//foreach (var item in m_dicBooks)
			//{
			//	str += string.Format("Book {0} , has {1} Words \n", item.Value.BookName, item.Value.Words.Count);
			//	foreach (var item2 in item.Value.Words)
			//	{
			//		str += string.Format("\t Word {0} , Count {1}\n", item2.Value.Spell, item2.Value.Count);
			//		for (int i = 0; i < item2.Value.Count; i++)
			//		{
			//			str += string.Format("\t\t Context :{0} , Time {1} , RelatedWord {2} \n", item2.Value.Contexts[i].Context, item2.Value.Contexts[i].AddTime, item2.Value.Contexts[i].RelatedWord);
			//		}
			//	}
			//}
			return str;
		}
		#endregion
	}
}



