using System.Collections.Generic;
using System;
using Model.VO;
using PureMVC.Interfaces;
using UnityEngine;
using Mono.Data.Sqlite;


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

			#region Create Table If it not exist
			bool bIsInit = db.IsTableExist("Book");
			if (!bIsInit)
			{
				string[] cmds = new string[7];
				//create Table "Tag"
				cmds[0] = @"CREATE TABLE Tag
						(
							 TAG TEXT PRIMARY KEY
						)";

				//create Table "Book"
				cmds[1] = @"CREATE TABLE Book
								(
									 NAME TEXT NAME TEXT PRIMARY KEY,
									 TAG TEXT,
									 CREATETIME INTEGER
								)";

				//create Table "Word"
				cmds[2] = @"CREATE TABLE Word
						(
							 WORD TEXT PRIMARY KEY,
							 TAG TEXT
						)";
				//create Table "AddInfo"
				cmds[3] = @"CREATE TABLE AddInfo
						(
							 ID INTEGER PRIMARY KEY AUTOINCREMENT,
							 CONTEXT TEXT,
							 TAG TEXT,
							 ADDTIME INTEGER,
							 WORD TEXT,
							 FOREIGN KEY(WORD) REFERENCES Word (WORD)
						)";
				//create Table "Word_AddInfo"
				cmds[4] = @"CREATE TABLE Word_AddInfo
						(
							 BOOK TEXT,
							 WORD TEXT,
							 ADDINFO ID,
							 FOREIGN KEY(BOOK) REFERENCES Book (NAME),
							 FOREIGN KEY(WORD) REFERENCES Word (WORD),
							 FOREIGN KEY(ADDINFO) REFERENCES AddInfo (ID)
						)
						";
				//create Table "Word_LearnState"
				cmds[5] = @"CREATE TABLE Word_LearnState
						(
     
							 BOOK INTEGER ,
							 WORD TEXT,
							 ALREADYLEARN  BOOLEAN,
							 FOREIGN KEY(BOOK ) REFERENCES Book (ID)
							 FOREIGN KEY(WORD) REFERENCES Word (WORD)
						)";
				cmds[6] = @"insert into Book (NAME , CREATETIME) values ('default' , " + Util.GetCurTimeStamp().ToString() + ")";
				for (int i = 0; i < cmds.Length; i++)
				{
					db.ExecuteQuery(cmds[i]);
				}
			}
			#endregion

			//Load data from db
			//Load Book
			string cmd = "select * from Book";
			using (SqliteDataReader dr = db.ExecuteQuery(cmd))
			{
				while (dr.Read())
				{
					string strBookName = dr.GetString(dr.GetOrdinal("NAME"));
					long lCreateTime = dr.GetInt64(dr.GetOrdinal("CREATETIME"));
				}
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



