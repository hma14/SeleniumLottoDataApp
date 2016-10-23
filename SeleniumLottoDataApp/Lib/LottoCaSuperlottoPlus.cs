using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoCaSuperlottoPlus : LottoBase
    {
        public LottoCaSuperlottoPlus()
        {
            Driver.Url = "http://www.calottery.com/play/draw-games/superlotto-plus";           
        }

        private string searchDrawDate()
        {
            var dat = Driver.FindElement(By.ClassName("date"));
            var arr = dat.Text.Split(',');
            var mo = arr[1].Trim().Split()[0];
            var da = arr[1].Trim().Split()[1];
            var yr = arr[2].Trim().Split()[0];
            var date = yr + "-" + DicDate[mo] + "-" + da;
            return date;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> NList = new List<string>();
            var list = Driver.FindElements(By.XPath("//ul[@class='winning_number_sm']/li"));
            foreach (var lst in list)
            {
                NList.Add(lst.Text);
            }
            return NList;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.CaSuperlottoPlus.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (currentDrawDate != lastDrawDate)
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new CaSuperlottoPlu();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);
                    
                    // save to db
                    db.CaSuperlottoPlus.Add(entity);
                    db.SaveChanges();

                    var mega = new CaSuperlottoPlus_Mega();
                    mega.DrawNumber = lastDrawNumber + 1;
                    mega.DrawDate = currentDrawDate;
                    mega.Mega = int.Parse(numbers[5]);

                    // save to db for Mega
                    db.CaSuperlottoPlus_Mega.Add(mega);
                    db.SaveChanges();
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
