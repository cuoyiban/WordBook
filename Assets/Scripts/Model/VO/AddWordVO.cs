

namespace Model.VO
{
	public class AddWordVO
	{
		public string BookName;
		public string Spell;
		public string Context;

		public AddWordVO(string strBookName , string strSpell , string strContext)
		{
			BookName = strBookName;
			Spell = strSpell;
			Context = strContext;
		}
	}
}
