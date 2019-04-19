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
            Driver.Url = "https://www.powerball.com/games/powerball";           
        }

        private string searchDrawDate()
        {

            var divs = Driver.FindElements(By.ClassName("field_draw_date"));
            var dat = divs.First().Text.Split();                       
            var da = dat[5] + "-" + DicDateShort[dat[3]] + "-" + dat[4].Trim(',');
            return da;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();           

            var balls = Driver.FindElements(By.ClassName("numbers-ball")).Where(x => !string.IsNullOrEmpty(x.Text));
            foreach (var ball in balls.Take(6))
            {
                
                numbers.Add(ball.Text);
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
