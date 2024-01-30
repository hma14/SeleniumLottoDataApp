using OpenQA.Selenium;
using SeleniumLottoDataApp.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SeleniumLottoDataApp.BusinessModels.Constants;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoCash4Life : LottoBase
    {
        public LottoCash4Life()
        {
            Driver.Url = "https://www.flalottery.com/cash4Life";
           
        }

        private string searchDrawDate()
        {
            var p = Driver.FindElement(By.ClassName("draw-date"));
            var txt = p.Text;
            txt = txt.Replace(",", "");
            var arr = txt.Split();
            var date = arr[3] + "-" + DicDateShort2[arr[1]] + "-" + arr[2];

            return date;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            //var spans = Driver.FindElements(By.XPath("//div[@class='gamePageBalls']/p/span"));

            var lis = Driver.FindElements(By.ClassName("game-numbers__number"));
            var arr = lis.Take(6).ToList();
            foreach (var a in arr)
            {
                numbers.Add(a.FindElement(By.TagName("span")).Text);
            }
            return numbers;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.Cash4Life.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault()?.Item2; 
                var currentDrawDate = searchDrawDate();

                if (DateTime.Parse(currentDrawDate) > DateTime.Parse(lastDrawDate))
                {
                    var lastDrawNumber = dates.LastOrDefault()?.Item1 ?? 0;
                    var numbers = searchDrawNumbers();
                    if (numbers != null)
                    {
                        var entity = new Cash4Life();
                        entity.DrawNumber = lastDrawNumber + 1;
                        entity.DrawDate = currentDrawDate;
                        entity.Number1 = int.Parse(numbers[0]);
                        entity.Number2 = int.Parse(numbers[1]);
                        entity.Number3 = int.Parse(numbers[2]);
                        entity.Number4 = int.Parse(numbers[3]);
                        entity.Number5 = int.Parse(numbers[4]);


                        // save to db
                        db.Cash4Life.Add(entity);

                        // save to CashBall table
                        var cashball = new Cash4Life_CashBall();
                        cashball.DrawNumber = lastDrawNumber + 1;
                        cashball.DrawDate = currentDrawDate;
                        cashball.CashBall = int.Parse(numbers[5]);
                        db.Cash4Life_CashBall.Add(cashball);

                        db.SaveChanges();
                    }
                }
            }
            Driver.Close();
            Driver.Quit();
        }

#if false
        internal override void InsertLottoNumberTable()
        {
            using (var db = new LottoDb())
            {
                var lotto = db.Cash4Life.ToList().Last();
                var prevLottoNumber = db.LottoNumber.ToList().Where(x => x.LottoName == LottoNames.LottoCash4Life).LastOrDefault();
                var prevDistance = prevLottoNumber != null ? prevLottoNumber.Distance : 0;

                if (lotto.DrawNumber == prevLottoNumber?.DrawNumber)
                    return;

                for (int i = 1; i <= (int)LottoNumberRange.LottoCash4Life; i++)
                {
                    LottoNumber entity = new LottoNumber
                    {
                        LottoName = LottoNames.LottoCash4Life,
                        DrawNumber = lotto.DrawNumber,
                        DrawDate = DateTime.Parse(lotto.DrawDate),
                        Number = i,
                        Distance = (lotto.Number1 != i &&
                                    lotto.Number2 != i &&
                                    lotto.Number3 != i &&
                                    lotto.Number4 != i &&
                                    lotto.Number5 != i) ? prevDistance + 1 : 0,

                        IsHit = (lotto.Number1 == i ||
                                    lotto.Number2 == i ||
                                    lotto.Number3 == i ||
                                    lotto.Number4 == i ||
                                    lotto.Number5 == i ) ? true : false,

                        NumberofDrawsWhenHit =
                                   (lotto.Number1 == i ||
                                    lotto.Number2 == i ||
                                    lotto.Number3 == i ||
                                    lotto.Number4 == i ||
                                    lotto.Number5 == i ) ? prevDistance + 1 : 0,
                    };

                    db.LottoNumber.Add(entity);
                    db.SaveChanges();
                }
            }
        }
#endif
    }
}
