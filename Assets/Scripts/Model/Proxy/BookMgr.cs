using System.Collections.Generic;
using System;
using Model.VO;
using PureMVC.Interfaces;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System.Text;
using LitJson;

namespace Model.Proxy {
    public class BookMgr :IProxy
    {
		private DbAccess db;
        private Dictionary<string, BookVO> m_dicBooks = new Dictionary<string, BookVO>();

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
			//AddWord("sapling", "Sepling is a baby true");
			//AddWord("sapling", "Sepling is so cute");
			//AddWord("sapling", "I like sapling");
			//AddWord("sapling", "Sepling is so cute");
			//AddWord("glass", "He need some glass");
			//AddWord("glass", "I have got glass");
			//AddWord("apple", "Apple is so rich");
			//InitDB();
			AddBook(m_strDefaultBook);
		}

        public BookVO AddBook(string strBookName , long lCreateTime)
        {
            BookVO book = null;
            if (!m_dicBooks.ContainsKey(strBookName))
            {
                book = new BookVO(strBookName , lCreateTime);
                m_dicBooks.Add(strBookName, book);
			}
			else
			{
				book = m_dicBooks[strBookName];
			}
            return book;
        }

		public BookVO AddBook(string strBookName)
		{
			return AddBook(strBookName, Util.GetCurTimeStamp());
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
			AddInfo addInfo = new AddInfo(Util.GetUUID() , strContext , "" , strWord , Util.GetCurTimeStamp());
			book.AddWord(strWord, addInfo);
			//DB_AddWordIfNotExist(strWord);
			//DB_AddAddInfo(addInfo.ID, addInfo.Tag, addInfo.RelatedWord, addInfo.Context, addInfo.AddTime);
        }

		public void AddWordToDefault(string strWord, string strContext)
		{
			AddWord(m_strDefaultBook, strWord, strContext);
		}

		public void AddWord(string strBookName , string strWord , AddInfo addInfo)
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
			book.AddWord(strWord, addInfo);
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

		public void SetWordState(string strBookName , string strWord , bool bAlreadyLearned)
		{
			BookVO bookVO;
			if (!m_dicBooks.ContainsKey(strBookName))
			{
				return;
			}
			bookVO = m_dicBooks[strBookName];
			bookVO.SetWordState(strWord, bAlreadyLearned);
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
								 Tag TEXT PRIMARY KEY
							)";

				//create Table "Book"
				cmds[1] = @"CREATE TABLE Book
							(
								 BookName TEXT PRIMARY KEY,
								 Tag TEXT,
								 CreateTime INTEGER
							)";

