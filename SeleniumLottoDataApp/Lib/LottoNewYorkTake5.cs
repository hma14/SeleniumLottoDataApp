using OpenQA.Selenium;
using SeleniumLottoDataApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoNewYorkTake5 : LottoBase
    {
        public LottoNewYorkTake5()
        {
            Driver.Url = "http://nylottery.ny.gov/wps/portal/Home/Lottery/Home/Daily+Games/TAKE+5";           
        }

        private string searchDrawDate()
        {
            var div = Driver.FindElement(By.ClassName("WinningNumbersText"));
            var txt = div.Text;
            txt = txt.Replace(",", "");
            var arr = txt.Split();
            var da = DicDateShort[arr[0]] + "/" + arr[1] + '/' + arr[2];
            return da;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var divs = Driver.FindElements(By.ClassName("WinningNumbersResultsTake5Landing"));
            foreach (var div in divs)
            {
                if (Char.IsDigit(div.Text[0]) == true)
                {
                    numbers.Add(div.Text);
                }               
            }
            
            return numbers;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.NewYorkTake5.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (currentDrawDate != lastDrawDate)
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new NewYorkTake5();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);

                    
                    // save to db
                    db.NewYorkTake5.Add(entity);
                    db.SaveChanges();
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
