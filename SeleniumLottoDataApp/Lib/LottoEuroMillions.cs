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
            //var trs = Driver.FindElementsByClassName("dateRow");
            var td = Driver.FindElement(By.ClassName("date"));
            var array = td.Text.Split('\n');
            var dm = array.First().Split();
            var day = dm[1];
            day = day.Remove(day.Length - 2);           
            var mon = DicDate[dm[2]];
            var year = DateTime.Now.Year;

            var da = $"{year}-{mon}-{day}";

            return da;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();

            var newBalls = Driver.FindElements(By.ClassName("ball")).Take(5);
            foreach(var ball in newBalls)
            {
                numbers.Add(ball.Text);
            }
            var luckyStars = Driver.FindElements(By.ClassName("lucky-star")).Take(2);
            foreach(var star in luckyStars)
            {
                numbers.Add(star.Text);
            }

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

                if (DateTime.Parse(currentDrawDate) > DateTime.Parse(lastDrawDate))
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
