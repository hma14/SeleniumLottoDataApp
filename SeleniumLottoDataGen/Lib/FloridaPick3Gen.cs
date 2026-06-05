using SeleniumLottoDataApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SeleniumLottoDataGen.Lib
{
    public class FloridaPick3Gen
    {
        public FloridaPick3Gen()
        {

        }


        public void ParseData()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var Path = parent + @"\Lotto.Data\pick3_data.txt";
            using (StreamReader reader = new StreamReader(Path))
            {
                string line = string.Empty;
                List<FloridaPick3> cols = new List<FloridaPick3>();
                List<List<string>> rows = new List<List<string>>();
                char[] separator = new[] { ' ' };
                int drawNumber1 = 1000;
                while ((line = reader.ReadLine()) != null && drawNumber1 > 0)
                {                
                    string[] arr = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length == 15 && arr[13] == "FB")
                    {
                        var entity1 = new FloridaPick3()
                        {
                            DrawNumber = drawNumber1,
                            DrawDate = arr[0],
                            Number1 = int.Parse(arr[8]),
                            Number2 = int.Parse(arr[10]),
                            Number3 = int.Parse(arr[12]),
                            Fb = int.Parse(arr[14]),
                        };
                        drawNumber1--;
                        cols.Add(entity1);
                    }
                }
                //rows.AddRange(cols);
                cols.Reverse();

                InsertDb(cols);

            }           
        }

        public void InsertDb(List<FloridaPick3> rows)
        {
            using (var db = new LottoDb())
            {
                db.FloridaPick3.AddRange(rows);
                db.SaveChanges();
            }
        }       
    }
}
