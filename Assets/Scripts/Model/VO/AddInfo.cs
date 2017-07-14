using System;
using System.Collections.Generic;

namespace Model.VO
{
	public class AddInfo
	{
		public string ID;
		public string Tag;
		public string RelatedWord;
		public string Context;
		public long AddTime;

		public AddInfo(string strRelatedWord , string strContext , long lAddTime)
		{
			Init(strRelatedWord ,strContext , lAddTime);
		}

		public AddInfo(string strRelatedWord , string strContext)
		{
			TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
			Init(strContext, strRelatedWord, Convert.ToInt64(ts.TotalSeconds));
		}

		public AddInfo(string strID , string strContext , string strTag , string strRelatedWord , long lAddTime)
		{
			Init(strID, strContext, strTag, strRelatedWord, lAddTime);
		}

		public void Init(string strRelatedWord , string strContext , long lAddTime)
		{
			Context = strContext;
			RelatedWord = strRelatedWord;
			AddTime = lAddTime;
		}

		public void Init(string strID, string strContext, string strTag, string strRelatedWord, long lAddTime)
		{
			ID = strID;
			Context = strContext;
			Tag = strTag;
			RelatedWord = strRelatedWord;
			AddTime = lAddTime;
		}
	}
}
