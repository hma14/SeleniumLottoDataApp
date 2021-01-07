using OpenQA.Selenium;
using SeleniumLottoDataApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoNewYorkTake5 : LottoBase
    {
        public LottoNewYorkTake5()
        {
            Driver.Url = "https://nylottery.ny.gov/draw-game?game=take5";
        }

        private string searchDrawDate()
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            var hds = Driver.FindElements(By.ClassName("fprWXx"));
            var hd = hds[0];
            var dat = hd.Text.Split();
            var date = dat[1];
            return date;
        }


        private List<string> searchDrawNumbers()
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            List<string> numbers = new List<string>();
            var nums = Driver.FindElements(By.ClassName("jsyPzH"));
            foreach (var num in nums)
            {
                if (!string.IsNullOrWhiteSpace(num.Text))
                {
                    numbers.Add(num.Text);
                }
            }         

            return numbers;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.NewYorkTake5.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (DateTime.Parse(currentDrawDate) > DateTime.Parse(lastDrawDate))
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new NewYorkTake5();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);


                    // save to db
                    db.NewYorkTake5.Add(entity);
                    db.SaveChanges();
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
