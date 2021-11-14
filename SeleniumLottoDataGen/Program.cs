using SeleniumLottoDataGen.Lib;

namespace SeleniumLottoDataGen
{
    class Program
    {
        static void Main(string[] args)
        {
            LottoMaxDataGen obj = new LottoMaxDataGen();
            obj.ParseData();

            //Lotto649DataGen obj = new Lotto649DataGen();
            //obj.ParseData();

            //BC49DataGen obj = new BC49DataGen();
            //obj.ParseData();

            //DailyGrandGen obj4 = new DailyGrandGen();
            //obj4.ParseCsv();

            //NewYorkTake5Gen obj1 = new NewYorkTake5Gen();
            //obj1.ParseCsv();

            //TexasCashFiveGen obj2 = new TexasCashFiveGen();
            //obj2.ParseCsv();

            //FloridaFantasy5Gen obj3 = new FloridaFantasy5Gen();
            //obj3.ParseData();

            //Cash4LifeGen obj = new Cash4LifeGen();
            //obj.ParseData();

        }
    }
}
