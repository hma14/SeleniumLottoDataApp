using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoNYLotto : LottoBase
    {
        public LottoNYLotto()
        {
            Driver.Url = "http://nylottery.ny.gov/wps/portal/!ut/p/c5/04_SB8K8xLLM9MSSzPy8xBz9CP0os_jggBC3kDBPE0MLC0dnA09vT0fLQDNvA0dfU30_j_zcVP1I_ShzXKoCgw30I3NS0xOTK_ULst0cAYmfjdU!/dl3/d3/L0lJSklna21BL0lKakFBRXlBQkVSQ0pBISEvNEZHZ3NvMFZ2emE5SUFnIS83X1NQVEZUVkk0MTg4QUMwSUtJQTlRNkswUVMwL2tvS2ExNjY5MDAwMDE!/?PC_7_SPTFTVI4188AC0IKIA9Q6K0QS0_WCM_CONTEXT=/wps/wcm/connect/NYSL+Content+Library/NYSL+Internet+Site/Home/Jackpot+Games/LOTTO";

        }

        private string searchDrawDate()
        {
            var dat = Driver.FindElements(By.ClassName("WinningNumbersText"));
            var txt = dat[0].Text;
            txt = txt.Replace(",", "");
            var arr = txt.Split();
            var da = arr[2] + "-" + DicDateShort[arr[0]] + "-" + arr[1];

            return da;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> numbers = new List<string>();
            var divs = Driver.FindElements(By.ClassName("WinningNumbersResultsLotto"));
            foreach (var div in divs)
            {                
                numbers.Add(div.Text);
            }
            var bonus = Driver.FindElement(By.ClassName("WinningNumbersMegaBallLotto"));
            numbers.Add(bonus.Text);

            return numbers;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.NYLottoes.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (currentDrawDate != lastDrawDate)
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new NYLotto();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);
                    entity.Number6 = int.Parse(numbers[5]);
                    entity.Bonus = int.Parse(numbers[6]);

                    // save to db
                    db.NYLottoes.Add(entity);
                    db.SaveChanges();
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
