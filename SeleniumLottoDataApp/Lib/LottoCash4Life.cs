using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            var ps = Driver.FindElements(By.XPath("//div[@class='gamePageNumbers']/p"));
            var txt = ps[1].Text;
            txt = txt.Replace(",", "");
            var arr = txt.Split();
            //var da = arr[3] + '-' + DicDate[arr[1]] + "-" + arr[2];
            var da = DicDate[arr[1]] + "/" + arr[2] + "/" + arr[3];
            return da;
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
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (DateTime.Parse(currentDrawDate) >= DateTime.Parse(lastDrawDate))
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

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
            Driver.Close();
            Driver.Quit();
        }
    }
}
