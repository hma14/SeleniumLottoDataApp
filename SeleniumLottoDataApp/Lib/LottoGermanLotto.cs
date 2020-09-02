using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoGermanLotto : LottoBase
    {
        public LottoGermanLotto()
        {
            Driver.Url = "http://www.lotto.net/german-lotto/results";
        }

        private string searchDrawDate()
        {
            var dat = Driver.FindElement(By.ClassName("date"));
            var txt = dat.FindElement(By.TagName("span")).Text;
            var arr = txt.Split();
            var da = arr[0];

            var mo = arr[1][0] + arr[1].Substring(1, arr[1].Length - 1).ToLower();
            var yr = arr[2];
            var date = yr + "-" + DicDate[mo] + "-" + da; 

            return date;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var row = Driver.FindElement(By.ClassName("row-2"));
            var spans = row.FindElements(By.TagName("span"));
            foreach (var span in spans)
            {
                if (Char.IsDigit(span.Text[0]) == true)
                numbers.Add(span.Text.Trim());
            }
            return numbers;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.GermanLottoes.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (DateTime.Parse(currentDrawDate) > DateTime.Parse(lastDrawDate))
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new GermanLotto();
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
                    db.GermanLottoes.Add(entity);
                    db.SaveChanges();
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
