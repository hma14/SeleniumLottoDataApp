using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoSevenLotto : LottoBase
    {
        public LottoSevenLotto()
        {
            Driver.Url = "http://www.zhcw.com/kaijiang/zhcw_qlc_index.html";           
        }

        private string searchDrawDate()
        {
            var dat = Driver.FindElements(By.TagName("span"));
            var da = dat[0].Text;
            return da;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var list = Driver.FindElements(By.XPath("//li[@class='red']"));
            foreach (var lst in list)
            {
                numbers.Add(lst.Text);
            }
            var special = Driver.FindElement(By.XPath("//li[@class='blue']"));
            numbers.Add(special.Text);
            return numbers;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.SevenLottoes.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (currentDrawDate != lastDrawDate)
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new SevenLotto();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);
                    entity.Number6 = int.Parse(numbers[5]);
                    entity.Number7 = int.Parse(numbers[6]);
                    entity.Special = int.Parse(numbers[7]);

                    
                    // save to db
                    db.SevenLottoes.Add(entity);
                    db.SaveChanges();
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
