using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoConnecticutLotto : LottoBase
    {
        public LottoConnecticutLotto()
        {
            Driver.Url = "http://www.ctlottery.org/Modules/Games/default.aspx?id=6";           
        }

        private string searchDrawDate()
        {
            var dat = Driver.FindElement(By.ClassName("date"));
            var arr = dat.Text.Split();         
            var date = arr[2] + "-" + DicDate[arr[0]] + "-" + arr[1].Substring(0, arr[1].Length - 1);
            return date;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var cls = Driver.FindElement(By.ClassName("p1"));
            var balls = cls.FindElements(By.ClassName("ball"));
            foreach(var ball in balls)
            {
                numbers.Add(ball.Text);
            }

            return numbers;          
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.ConnecticutLottoes.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (currentDrawDate != lastDrawDate)
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new ConnecticutLotto();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);
                    entity.Number6 = int.Parse(numbers[5]);

                    // save to db
                    db.ConnecticutLottoes.Add(entity);
                    db.SaveChanges();                  
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
