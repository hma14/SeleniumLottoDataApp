using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoNYLotto : LottoBase
    {
        public LottoNYLotto()
        {
            Driver.Url = "https://nylottery.ny.gov/lotto";
        }

        private string searchDrawDate()
        {
            var hds = Driver.FindElements(By.ClassName("header4"));
            var hd = hds.Take(1).First();
            var dat = hd.Text.Split();
            var date = DateTime.Today.Year.ToString() + "-" + DicDateShort2[dat[1]] + "-" + dat[2];
            return date;
        }


        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var nums = Driver.FindElements(By.ClassName("winning-number"));
            foreach (var num in nums)
            {
                if (!string.IsNullOrWhiteSpace(num.Text))
                {
                    numbers.Add(num.Text);
                }
            }
            if (string.IsNullOrWhiteSpace(numbers[6]))
            {
                var bonus = Driver.FindElementByClassName("special-ball-number");
                numbers[6] = bonus.Text;
            }
            return numbers;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.NYLottoes.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (currentDrawDate != null && currentDrawDate != lastDrawDate)
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new NYLotto();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);
                    entity.Number6 = int.Parse(numbers[5]);
                    entity.Bonus = int.Parse(numbers[6]);

                    // save to db
                    db.NYLottoes.Add(entity);
                    db.SaveChanges();
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
