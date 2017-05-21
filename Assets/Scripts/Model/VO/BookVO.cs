using System;
using System.Collections.Generic;
using Model.VO;

namespace Model.VO
{
    public class BookVO
    {
        public string BookName;
        public Dictionary<string, WordVO> Words;
        public BookVO(string strBookName)
        {
            BookName = strBookName;
            Words = new Dictionary<string, WordVO>();
        }

		#region Debug Func
		//public string DebugInfo()
		//{
		//	string str = "";
		//	str += string.Format("Book {0} have {1} words \n", BookName, Words.Count);
		//	for (int i = 0; i < Contexts.Count; i++)
		//	{
		//		str += Contexts[i].Context + " Time : " + Contexts[i].AddTime.ToString() + " RelatedWord is " + Contexts[i].RelatedWord + "\n";
		//	}
		//	return str;
		//}
		#endregion
	}
}
