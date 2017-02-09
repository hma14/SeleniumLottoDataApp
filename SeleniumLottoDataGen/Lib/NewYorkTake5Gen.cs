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
    public class NewYorkTake5Gen
    {
        public NewYorkTake5Gen()
        {
            
        }

        public void ParseCsv()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var Path = parent + @"\Lotto.Data\nytake5.csv";
            int drawNumber = 1;
            using (StreamReader reader = new StreamReader(Path))
            {
                string line = reader.ReadLine(); // first line is header, don't care
                while ((line = reader.ReadLine()) != null)
                {
                    //Define pattern
                    Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                    //Separating columns to array
                    string[] arr = CSVParser.Split(line);
                    string drawDate = arr[0];
                    string[] numbers = arr[1].Split(' ');
                    InsertDb(drawNumber++, drawDate, numbers);
                    
                }
            }
        }

        public void InsertDb(int drawNumber, string drawDate, string[] numbers)
        {
            using (var db = new LottoDb())
            {
                var entity = new NewYorkTake5();
                entity.DrawNumber = drawNumber;
                entity.DrawDate = drawDate;
                entity.Number1 = int.Parse(numbers[0]);
                entity.Number2 = int.Parse(numbers[1]);
                entity.Number3 = int.Parse(numbers[2]);
                entity.Number4 = int.Parse(numbers[3]);
                entity.Number5 = int.Parse(numbers[4]);

                // save to db
                db.NewYorkTake5.Add(entity);
                db.SaveChanges();

            }       
        }
    }
}
