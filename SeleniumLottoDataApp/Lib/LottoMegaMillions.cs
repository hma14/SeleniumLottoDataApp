using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoMegaMillions : LottoBase
    {
        public LottoMegaMillions()
        {
            Driver.Url = "http://www.megamillions.com/winning-numbers";           
        }

        private string searchDrawDate()
        {
            var da = Driver.FindElement(By.Id("lastestDate"));
            var dat = da.Text.Split()[1];
            return dat;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var balls = Driver.FindElements(By.ClassName("ball"));
           foreach (var ball in balls.Take(6))
            {
                numbers.Add(ball.Text);
            }            
            return numbers;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.MegaMillions.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (DateTime.Parse(currentDrawDate) > DateTime.Parse(lastDrawDate))
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new MegaMillion();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);
                    
                    // save to db
                    db.MegaMillions.Add(entity);
                    db.SaveChanges();


                    // save to db for Euros
                    var mega = new MegaMillions_MegaBall();
                    mega.DrawNumber = lastDrawNumber + 1;
                    mega.DrawDate = currentDrawDate;
                    mega.MegaBall = int.Parse(numbers[5]);

                    db.MegaMillions_MegaBall.Add(mega);
                    db.SaveChanges();

                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
