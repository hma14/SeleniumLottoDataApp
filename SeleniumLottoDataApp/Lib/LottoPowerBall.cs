using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoPowerBall : LottoBase
    {
        public LottoPowerBall()
        {
            Driver.Url = "http://www.powerball.com/powerball/pb_numbers.asp";           
        }

        private string searchDrawDate()
        {
            var tbl = Driver.FindElement(By.XPath("//table[@align='center']"));
            var trs = tbl.FindElements(By.TagName("tr"));
            var tds = trs[1].FindElements(By.TagName("td"));
            var arr = tds[0].Text.Split('/');
            var da = arr[2] + "-" + arr[0] + "-" + arr[1];
            return da;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var tbl = Driver.FindElement(By.XPath("//table[@align='center']"));
            var trs = tbl.FindElements(By.TagName("tr"));
            var tds = trs[1].FindElements(By.TagName("td"));

            for (int i = 1; i < 8; i++)
            {
                if (i == 6) continue;
                numbers.Add(tds[i].Text);
            }
            
            return numbers;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.PowerBalls.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (currentDrawDate != lastDrawDate)
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new PowerBall();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);
                    
                    // save to db
                    db.PowerBalls.Add(entity);
                    db.SaveChanges();


                    // save to db for PowerBall
                    var pball = new PowerBall_PowerBall();
                    pball.DrawNumber = lastDrawNumber + 1;
                    pball.DrawDate = currentDrawDate;
                    pball.PowerBall = int.Parse(numbers[5]);

                    db.PowerBall_PowerBall.Add(pball);
                    db.SaveChanges();

                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
