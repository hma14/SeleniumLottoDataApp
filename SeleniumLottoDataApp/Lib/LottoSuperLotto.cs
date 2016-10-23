using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoSuperLotto : LottoBase
    {
        public LottoSuperLotto()
        {
            Driver.Url = "http://www.lottery.gov.cn/historykj/history.jspx?_ltype=dlt";

        }

        private string searchDrawDate()
        {
            var tbl = Driver.FindElement(By.XPath("//div[@class='result']/table"));
            var tds = tbl.FindElements(By.TagName("td"));
            var da = tds[19].Text;

            return da;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var tbl = Driver.FindElement(By.XPath("//div[@class='result']/table"));
            var tds = tbl.FindElements(By.TagName("td"));

            for (int i = 1; i < 8; i++)
            {
                numbers.Add(tds[i].Text);
            }

            return numbers;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.SuperLottoes.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (currentDrawDate != lastDrawDate)
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new SuperLotto();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);

                    // save to db
                    db.SuperLottoes.Add(entity);
                    db.SaveChanges();

                    // SuperLotto_Rear
                    var ent = new SuperLotto_Rear();
                    ent.DrawNumber = lastDrawNumber + 1;
                    ent.DrawDate = currentDrawDate;
                    ent.RearNumber1 = int.Parse(numbers[5]);
                    ent.RearNumber2 = int.Parse(numbers[6]);

                    // save to db
                    db.SuperLotto_Rear.Add(ent);
                    db.SaveChanges();
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
