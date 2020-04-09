using OpenQA.Selenium;
using SeleniumLottoDataApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoTexasCashFive : LottoBase
    {
        public LottoTexasCashFive()
        {
            Driver.Url = "https://www.lotteryusa.com/texas/cash-5/";
        }

        private string searchDrawDate()
        {
            var dat = Driver.FindElementByXPath("//time[@class='c-game-table__game-date']").GetAttribute("datetime");
            return dat;

        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var uls = Driver.FindElements(By.ClassName("c-result"));
            var ul = uls.First();
            var lis = ul.FindElements(By.TagName("li"));
            foreach (var li in lis.Take(5))
            {
                numbers.Add(li.Text);
            }
            return numbers;

        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.TexasCashFive.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (currentDrawDate != lastDrawDate)
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new TexasCashFive();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);


                    // save to db
                    db.TexasCashFive.Add(entity);
                    db.SaveChanges();
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
