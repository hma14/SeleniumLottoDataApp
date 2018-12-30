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

        // data download URL:  https://catalog.data.gov/dataset/lottery-take-5-winning-numbers

        public void ParseCsv()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var Path = parent + @"\Lotto.Data\Lottery_Take_5_Winning_Numbers.csv";
            int drawNumber = 7813;
            using (StreamReader reader = new StreamReader(Path))
            {
                string line = reader.ReadLine(); // first line is header, don't care
                for (int i = 1; i <= drawNumber; i++)
                {
                    line = reader.ReadLine();
                }
                List<NyTake5> list = new List<NyTake5>();
                Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                while ((line = reader.ReadLine()) != null)
                {                    
                    string[] arr = CSVParser.Split(line);
                    NyTake5 model = new NyTake5
                    {
                        DrawNumber = ++drawNumber,
                        DrawDate = arr[0],
                        Numbers = arr[1].Split(' '),
                    };
                    list.Add(model);
                }
                InsertDb(list);
            }
        }

        public void InsertDb(List<NyTake5> model)
        {
            using (var db = new LottoDb())
            {
                List<NewYorkTake5> entities = new List<NewYorkTake5>();
                foreach(var mod in model)
                {
                    var entity = new NewYorkTake5();
                    entity.DrawNumber = mod.DrawNumber;
                    entity.DrawDate = mod.DrawDate;
                    entity.Number1 = int.Parse(mod.Numbers[0]);
                    entity.Number2 = int.Parse(mod.Numbers[1]);
                    entity.Number3 = int.Parse(mod.Numbers[2]);
                    entity.Number4 = int.Parse(mod.Numbers[3]);
                    entity.Number5 = int.Parse(mod.Numbers[4]);

                    entities.Add(entity);
                }
                
                // save to db
                db.NewYorkTake5.AddRange(entities);
                db.SaveChanges();

            }
        }
    }

    public class NyTake5
    {
        public int DrawNumber { get; set; }
        public string DrawDate { get; set; }
        public string[] Numbers { get; set; }
    }
}
