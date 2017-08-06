using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoMAX : LottoBase
    {
        public LottoMAX()
        {
            Driver.Url = "http://lotto.bclc.com/winning-numbers/lotto-max-and-extra.html";           
        }

        private string searchDrawDate()
        {
            var dat = Driver.FindElements(By.ClassName("date"));
            var arr = dat[0].Text.Split();
            var da = arr[2] + '-' + DicDateShort2[arr[0].ToUpper()] + "-" + arr[1].Substring(0, arr[1].Length - 1);
            return da;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> NList = new List<string>();
            var list = Driver.FindElements(By.XPath("//ul[@class='list-items']/li"));
            foreach (var lst in list)
            {
                NList.Add(lst.Text);
            }

            NList[1] = NList[1].Replace("Bonus", "").Trim();
            NList[0] = NList[0] + " " + NList[1];
            var numbers = NList[0].Split();

            return numbers.ToList();
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.LottoMaxes.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (currentDrawDate != lastDrawDate)
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new LottoMax();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);
                    entity.Number6 = int.Parse(numbers[5]);
                    entity.Number7 = int.Parse(numbers[6]);
                    entity.Bonus = int.Parse(numbers[7]);

                    
                    // save to db
                    db.LottoMaxes.Add(entity);
                    db.SaveChanges();
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
