using System.Collections.Generic;
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
            if (!book.Words.ContainsKey(strWord))
            {
                word = new WordVO(strWord);
                book.Words.Add(strWord, word);
            }
            word.Contexts.Add(strContext);
        }
    }
}



