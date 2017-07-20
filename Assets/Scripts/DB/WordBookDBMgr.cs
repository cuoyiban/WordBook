using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Data.Sqlite;
public class WordBookDBMgr
{
	private static WordBookDBMgr m_Instance;
	public static WordBookDBMgr Instance
	{
		get
		{
			if (m_Instance == null)
			{
				m_Instance = new WordBookDBMgr();
			}
			return m_Instance;
		}
	}

	private DbAccess db;

	private WordBookDBMgr()
	{
		db = new DbAccess("data source=wordbook.db");
	}


	private void initDB()
	{
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
}