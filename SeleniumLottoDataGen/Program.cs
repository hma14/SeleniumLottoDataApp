using SeleniumLottoDataGen.Lib;

namespace SeleniumLottoDataGen
{
    class Program
    {
        static void Main(string[] args)
        {

            LottoGenBase obj = new BC49DataGen();
            obj.ParseData();

#if false

            obj = new LottoMaxDataGen();
            obj.ParseData();

            obj = new Lotto649DataGen();
            obj.ParseData();          

            obj = new DailyGrandGen();
            obj.ParseData();

            obj = new DailyGrand_GrandNumberGen();
            obj.ParseData();

            obj = new DailyGrandGen();
            obj.ParseData();

            obj = new DailyGrand_GrandNumberGen();
            obj.ParseData();
#endif

            
#if false
            NewYorkTake5Gen obj1 = new NewYorkTake5Gen();
            obj1.ParseCsv();

            TexasCashFiveGen obj2 = new TexasCashFiveGen();
            obj2.ParseCsv();

            FloridaFantasy5Gen obj3 = new FloridaFantasy5Gen();
            obj3.ParseData();

            Cash4LifeGen obj = new Cash4LifeGen();
            obj.ParseData();
#endif
        }
    }
}
