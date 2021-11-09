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

        private DateTime searchDrawDate()
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            var ps = Driver.FindElements(By.XPath("//div[@class='gamePageNumbers']/p"));
            var txt = ps[1].Text;
            txt = txt.Replace(",", "");
            var arr = txt.Split();
            //var da = arr[3] + '-' + DicDate[arr[1]] + "-" + arr[2];
            var da = DicDate[arr[1]] + "/" + arr[2] + "/" + arr[3];
            return DateTime.Parse(da);
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var balls = Driver.FindElements(By.XPath("//div[@class='gamePageBalls']/p/span[@class='balls']"));
            foreach (var ball in balls)
            {
                var num = ball.GetAttribute("title");
                if (num != string.Empty)
                numbers.Add(num);
            }

            // get Cash Ball
            var cb = Driver.FindElements(By.XPath("//div[@class='gamePageBalls']/p/span[@class='balls c4lCBBall']"));
            var cashBall = cb.First().Text;
            numbers.Add(cashBall);
            return numbers;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.Cash4Life.ToList();
                IList<Tuple<int, DateTime>> dates = list.Select(x => new Tuple<int, DateTime>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault()?.Item2 ?? DateTime.Now.AddYears(-5); 
                var currentDrawDate = searchDrawDate();

                if (currentDrawDate > lastDrawDate)
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
                        DrawDate = lotto.DrawDate,
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
    }
}
