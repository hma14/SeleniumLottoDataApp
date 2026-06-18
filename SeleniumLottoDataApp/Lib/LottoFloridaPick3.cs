using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SeleniumLottoDataApp.Lib
{
    public class LottoFloridaPick3 : LottoBase
    {
        public LottoFloridaPick3()
        {
            Driver.Url = "https://floridalottery.com/games/draw-games/pick-3";
        }

        private string searchDrawDate()
        {
            var ps = Driver.FindElements(By.ClassName("draw-date--pick3"));
            var txt = ps[1].Text;
            var txts = txt.Split(','); // Fix: Changed the argument from ',' (string) to ',' (char)
            var monthDay = txts[1].Split(' '); // Fix: Added a space character (' ') as the argument for Split
            var year = txts[2];
            var month = monthDay[1];
            var day = monthDay[2];
            var da = year + '-' + DicDateShort2[month] + "-" + day;
            return da;
        }
        

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            //var spans = Driver.FindElements(By.XPath("//div[@class='gamePageBalls']/p/span"));

            var divs = Driver.FindElements(By.ClassName("game-numbers--pick3"));
            var div = divs[1];
            var lis = div.FindElements(By.ClassName("game-numbers__number"));
            foreach (var li in lis)
            {
                if (Char.IsDigit(li.Text[0]) == true)
                numbers.Add(li.Text);
            }
            var l = div.FindElement(By.ClassName("game-numbers__bonus"));
            var sp = l.FindElement(By.ClassName("game-numbers__bonus-text"));
            if (Char.IsDigit(sp.Text[0]) == true)
            {
                numbers.Add(sp.Text);
            }
                
            return numbers;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.FloridaPick3.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (DateTime.Parse(currentDrawDate) > DateTime.Parse(lastDrawDate))
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new FloridaPick3();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Fb = int.Parse(numbers[3]);

                    // save to db
                    db.FloridaPick3.Add(entity);
                    db.SaveChanges();
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
