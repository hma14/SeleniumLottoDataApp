using SeleniumLottoDataApp;
using SeleniumLottoDataApp.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace SeleniumLottoDataGen.Lib
{
    public class DailyGrandGen
    {
        public DailyGrandGen()
        {
                
        }

        public void ParseCsv()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var Path = parent + @"\Lotto.Data\DailyGrand.csv";
            List<DailyGrand> rows = new List<DailyGrand>();
            int drawNumber = 1;
            using (StreamReader reader = new StreamReader(Path))
            {
                string line;
                reader.ReadLine(); // skip first line

                while ((line = reader.ReadLine()) != null)
                {
                    string[] arr = line.Split(',');
                    var entity = new DailyGrand()
                    {
                        DrawNumber = drawNumber++,
                        DrawDate = arr[2].Replace("\"", string.Empty),
                        Number1 = int.Parse(arr[5]),
                        Number2 = int.Parse(arr[6]),
                        Number3 = int.Parse(arr[7]),
                        Number4 = int.Parse(arr[8]),
                        Number5 = int.Parse(arr[9]),
                        Grand = int.Parse(arr[10]),
                    };
                    rows.Add(entity);                  
                }
                InsertDb(rows);
            }
        }

        public void InsertDb(List<DailyGrand> rows)
        {
            using (var db = new LottoDb())
            {
                db.DailyGrand.AddRange(rows);
                db.SaveChanges();
            }
        }
    }
}
