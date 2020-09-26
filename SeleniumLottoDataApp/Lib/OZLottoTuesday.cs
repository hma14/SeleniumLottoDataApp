using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class OZLottoTuesday : LottoBase
    {
        public OZLottoTuesday()
        {
            Driver.Url = "https://www.ozlotteries.com/oz-lotto/results";           
        }

        private string searchDrawDate()
        {
            var dat = Driver.FindElements(By.XPath("//div[@class='css-1b5vidf-Results e17om90p4']/div"));
            var arr = dat[0].Text.Split();
            var da = arr[2].Split('D')[0] + '-' + DicDate[arr[1]] + "-" + arr[0];
            return da;
        }

        private List<int> searchDrawNumbers()
        {
            List<int> NList = new List<int>();
            var divs = Driver.FindElements(By.ClassName("eik1jin0")).Take(9);
            int i = 0;
            foreach (var div in divs)
            {
                NList.Add(int.Parse(div.Text));
                i++;
                if (i == 7)
                    NList.Sort();
            }
            return NList;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.OZLottoTues.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (DateTime.Parse(currentDrawDate) > DateTime.Parse(lastDrawDate))
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();
                    if (numbers != null)
                    {
                        var entity = new OZLottoTue();
                        entity.DrawNumber = lastDrawNumber + 1;
                        entity.DrawDate = currentDrawDate;
                        entity.Number1 = numbers[0];
                        entity.Number2 = numbers[1];
                        entity.Number3 = numbers[2];
                        entity.Number4 = numbers[3];
                        entity.Number5 = numbers[4];
                        entity.Number6 = numbers[5];
                        entity.Number7 = numbers[6];
                        entity.Supp1 = numbers[7];
                        entity.Supp2 = numbers[8];


                        // save to db
                        db.OZLottoTues.Add(entity);
                        db.SaveChanges();
                    }
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
