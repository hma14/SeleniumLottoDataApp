using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoFloridaPick3 : LottoBase
    {
        public LottoFloridaPick3()
        {
            Driver.Url = "http://flalottery.com/lotto.do";
        }

        private string searchDrawDate()
        {
            var ps = Driver.FindElements(By.XPath("//div[@class='gamePageNumbers']/p"));
            var txt = ps[1].Text;
            txt = txt.Replace(",", "");
            var arr = txt.Split();
            var date = arr[3] + "-" + DicDate[arr[1]] + "-" + arr[2];

            return date;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            //var spans = Driver.FindElements(By.XPath("//div[@class='gamePageBalls']/p/span"));

            var div = Driver.FindElements(By.ClassName("gamePageBalls")).First();
            var spans = div.FindElements(By.ClassName("balls"));
            foreach (var span in spans)
            {
                if (Char.IsDigit(span.Text[0]) == true)
                numbers.Add(span.Text);
            }
            //var mb = Driver.FindElement(By.ClassName("multiplier"));
            //numbers.Add(mb.Text[1].ToString());

            return numbers;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.FloridaPick3.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (DateTime.Parse(currentDrawDate) > DateTime.Parse(lastDrawDate))
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new FloridaPick3();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Fb = int.Parse(numbers[3]);

                    // save to db
                    db.FloridaPick3.Add(entity);
                    db.SaveChanges();
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
