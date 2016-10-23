using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoFloridaLucky : LottoBase
    {
        public LottoFloridaLucky()
        {
            Driver.Url = "http://www.flalottery.com/luckyMoney";           
        }

        private string searchDrawDate()
        {
            var ps = Driver.FindElements(By.XPath("//div[@class='gamePageNumbers']/p"));
            var txt = ps[1].Text;
            txt = txt.Replace(",", "");
            var arr = txt.Split();
            var da = arr[3] + '-' + DicDate[arr[1]] + "-" + arr[2];
            return da;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var spans = Driver.FindElements(By.XPath("//div[@class='gamePageBalls']/p/span"));
            foreach (var span in spans)
            {
                if (Char.IsDigit(span.Text[0]) == true)
                {
                    numbers.Add(span.Text);
                }               
            }
            
            return numbers;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.FloridaLuckies.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (currentDrawDate != lastDrawDate)
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new FloridaLucky();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Lb = int.Parse(numbers[4]);

                    
                    // save to db
                    db.FloridaLuckies.Add(entity);
                    db.SaveChanges();
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
