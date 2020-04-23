using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoSuperLotto : LottoBase
    {
        public LottoSuperLotto()
        {
            string url = "http://www.lottery.gov.cn/dlt/";
            Driver.Navigate().GoToUrl(url);
        }

        private string searchDrawDate()
        {
            var span = Driver.FindElement(By.Id("_openTime_kj"));
            var dat = span.Text.Split()[0];
            dat = Regex.Replace(dat, @"[^\u0000-\u007F]+", "-").TrimEnd('-');

            return dat;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var ul = Driver.FindElement(By.Id("_codeNum_kj"));
            var lis = ul.FindElements(By.TagName("li"));
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