				//create Table "Word"
				cmds[2] = @"CREATE TABLE Word
							(
								 Spell TEXT PRIMARY KEY,
								 Tag TEXT
							)";
				//create Table "AddInfo"
				cmds[3] = @"CREATE TABLE AddInfo
							(
								 ID TEXT PRIMARY KEY,
								 Context TEXT,
								 Tag TEXT,
								 AddTime INTEGER,
								 Word TEXT,
								 FOREIGN KEY(Word) REFERENCES Word (Spell)
							)";
				//create Table "Word_AddInfo"
				cmds[4] = @"CREATE TABLE Word_AddInfo
							(
								 BookName TEXT,
								 Word TEXT,
								 AddInfoID TEXT ,
								 FOREIGN KEY(BookName) REFERENCES Book (BookName),
								 FOREIGN KEY(Word) REFERENCES Word (Spell),
								 FOREIGN KEY(AddInfoID) REFERENCES AddInfo (ID)
							)";
				//create Table "Word_LearnState"
				cmds[5] = @"CREATE TABLE Word_LearnState
							(    
								 BookName TEXT,
								 Word TEXT,
								 AlreadyLearn BOOLEAN,
								 FOREIGN KEY(BookName) REFERENCES Book (BookName)
								 FOREIGN KEY(Word) REFERENCES Word (Spell)
							)";
				cmds[6] = @"insert into Book (BookName , CREATETIME) values ('default' , " + Util.GetCurTimeStamp().ToString() + ")";
				for (int i = 0; i < cmds.Length; i++)
				{
					db.ExecuteQuery(cmds[i]);
				}
			}
			#endregion

			////Load data from db
			////Load Book
			//string cmd = "select * from Book";
			//using (SqliteDataReader dr = db.ExecuteQuery(cmd))
			//{
			//	while (dr.Read())
			//	{
			//		string strBookName = dr.GetString(dr.GetOrdinal("BookName"));
			//		long lCreateTime = dr.GetInt64(dr.GetOrdinal("CreateTime"));
			//		m_dicBooks.Add(strBookName, new BookVO(strBookName, lCreateTime));
			//	}
			//	dr.Close();
			//}

			////读取所有的单词添加记录
			//cmd = @"select AddInfo.ID AddInfo.Context AddInfo.Tag AddInfo.AddTime Word_AddInfo.BookName Word_AddInfo.Word
			//		from AddInfo inner join Word_AddInfo
			//		on AddInfo.ID = Word_AddInfo.AddInfoID";
			//using (SqliteDataReader dr = db.ExecuteQuery(cmd))
			//{
			//	while (dr.Read())
			//	{
			//		string strAddInfoID = dr.GetString(dr.GetOrdinal("AddInfo.ID"));
			//		string strContext = dr.GetString(dr.GetOrdinal("AddInfo.Context"));
			//		string strTag = dr.GetString(dr.GetOrdinal("AddInfo.Tag"));
			//		long lAddTime = dr.GetInt64(dr.GetOrdinal("AddInfo.AddTime"));
			//		string strBookName = dr.GetString(dr.GetOrdinal("Word_AddInfo.BookName"));
			//		string strWord = dr.GetString(dr.GetOrdinal("Word_AddInfo.Word"));
			//		AddWord(strBookName, strWord, new AddInfo(strAddInfoID, strContext, strTag, strWord, lAddTime));
			//	}
			//}

			////读取所有单词的学习状态
			//cmd = @"select * from Word_LearnState";
			//using (SqliteDataReader dr = db.ExecuteQuery(cmd))
			//{
			//	while (dr.Read())
			//	{
			//		//BookName TEXT,
			//		//Word TEXT,
			//		// AlreadyLearn BOOLEAN,
			//		string strBookName = dr.GetString(dr.GetOrdinal("BookName"));
			//		string strWord = dr.GetString(dr.GetOrdinal("Word"));
			//		bool bAlreadyLearned = dr.GetBoolean(dr.GetOrdinal("AlreadyLearn"));
			//	}
			//	dr.Close();
			//}
		}

		public void Exit()
		{
			db.CloseSqlConnection();
		}

		#region UpdateDB
		public void DB_AddWord(string strBookName , string strWord , string strAddInfoID , string strContext , string strTag , long lAddTime)
		{
			//查询单词本是否存在，无则添加
			DB_AddBookIfNotExist(strBookName);
			//查询单词是否存在，无则添加
			DB_AddWordIfNotExist(strWord);
			//添加 添加信心到AddInfo表
			//string cmd = 
			//添加 记录到关联表
		}

		public void DB_AddBookIfNotExist(string strBookName)
		{
			string cmd = "select" + Util.AddSpace(DBString.BookName) + "from" + Util.AddSpace(DBString.TB_Book) + "where" + Util.AddSpace(DBString.BookName) + "=" + Util.AddSpace(Util.StringToDBString(strBookName)); 
			using(SqliteDataReader dr = db.ExecuteQuery(cmd))
			{
				if (!dr.HasRows)
				{
					cmd = string.Format("insert into {0}({1} , {2}) values({3} , {4})", DBString.TB_Book, DBString.BookName, DBString.BookCreateTime, Util.StringToDBString(strBookName), Util.GetCurTimeStamp());
					db.ExecuteQuery(cmd);
				}
				dr.Close();
			}
		}

		public void DB_AddWordIfNotExist(string strWordName)
		{
			string cmd = string.Format("select {0} from {1} where {2} = {3}", "*", DBString.TB_Word, DBString.Word, Util.StringToDBString(strWordName));
			using (SqliteDataReader dr = db.ExecuteQuery(cmd))
			{
				if (!dr.HasRows)
				{
					cmd = string.Format("insert into {0}({1}) values({3})", DBString.TB_Word, DBString.Word, Util.StringToDBString(strWordName));
					db.ExecuteQuery(cmd);
				}
				dr.Close();
			}
		}

		public void DB_AddAddInfo(string strID , string strTag , string strRelatedWord , string strContext , long lAddTime)
		{
			string cmd = string.Format("select {0} from {1} where {2} = {3}", "*", DBString.TB_AddInfo, DBString.AddInfoID, Util.StringToDBString(strID));
			using (SqliteDataReader dr = db.ExecuteQuery(cmd))
			{
				if (!dr.HasRows)
				{
					cmd = string.Format("insert into {0} values({1},{2},{3},{4},{5})", DBString.TB_AddInfo,strID , strContext , strTag , lAddTime , strRelatedWord);
					db.ExecuteQuery(cmd);
				}
				else
				{

				}
				dr.Close();
			}
		}

		#endregion


		#region SaveLoad
		private string s_saveFilePath = "E:\\WordBookData.txt";
		public void Save()
		{
			string json = JsonMapper.ToJson(m_dicBooks);
			FileStream fs = new FileStream(s_saveFilePath, FileMode.Create);
			byte[] data = System.Text.Encoding.Default.GetBytes(json);
			fs.Write(data, 0, data.Length);
			fs.Flush();
			fs.Close();
		}

		public void Load()
		{
			FileStream file = new FileStream(s_saveFilePath, FileMode.Open);
			long beginPos = file.Seek(0, SeekOrigin.Begin);
			long endPos = file.Seek(0, SeekOrigin.End);
			file.Seek(0, SeekOrigin.Begin);
			byte[] data = new byte[endPos - beginPos];
			file.Read(data, 0, data.Length);
			Decoder d = Encoding.Default.GetDecoder();
			char[] charData = new char[data.Length];
			d.GetChars(data, 0, data.Length, charData, 0);
			string json = new string(charData);
			JsonMapper.RegisterImporter<Int32, Int64>(Util.Int32ToInt64);
			Dictionary<string, BookVO> temp = JsonMapper.ToObject<Dictionary<string, BookVO>>(json);
			file.Close();
			
		}


		#endregion
	}
}



