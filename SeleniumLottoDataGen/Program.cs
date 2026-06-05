using SeleniumLottoDataApp;
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
			//DailyGrandGen obj = new DailyGrandGen();
			//obj.ParseCsv();

			//NewYorkTake5Gen obj = new NewYorkTake5Gen();
			//obj.ParseCsv();

			//TexasCashFiveGen obj = new TexasCashFiveGen();
			//obj.ParseCsv();

			//FloridaFantasy5Gen obj = new FloridaFantasy5Gen();
			//obj.ParseData();

			FloridaPick3Gen obj = new FloridaPick3Gen();
			obj.ParseData();
		}
	}
}
