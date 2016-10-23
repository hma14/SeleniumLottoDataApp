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
            Driver.Url = "http://www.zhcw.com/ssq/";           
        }

        private string searchDrawDate()
        {
            List<string> numbers = new List<string>();
            var dat = Driver.FindElement(By.Id("kj_date"));
            return dat.Text;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var nums = Driver.FindElement(By.Id("kj_num"));
            var arr = nums.Text.Split('\n');

           foreach(var a in arr)
            {
                numbers.Add(a.TrimEnd('\r'));
            }

            return numbers;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.SSQs.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (currentDrawDate != lastDrawDate)
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
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
            Driver.Close();
            Driver.Quit();
        }
    }
}
