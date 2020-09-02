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
            Driver.Url = "https://www.ctlottery.org/#f";           
        }

        private string searchDrawDate()
        {
            var devs = Driver.FindElements(By.ClassName("winning-list-wrap"));
            var dat = devs.First().FindElement(By.TagName("time"));
            var arr = dat.Text.Split();
            var mon = arr[1].TrimEnd('.');
            var day = arr[2].TrimEnd(',');
            var date = arr[3] + "-" + DicDateShort[mon] + "-" + day;
            return date;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var cls = Driver.FindElements(By.ClassName("number-list"));
            var lis = cls.First().FindElements(By.TagName("li"));
            foreach(var li in lis)
            {
                numbers.Add(li.Text);
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

                if (DateTime.Parse(currentDrawDate) > DateTime.Parse(lastDrawDate))
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
