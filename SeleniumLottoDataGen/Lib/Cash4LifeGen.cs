using SeleniumLottoDataApp;
using System;
using System.Collections.Generic;
using System.IO;

namespace SeleniumLottoDataGen.Lib
{
    public class Cash4LifeGen
    {
        public Cash4LifeGen()
        {

        }


        public void ParseData()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var Path = parent + @"\Lotto.Data\Lottery_Cash_4_Life_Winning_Numbers__Beginning_2014.txt";
            using (StreamReader reader = new StreamReader(Path))
            {
                string line = string.Empty;
                List<Cash4Life> records = new List<Cash4Life>();
                List<List<string>> rows = new List<List<string>>();
                char[] separator = new[] { ' ', '\t' };
                int drawNumber = 0;
                //int drawNumber2 = 3639;
                while ((line = reader.ReadLine()) != null)
                {
                    if (drawNumber == 0)
                    {
                        ++drawNumber;
                        continue;
                    }
                    string[] arr = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                    var entity1 = new Cash4Life()
                    {
                        DrawNumber = drawNumber,
                        DrawDate = arr[0],
                        Number1 = int.Parse(arr[1]),
                        Number2 = int.Parse(arr[2]),
                        Number3 = int.Parse(arr[3]),
                        Number4 = int.Parse(arr[4]),
                        Number5 = int.Parse(arr[5]),
                        CashBall = int.Parse(arr[6])
                    };
                    drawNumber++;

                    records.Add(entity1);
                }
                InsertDb(records);

            }           
        }

        public void InsertDb(List<Cash4Life> rows)
        {
            using (var db = new LottoDb())
            {
                db.Cash4Life.AddRange(rows);
                db.SaveChanges();
            }
        }       
    }
}
