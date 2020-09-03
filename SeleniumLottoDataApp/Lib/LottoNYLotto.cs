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
            Driver.Url = "https://nylottery.ny.gov/draw-game/?game=lotto";
        }

        private string searchDrawDate()
        {
            var hds = Driver.FindElements(By.ClassName("fprWXx"));
            var hd = hds[0];
            var dat = hd.Text.Split();
            var date = dat[1];
            return date;
        }


        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var nums = Driver.FindElements(By.ClassName("jsyPzH"));
            foreach (var num in nums)
            {
                if (!string.IsNullOrWhiteSpace(num.Text))
                {
                    numbers.Add(num.Text);
                }
            }
            var bonus = Driver.FindElements(By.ClassName("jaSWZA"));
            numbers.Add(bonus[0].Text);
            
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

                if (DateTime.Parse(currentDrawDate) > DateTime.Parse(lastDrawDate))
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
