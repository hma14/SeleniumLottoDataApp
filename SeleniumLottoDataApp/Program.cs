using SeleniumLottoDataApp.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp
{
    class Program
    {
        static void Main(string[] args)
        {
            LottoBase obj = new LottoFloridaFantasy5();
            obj.InsertDb();

#if false
            obj = new LottoBC49();
            obj.InsertDb();
            obj.InsertLottoNumberTable();

            obj = new Lottery649();
            obj.InsertDb();
            obj.InsertLottoNumberTable();

            obj = new LottoMAX();
            obj.InsertDb();
            obj.InsertLottoNumberTable();

            obj = new LottoCash4Life();
            obj.InsertDb();
            obj.InsertLottoNumberTable();

            

            obj = new LottoFloridaLotto();
            obj.InsertDb();

            obj = new LottoFloridaLucky();
            obj.InsertDb();
#endif
           

            //obj = new LottoNewYorkTake5();
            //obj.InsertDb();

            //obj = new LottoNYLotto();
            //obj.InsertDb();

           



            //obj = new LottoColorado();
            //obj.InsertDb();

            //obj = new LottoTexasCashFive();
            //obj.InsertDb();

            //obj = new LottoGermanLotto();
            //obj.InsertDb();

            //obj = new LottoConnecticutLotto();
            //obj.InsertDb();

            //obj = new LottoPowerBall();
            //obj.InsertDb();



            //obj = new LottoEuroMillions();
            //obj.InsertDb();

            //obj = new LottoEuroJackpot();
            //obj.InsertDb();


            // BC lotto


            //obj = new LottoMegaMillions();
            //obj.InsertDb();

            //obj = new LottoCaSuperlottoPlus();
            //obj.InsertDb();

            //// OZ Lottos

            //obj = new OZLottoMonday();
            //obj.InsertDb();

            //obj = new OZLottoTuesday();
            //obj.InsertDb();

            //obj = new OZLottoWednesday();
            //obj.InsertDb();

            //obj = new OZLottoSaturday();
            //obj.InsertDb();

            //obj = new LottoDailyGrand();
            //obj.InsertDb();

            ////////////////

            //obj = new LottoSuperLotto();
            //obj.InsertDb();

            //obj = new LottoSSQ();
            //obj.InsertDb();

            //obj = new LottoSevenLotto();
            //obj.InsertDb();


            obj.CloseDriver();
        }
    }
}
