﻿using SeleniumLottoDataApp;
using SeleniumLottoDataApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SeleniumLottoDataGen.Lib
{
    public class FloridaFantasy5Gen
    {
        public FloridaFantasy5Gen()
        {

        }

#if true
        public void ParseData()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var filePath = parent + @"\Lotto.Data\fantasy-5-data.txt";

            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            // A list to store the parsed results
            List<string> parsedResults = new List<string>();

            // We will parse the data two lines at a time (one for the date, one for the numbers)

            List<FloridaFantasy5> rows = new List<FloridaFantasy5>();
            int drawNumber = 271;
            string day = string.Empty;
            string month = string.Empty;
            string year = string.Empty;
            for (int i = 0; i < lines.Length; i++)
            {
                
                var numbers = new string[5];
                if (i % 3 == 0) continue;
                if (i % 3 == 1)
                {
                    var date = lines[i].Split(',');
                    day = date[0].Split()[1];
                    month = (string) Constants.DicDate[date[0].Split()[0]];
                    year = date[1].Trim();
                    continue;
                }
                if (i % 3 == 2)
                {
                    numbers = lines[i].Split().ToArray();
                }
               
                FloridaFantasy5 entity = new FloridaFantasy5
                {
                    DrawNumber = drawNumber--,
                    DrawDate = $"{year}-{month}-{day}",
                    Number1 = int.Parse(numbers[0]),
                    Number2 = int.Parse(numbers[1]),
                    Number3 = int.Parse(numbers[2]),
                    Number4 = int.Parse(numbers[3]),
                    Number5 = int.Parse(numbers[4])
                };

                rows.Add(entity);
            }

            InsertDb(rows);
        }

#else

        public void ParseData()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var Path = parent + @"\Lotto.Data\fantasy-5-data.txt";
            using (StreamReader reader = new StreamReader(Path))
            {
                string line = string.Empty;
                List<FloridaFantasy5> cols1 = new List<FloridaFantasy5>();
                List<FloridaFantasy5> cols2 = new List<FloridaFantasy5>();
                List<List<string>> rows = new List<List<string>>();
                char[] separator = new[] { '\t' };
                int drawNumber1 = 3654;
                //int drawNumber2 = 3639;
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

                    //var entity2 = new FloridaFantasy5()
                    //{
                    //    DrawNumber = drawNumber2,
                    //    DrawDate = arr[10],
                    //    Number1 = int.Parse(arr[11]),
                    //    Number2 = int.Parse(arr[13]),
                    //    Number3 = int.Parse(arr[15]),
                    //    Number4 = int.Parse(arr[17]),
                    //    Number5 = int.Parse(arr[19])
                    //};
                    //drawNumber2--;

                    cols1.Add(entity1);
                    //cols2.Add(entity2);
                }
                cols1.AddRange(cols2);
                cols1.Reverse();

                InsertDb(cols1);

            }           
        }
#endif

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
