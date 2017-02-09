using OpenQA.Selenium;
using SeleniumLottoDataApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoTexasCashFive : LottoBase
    {
        public LottoTexasCashFive()
        {
            Driver.Url = "https://www.txlottery.org/export/sites/lottery/Games/Cash_Five/index.html";           
        }

        private string searchDrawDate()
        {
            var hs = Driver.FindElements(By.TagName("h3"));
            foreach( var h in hs)
            {
                if (h.Text.Contains("Cash Five Winning Numbers for"))
                {
                    var txts = h.Text.Split(' ');
                    var da = txts[5];
                    return da;
                }
            }
            return null;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var spans = Driver.FindElements(By.XPath("//ol[@class='winningNumberBalls']/li"));
            foreach (var span in spans)
            {
                if (!string.IsNullOrEmpty(span.Text) && Char.IsDigit(span.Text[0]) == true)
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
                var list = db.TexasCashFive.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (currentDrawDate != lastDrawDate)
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new TexasCashFive();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);


                    // save to db
                    db.TexasCashFive.Add(entity);
                    db.SaveChanges();
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
