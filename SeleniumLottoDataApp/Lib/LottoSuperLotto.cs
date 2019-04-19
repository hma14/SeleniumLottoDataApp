using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoSuperLotto : LottoBase
    {
        public LottoSuperLotto()
        {
            string url = "https://www.magayo.com/lotto/china/super-lotto-results";
            Driver.Navigate().GoToUrl(url);
        }

        private string searchDrawDate()
        {
            var p = Driver.FindElement(By.XPath("//div[@class='small-12 medium-7 large-7 columns']/p"));
            var dat = p.Text.Split('(')[0];

            var d = dat.Replace(",", "").Split();
            var da = $"{d[2]}-{DicDateShort[d[0]]}-{d[1]}";
            return da;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var p = Driver.FindElement(By.XPath("//div[@class='small-12 medium-7 large-7 columns']/p"));
            var nums = p.FindElements(By.TagName("img"));
            foreach (var num in nums)
            {
                var n = num.GetAttribute("src").Split('=')[2].Split('.')[0];
                numbers.Add(n);
            }
            return numbers;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.SuperLottoes.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (currentDrawDate != lastDrawDate)
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new SuperLotto();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);

                    // save to db
                    db.SuperLottoes.Add(entity);
                    db.SaveChanges();

                    // SuperLotto_Rear
                    var ent = new SuperLotto_Rear();
                    ent.DrawNumber = lastDrawNumber + 1;
                    ent.DrawDate = currentDrawDate;
                    ent.RearNumber1 = int.Parse(numbers[5]);
                    ent.RearNumber2 = int.Parse(numbers[6]);

                    // save to db
                    db.SuperLotto_Rear.Add(ent);
                    db.SaveChanges();
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
