using SeleniumLottoDataApp;
using SeleniumLottoDataApp.Lib;
using SeleniumLottoDataApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeleniumLottoDataGen.Lib
{
    public class TexasCashFiveGen
    {
        public void ParseCsv()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var Path = parent + @"\Lotto.Data\Texas_cashfive.csv";
            int drawNumber = 1;
            using (StreamReader reader = new StreamReader(Path))
            {
                string line;
                int[] numbers = new int[5];
                while ((line = reader.ReadLine()) != null)
                {
                    //Define pattern
                    Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                    //Separating columns to array
                    string[] arr = CSVParser.Split(line);
                    string drawDate = arr[1] + "-" + arr[2] + "-" + arr[3];
                    for (int i = 0; i < 5; i++)
                    {
                        numbers[i] = int.Parse(arr[i+4]);
                    }
                    Array.Sort(numbers);
                    InsertDb(drawNumber++, drawDate, numbers);

                }
            }
        }

        public void InsertDb(int drawNumber, string drawDate, int[] numbers)
        {
            using (var db = new LottoDb())
            {
                var entity = new TexasCashFive();
                entity.DrawNumber = drawNumber;
                entity.DrawDate = drawDate;
                entity.Number1 = numbers[0];
                entity.Number2 = numbers[1];
                entity.Number3 = numbers[2];
                entity.Number4 = numbers[3];
                entity.Number5 = numbers[4];

                // save to db
                db.TexasCashFive.Add(entity);
                db.SaveChanges();

            }
        }
    }
}
