using System;
using System.Collections.Generic;
using Model.VO;

namespace Model.VO
{
	public class BookVO
	{
		private string BookName;
		private long m_lCreateTime;
		public Dictionary<string, WordVO> Words { get; set; }
		public BookVO(string strBookName)
		{
			BookName = strBookName;
			Words = new Dictionary<string, WordVO>();
		}

		public BookVO(string strBookName , long lCreateTime)
		{
			BookName = strBookName;
			m_lCreateTime = lCreateTime;
			Words = new Dictionary<string, WordVO>();
		}

		public WordVO GetWord(string strSpell)
		{
			WordVO wordVO = null;
			if (!Words.ContainsKey(strSpell))
			{
				return new WordVO(strSpell);
			}
			else
			{
				wordVO = Words[strSpell];
			}
			return wordVO;
		}

		public void AddWord(string strSpell)
		{
			if (!Words.ContainsKey(strSpell))
			{
				Words.Add(strSpell, new WordVO(strSpell));
			}
		}

		public void AddWord(string strSpell , AddInfo addInfo)
		{

			WordVO wordVO = null;
			if (!Words.ContainsKey(strSpell))
			{
				wordVO = new WordVO(strSpell);
				Words.Add(strSpell, wordVO);
			}
			else
			{
				wordVO = Words[strSpell];
			}
			wordVO.AddAddInfo(addInfo);
		}

		public void AddWord(string strSpell, string strContext)
		{
			WordVO wordVO = null;
			if (!Words.ContainsKey(strSpell))
			{
				wordVO = new WordVO(strSpell);
				Words.Add(strSpell, wordVO);
			}
			else
			{
				wordVO = Words[strSpell];
			}
			wordVO.AddContext(strContext);
		}

		public void SetWordState(string strWord , bool bAlreadyLearned)
		{
			WordVO wordVO = null;
			if (!Words.ContainsKey(strWord))
			{
				return;
			}
			else
			{
				wordVO = Words[strWord];
			}
			wordVO.IsAlreadyLearn = bAlreadyLearned;
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
