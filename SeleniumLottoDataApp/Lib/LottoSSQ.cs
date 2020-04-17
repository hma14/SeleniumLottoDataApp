using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoSSQ : LottoBase
    {
        public LottoSSQ()
        {
            Driver.Url = "http://www.cwl.gov.cn/kjxx/ssq/";           
        }

        private string searchDrawDate()
        {
            List<string> numbers = new List<string>();
            var dat = Driver.FindElement(By.XPath("//div[@class='kjrq']/span"));
            return dat?.Text;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var lis = Driver.FindElements(By.XPath("//div[@class='kjhm']/ul/li"));

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
                var list = db.SSQs.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                if (dates != null && dates.Any())
                {
                    var lastDrawDate = dates.Last().Item2;
                    var currentDrawDate = searchDrawDate();

                    if (currentDrawDate != lastDrawDate)
                    {
                        var lastDrawNumber = dates.Last().Item1;
                        var numbers = searchDrawNumbers();

                        var entity = new SSQ();
                        entity.DrawNumber = lastDrawNumber + 1;
                        entity.DrawDate = currentDrawDate;
                        entity.Number1 = int.Parse(numbers[0]);
                        entity.Number2 = int.Parse(numbers[1]);
                        entity.Number3 = int.Parse(numbers[2]);
                        entity.Number4 = int.Parse(numbers[3]);
                        entity.Number5 = int.Parse(numbers[4]);
                        entity.Number6 = int.Parse(numbers[5]);

                        // save to db
                        db.SSQs.Add(entity);
                        db.SaveChanges();


                        // save to db for Euros
                        var blue = new SSQ_Blue();
                        blue.DrawNumber = lastDrawNumber + 1;
                        blue.DrawDate = currentDrawDate;
                        blue.Blue = int.Parse(numbers[6]);

                        db.SSQ_Blue.Add(blue);
                        db.SaveChanges();

                    }
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
