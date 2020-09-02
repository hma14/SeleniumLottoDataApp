using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoEuroJackpot : LottoBase
    {
        public LottoEuroJackpot()
        {
            Driver.Url = "http://www.euro-millions.com/eurojackpot-results.asp";           
        }

        private string searchDrawDate()
        {
            var divs = Driver.FindElements(By.ClassName("date"));

            var dat = divs.First().Text;

            var arr = dat.Split();
            var da = arr[3] + '-' + DicDate[arr[2]] + "-" + arr[1].Substring(0, arr[1].Length - 2);
            return da;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var div = Driver.FindElement(By.ClassName("latest-result-euro"));
            var ul = div.FindElement(By.ClassName("jack-balls"));
            var balls = ul.FindElements(By.ClassName("jack-ball"));
            foreach (var ball in balls)
            {
                numbers.Add(ball.Text);
            }
            var euros = ul.FindElements(By.ClassName("jack-euro"));
            foreach (var e in euros)
            {
                numbers.Add(e.Text);
            }
            

            return numbers;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.EuroJackpots.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (DateTime.Parse(currentDrawDate) > DateTime.Parse(lastDrawDate))
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new EuroJackpot();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);
                    
                    // save to db
                    db.EuroJackpots.Add(entity);
                    db.SaveChanges();


                    // save to db for Euros
                    var euros = new EuroJackpot_Euros();
                    euros.DrawNumber = lastDrawNumber + 1;
                    euros.DrawDate = currentDrawDate;
                    euros.Euro1 = int.Parse(numbers[5]);
                    euros.Euro2 = int.Parse(numbers[6]);

                    db.EuroJackpot_Euros.Add(euros);
                    db.SaveChanges();

                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
