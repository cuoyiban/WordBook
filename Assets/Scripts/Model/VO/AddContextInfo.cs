using System;
using System.Collections.Generic;

namespace Model.VO
{
	public class AddContextInfo
	{ 
		public string RelatedWord;
		public string Context;
		public long AddTime;

		public AddContextInfo(string strRelatedWord , string strContext , long lAddTime)
		{
			Init(strRelatedWord ,strContext , lAddTime);
		}

		public AddContextInfo(string strRelatedWord , string strContext)
		{
			TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
			Init(strContext, strRelatedWord, Convert.ToInt64(ts.TotalSeconds));
		}

		public void Init(string strRelatedWord , string strContext , long lAddTime)
		{
			Context = strContext;
			RelatedWord = strRelatedWord;
			AddTime = lAddTime;
		}
	}
}
