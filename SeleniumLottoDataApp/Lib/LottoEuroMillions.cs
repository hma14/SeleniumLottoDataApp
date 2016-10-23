using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoEuroMillions : LottoBase
    {
        public LottoEuroMillions()
        {
            Driver.Url = "http://www.euro-millions.com/results.asp";           
        }

        private string searchDrawDate()
        {
            List<string> numbers = new List<string>();
            var table = Driver.FindElement(By.ClassName("resultsTable"));
            
            var trs = table.FindElements(By.TagName("tr"));
            var th = trs[1].FindElement(By.TagName("th")).Text.Split();
            var mo = DicDate[th[0]];
            var yr = th[1];
            var tds = trs[2].FindElements(By.TagName("td"));
            var dat = yr + "-" + mo + "-" +  tds[0].Text.Split()[2];
           
            return dat;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var table = Driver.FindElement(By.ClassName("resultsTable"));
            var trs = table.FindElements(By.TagName("tr"));
            var tds = trs[2].FindElements(By.TagName("td"));
            numbers = tds[1].Text.Split('-').ToList();
            var stars = trs[2].Text.Split('-').ToList();
            numbers.Add(stars[4].Trim().Split()[1]);
            numbers.Add(stars[5].Trim().Split()[0]);

            return numbers;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.EuroMillions.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (currentDrawDate != lastDrawDate)
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new EuroMillion();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);
                    
                    // save to db
                    db.EuroMillions.Add(entity);
                    db.SaveChanges();


                    // save to db for Euros
                    var euros = new EuroMillions_LuckyStars();
                    euros.DrawNumber = lastDrawNumber + 1;
                    euros.DrawDate = currentDrawDate;
                    euros.Star1 = int.Parse(numbers[5]);
                    euros.Star2 = int.Parse(numbers[6]);

                    db.EuroMillions_LuckyStars.Add(euros);
                    db.SaveChanges();

                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
