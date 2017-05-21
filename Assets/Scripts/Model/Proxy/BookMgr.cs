using System.Collections.Generic;
using System;
using Model.VO;
using PureMVC.Interfaces;
using UnityEngine;


namespace Model.Proxy {
    public class BookMgr :IProxy
    {
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
            WordVO word = null;
            if (!m_dicBooks.ContainsKey(strBookName))
            {
                book = AddBook(strBookName);
			}
			else
			{
				book = m_dicBooks[strBookName];
			}
            if (!book.Words.ContainsKey(strWord))
            {
                word = new WordVO(strWord);
                book.Words.Add(strWord, word);
			}
			else
			{
				word = book.Words[strWord];
			}
			word.AddContext(strContext);
        }

		public void AddWord(string strWord , string strContext)
		{
			AddWord(m_strDefaultBook, strWord, strContext);
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


		#region Debug Func
		public string DebugInfo()
		{
			string str = "";
			str += string.Format("Book Count {0}\n" , m_dicBooks.Count);
			foreach (var item in m_dicBooks)
			{
				str += string.Format("Book {0} , has {1} Words \n" , item.Value.BookName , item.Value.Words.Count);
				foreach (var item2 in item.Value.Words)
				{
					str += string.Format("\t Word {0} , Count {1}\n", item2.Value.Spell, item2.Value.Count);
					for (int i = 0; i < item2.Value.Count; i++)
					{
						str += string.Format("\t\t Context :{0} , Time {1} , RelatedWord {2} \n", item2.Value.Contexts[i].Context, item2.Value.Contexts[i].AddTime, item2.Value.Contexts[i].RelatedWord);
					}
				}
			}
			return str;
		}
		#endregion
	}
}



