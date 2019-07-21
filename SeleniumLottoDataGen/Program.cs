using SeleniumLottoDataApp.Lib;
using SeleniumLottoDataGen.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataGen
{
    class Program
    {
        static void Main(string[] args)
        {
            DailyGrandGen obj4 = new DailyGrandGen();
            obj4.ParseCsv();

            //NewYorkTake5Gen obj1 = new NewYorkTake5Gen();
            //obj1.ParseCsv();

            //TexasCashFiveGen obj2 = new TexasCashFiveGen();
            //obj2.ParseCsv();

            //FloridaFantasy5Gen obj3 = new FloridaFantasy5Gen();
            //obj3.ParseData();
        }
    }
}
