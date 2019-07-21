using OpenQA.Selenium;
using SeleniumLottoDataApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoDailyGrand : LottoBase
    {
        public LottoDailyGrand()
        {
            Driver.Url = "https://www.playnow.com/lottery/daily-grand-winning-numbers/";
        }

        private string searchDrawDate()
        {
            var tds = Driver.FindElements(By.ClassName("product-date-picker__draw-date"));
            var txt = tds.First().Text;
            var ta = txt.Split(',');
            var year = ta[2];
            var da = ta[1].Split();
            var mon = DicDate[da[0]];
            var day = da[1];
            var dat = $"{year}-{mon}-{day}";
            return dat;

        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var td = Driver.FindElementsByClassName("result").First();
            var lis = td.FindElements(By.TagName("li"));
            foreach (var li in lis)
            {
                numbers.Add(li.Text);
            }
            return numbers;

        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db?.DailyGrand?.ToList();
                if (list.Count == 0)
                {
                    return;
                }
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (currentDrawDate != lastDrawDate)
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new DailyGrand();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);
                    entity.Grand = int.Parse(numbers[5]);


                    // save to db
                    db.DailyGrand.Add(entity);
                    db.SaveChanges();
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
