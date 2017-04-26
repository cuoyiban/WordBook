using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.VO
{
    public class WordVO
    {
        public string Spell;
        public List<string> Contexts;
        public WordVO(string strSpell)
        {
            Spell = strSpell;
            Contexts = new List<string>();
        }
    }
}
