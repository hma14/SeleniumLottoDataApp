using SeleniumLottoDataApp;
using System;
using System.Collections.Generic;
using System.IO;

namespace SeleniumLottoDataGen.Lib
{
    public class MegaMillionGen : LottoGenBase
    {
        public MegaMillionGen()
        {

        }


        public override void ParseData()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var Path = parent + @"\Lotto.Data\Mega_Millions_Winning_Numbers.txt";
            using (StreamReader reader = new StreamReader(Path))
            {
                string line = string.Empty;
                List<MegaMillion> records = new List<MegaMillion>() ; 
                List<MegaMillions_MegaBall> records2 = new List<MegaMillions_MegaBall>();
                List<List<string>> rows = new List<List<string>>();
                char[] separator = new[] { ' ', '\t' };
                int drawNumber = 1;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line == string.Empty || line == "\t") break;
                   
                    string[] arr = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                    var entity1 = new MegaMillion()
                    {
                        DrawNumber = drawNumber,
                        DrawDate = arr[0],
                        Number1 = int.Parse(arr[1]),
                        Number2 = int.Parse(arr[2]),
                        Number3 = int.Parse(arr[3]),
                        Number4 = int.Parse(arr[4]),
                        Number5 = int.Parse(arr[5]),
                    };

                    var entity2 = new MegaMillions_MegaBall()
                    {
                        DrawNumber = drawNumber,
                        DrawDate = arr[0],
                        MegaBall = int.Parse(arr[6])
                    };
                    drawNumber++;

                    records.Add(entity1);
                    records2.Add(entity2);
                }
                
                InsertDb(records);
                InsertDb_MegaMillion(records2);

            }
        }

        public void InsertDb(List<MegaMillion> rows)
        {
            using (var db = new LottoDb())
            {
                db.MegaMillions.AddRange(rows);
                db.SaveChanges();
            }
        }
        public void InsertDb_MegaMillion(List<MegaMillions_MegaBall> rows)
        {
            using (var db = new LottoDb())
            {
                db.MegaMillions_MegaBall.AddRange(rows);
                db.SaveChanges();
            }
        }
    }
}
