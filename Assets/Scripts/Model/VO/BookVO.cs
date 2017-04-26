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
    }
}
