using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoFloridaFantasy5 : LottoBase
    {
        public string Numbers { get; set; }
        public LottoFloridaFantasy5()
        {
            Driver.Url = "http://flalottery.com/fantasy5";
           
        }

        private string searchDrawDate()
        {
            //Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            var ps = Driver.FindElements(By.ClassName("draw-date--fantasy5"));
            
            string da = string.Empty;
            try
            {
                // Evening 
                var txt = ps[1].Text;
                txt = txt.Replace(",", "");
                var arr = txt.Split();
                da = arr[3] + '-' + DicDateShort2[arr[1]] + "-" + arr[2];
            }
            catch
            {
                //var txt = ps[1].Text;
                //txt = txt.Replace(",", "");
                //var arr = txt.Split();
                //da = arr[3] + '-' + DicDate[arr[1]] + "-" + arr[2];
                //Numbers = ps[2].Text;
                throw new Exception("Couldn't find draw date for Florida Fantasy 5");
            }
            return da;
        }

        private List<string> searchDrawNumbers()
        {
            // var numbers = Numbers.Replace("\r", "-").Split('-').Take(5).ToList();
            List<string> numbers = new List<string>();

            var lis = Driver.FindElements(By.ClassName("game-numbers__number"));
            var arr = lis.Skip(5).Take(5).ToList();
            foreach (var item in arr)
            {
                var num = item.FindElement(By.TagName("span")).Text;
                numbers.Add(num);
            }

            return numbers;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.FloridaFantasy5.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (DateTime.Parse(currentDrawDate) > DateTime.Parse(lastDrawDate))
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new FloridaFantasy5();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);

                    
                    // save to db
                    db.FloridaFantasy5.Add(entity);
                    db.SaveChanges();
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
