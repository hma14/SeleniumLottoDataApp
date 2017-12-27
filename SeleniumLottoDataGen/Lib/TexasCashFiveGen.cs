using SeleniumLottoDataApp;
using SeleniumLottoDataApp.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace SeleniumLottoDataGen.Lib
{
    public class TexasCashFiveGen
    {
        public TexasCashFiveGen()
        {
                
        }

        public void ParseCsv()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var Path = parent + @"\Lotto.Data\Texas_cashfive.csv";
            int drawNumber = 6000; // started: DrawNumber = 6000
            List<TexasCashFive> rows = new List<TexasCashFive>();

            using (StreamReader reader = new StreamReader(Path))
            {
                string line;
                int[] numbers = new int[5];
                int counter = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    counter++;
                    if (counter < 6000)
                    {                       
                        continue;
                    }

                    //Define pattern
                    //Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                    //string[] arr = CSVParser.Split(line);

                    string[] arr = line.Split(',');
                    string drawDate = arr[1] + "-" + arr[2] + "-" + arr[3];
                    for (int i = 0; i < 5; i++)
                    {
                        numbers[i] = int.Parse(arr[i + 4]);
                    }
                    Array.Sort(numbers);

                    var entity = new TexasCashFive()
                    {
                        DrawNumber = drawNumber,
                        DrawDate = drawDate,
                        Number1 = numbers[0],
                        Number2 = numbers[1],
                        Number3 = numbers[2],
                        Number4 = numbers[3],
                        Number5 = numbers[4],
                    };
                    drawNumber++;
                    rows.Add(entity);                  
                }
                InsertDb(rows);
            }
        }

        public void InsertDb(List<TexasCashFive> rows)
        {
            using (var db = new LottoDb())
            {
                db.TexasCashFive.AddRange(rows);
                db.SaveChanges();
            }
        }
    }
}
