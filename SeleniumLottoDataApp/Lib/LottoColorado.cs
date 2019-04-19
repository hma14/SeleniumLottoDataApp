using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoColorado : LottoBase
    {
        public LottoColorado()
        {
            string url = "https://www.coloradolottery.com/en/games/lotto/";
            Driver.Navigate().GoToUrl(url);       
        }

        private string searchDrawDate()
        {
            var cls = Driver.FindElement(By.ClassName("winningNumbers"));
            var a = cls.FindElement(By.TagName("a"));
            var href = a.GetAttribute("href");
            var dat = href.Split('/')[7];
            return dat;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> NList = new List<string>();
            var draw = Driver.FindElements(By.ClassName("draw")).First();
            var spans = draw.FindElements(By.TagName("span"));
            foreach (var span in spans)
            {
                NList.Add(span.Text);
            }
            return NList;
        }

        internal override  void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.ColoradoLottoes.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (currentDrawDate != lastDrawDate)
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new ColoradoLotto();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);
                    entity.Number6 = int.Parse(numbers[5]);

                    
                    // save to db
                    db.ColoradoLottoes.Add(entity);
                    db.SaveChanges();
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
