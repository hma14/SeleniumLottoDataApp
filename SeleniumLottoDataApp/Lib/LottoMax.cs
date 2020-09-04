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
            Driver.Url = "https://www.playnow.com/lottery/lotto-max-winning-numbers/";           
        }

        private string searchDrawDate()
        {
            var dat = Driver.FindElements(By.ClassName("product-date-picker__draw-date"));
            var arr = dat[0].Text.Split();
            var da = arr[3] + '-' + DicDateShort3[arr[1].ToUpper()] + "-" + arr[2].Trim(',');
            return da;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> NList = new List<string>();
            var list = Driver.FindElements(By.ClassName("product-winning-numbers__number_lmax"));
            foreach (var lst in list)
            {
                NList.Add(lst.Text);
            }
            var list2 = Driver.FindElements(By.ClassName("product-winning-numbers__bonus-number_lmax"));
            if (list2 == null || !list2.Any())
                return null;
            NList.Add(list2[0].Text);
            return NList;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.LottoMaxes.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (DateTime.Parse(currentDrawDate) > DateTime.Parse(lastDrawDate))
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();
                    if (numbers != null)
                    {
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
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
