using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.VO
{
    public class WordVO
    {
        public string Spell;
        public List<AddInfo> Contexts;
		public long Count;
		public bool IsAlreadyLearn;//是否已学会
        public WordVO(string strSpell)
        {
            Spell = strSpell.ToLower();
            Contexts = new List<AddInfo>();
			Count = 0;
			IsAlreadyLearn = false;

		}

		public void AddContext(string strContext)
		{
			Contexts.Add(new AddInfo(strContext, Spell));
			Count++;
		}

		#region Debug Func
		public string DebugInfo()
		{
			string str = "";
			str += string.Format("Word {0} have {1} context \n", Spell, Count);
			for (int i = 0; i < Contexts.Count; i++)
			{
				str += Contexts[i].Context + " Time : " + Contexts[i].AddTime.ToString() + " RelatedWord is " + Contexts[i].RelatedWord  + "\n";
			}
			return str;
		}
		#endregion
	}
}
