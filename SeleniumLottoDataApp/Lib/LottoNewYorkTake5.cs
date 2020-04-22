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
            Driver.Url = "https://nylottery.ny.gov/take-5";
        }

        private string searchDrawDate()
        {
            var datestr = Driver.FindElement(By.XPath("//div[@id='replace_LatestWinningNumbers']/p[@class='header4']"));
            var dat = datestr.Text.Split();
            var date = DateTime.Now.Year + "-" + DicDateShort2[dat[1]] + "-" + dat[2];
            return date;
        }


        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var dev = Driver.FindElement(By.XPath("//div[@id='replace_LatestWinningNumbers']"));
            var lis = dev.FindElements(By.TagName("li"));
            foreach(var li in lis.Take(5))
            {
                var span = li.FindElement(By.TagName("span"));
                numbers.Add(span.Text);
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

                if (DateTime.Parse(currentDrawDate) != DateTime.Parse(lastDrawDate))
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
