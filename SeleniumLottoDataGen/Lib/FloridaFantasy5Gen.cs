using SeleniumLottoDataApp;
using SeleniumLottoDataApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SeleniumLottoDataGen.Lib
{
    public class FloridaFantasy5Gen
    {
        public FloridaFantasy5Gen()
        {

        }

        //public void ParseCsv()
        //{
        //    var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        //    var Path = parent + @"\Lotto.Data\nytake5.csv";
        //    int drawNumber = 1;
        //    using (StreamReader reader = new StreamReader(Path))
        //    {
        //        string line = reader.ReadLine(); // first line is header, don't care
        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            //Define pattern
        //            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        //            //Separating columns to array
        //            string[] arr = CSVParser.Split(line);
        //            string drawDate = arr[0];
        //            string[] numbers = arr[1].Split(' ');
        //            InsertDb(drawNumber++, drawDate, numbers);

        //        }
        //    }
        //}

        public void ParseData()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var Path = parent + @"\Lotto.Data\FloridaFantasy5_Data.txt";
            using (StreamReader reader = new StreamReader(Path))
            {
                string line = string.Empty;
                List<FloridaFantasy5> cols1 = new List<FloridaFantasy5>();
                List<FloridaFantasy5> cols2 = new List<FloridaFantasy5>();
                List<List<string>> rows = new List<List<string>>();
                char[] separator = new[] { '\t' };
                int drawNumber1 = 2840;
                int drawNumber2 = 2788;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] arr = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                    var entity1 = new FloridaFantasy5()
                    {
                        DrawNumber = drawNumber1,
                        DrawDate = arr[0],
                        Number1 = int.Parse(arr[1]),
                        Number2 = int.Parse(arr[3]),
                        Number3 = int.Parse(arr[5]),
                        Number4 = int.Parse(arr[7]),
                        Number5 = int.Parse(arr[9])
                    };
                    drawNumber1--;

                    var entity2 = new FloridaFantasy5()
                    {
                        DrawNumber = drawNumber2,
                        DrawDate = arr[10],
                        Number1 = int.Parse(arr[11]),
                        Number2 = int.Parse(arr[13]),
                        Number3 = int.Parse(arr[15]),
                        Number4 = int.Parse(arr[17]),
                        Number5 = int.Parse(arr[19])
                    };
                    drawNumber2--;

                    string[] arr2 = new string[] { arr[10], arr[11], arr[13], arr[15], arr[17], arr[19] };
                    cols1.Add(entity1);
                    cols2.Add(entity2);
                }
                cols1.AddRange(cols2);
                cols1.Reverse();

                InsertDb(cols1);

            }           
        }

        public void InsertDb(List<FloridaFantasy5> rows)
        {
            using (var db = new LottoDb())
            {
                db.FloridaFantasy5.AddRange(rows);
                db.SaveChanges();
            }
        }       
    }
}
