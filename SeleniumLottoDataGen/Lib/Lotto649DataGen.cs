using SeleniumLottoDataApp;
using SeleniumLottoDataApp.BusinessModels;
using SeleniumLottoDataApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using static SeleniumLottoDataApp.BusinessModels.Constants;

namespace SeleniumLottoDataGen.Lib
{
    public class Lotto649DataGen : LottoGenBase
    {
        public Lotto649DataGen()
        {

        }

        public override void ParseData()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var Path = parent + @"\Lotto.Data\649.csv";
            using (StreamReader reader = new StreamReader(Path))
            {
                string line = string.Empty;
                List<Lotto649> rows = new List<Lotto649>();
                List<List<LottoNumber>> rows2 = new List<List<LottoNumber>>();

                char[] separator = new[] { ',' };
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    string[] arr = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    string dat = arr[3].Trim('"');
                    int pastDays = int.Parse(ConfigurationManager.AppSettings["HistoryDays"]);
                    if (DateTime.Parse(dat) < DateTime.Now.AddDays(-pastDays)) continue;

                    var entity = new Lotto649()
                    {
                        DrawNumber = int.Parse(arr[1]),
                        DrawDate = dat,
                        Number1 = int.Parse(arr[4]),
                        Number2 = int.Parse(arr[5]),
                        Number3 = int.Parse(arr[6]),
                        Number4 = int.Parse(arr[7]),
                        Number5 = int.Parse(arr[8]),
                        Number6 = int.Parse(arr[9]),
                        Bonus = int.Parse(arr[10])
                    };
                    rows.Add(entity);
                    var lottoNumbers = GetLottoNumberRecord(entity);
                    rows2.Add(lottoNumbers);
                }

                InsertDb(rows);

                // Add distances to each Numbers

                List<LottoNumber> prevRow = rows2.First();
                foreach (var row in rows2)
                {                 
                    foreach(var n in row)
                    {
                        if (n.IsHit == true)
                        {                            
                            n.NumberofDrawsWhenHit = prevRow[n.Number - 1].Distance + 1;
                            n.Distance = 0;
                            n.TotalHits = prevRow[n.Number - 1].TotalHits + 1;
                        }
                        else
                        {
                            n.Distance = prevRow[n.Number - 1].Distance + 1;
                            n.TotalHits = prevRow[n.Number - 1].TotalHits;
                        }
                    }
                    prevRow = row;
                }
                InsertLottoNumberDb(rows2);

            }
        }

        public void InsertDb(List<Lotto649> rows)
        {
            
            using (var db = new LottoDb())
            {
                if (db.Lotto649.ToList().LastOrDefault()?.DrawNumber >= rows.FirstOrDefault().DrawNumber) return;
                db.Lotto649.AddRange(rows);
                db.SaveChanges();
            }
        }

        private List<LottoNumber> GetLottoNumberRecord(Lotto649 lotto)
        {
            using (var db = new LottoDb())
            {
                List<LottoNumber> rows = new List<LottoNumber>();
                for (int i = 1; i <= (int)LottoNumberRange.Lotto649; i++)
                {
                    LottoNumber entity = new LottoNumber
                    {
                        LottoName = LottoNames.Lotto649,
                        DrawNumber = lotto.DrawNumber,
                        DrawDate = DateTime.Parse(lotto.DrawDate),
                        Number = i,
                        Distance = 0,
                        IsHit = (lotto.Number1 == i ||
                                    lotto.Number2 == i ||
                                    lotto.Number3 == i ||
                                    lotto.Number4 == i ||
                                    lotto.Number5 == i ||
                                    lotto.Number6 == i ||
                                    lotto.Bonus == i) ? true : false,
                        NumberRange = LottoNumberRange.Lotto649,
                        NumberofDrawsWhenHit = 0,
                        IsBonusNumber = lotto.Bonus == i ? true : false,
                        TotalHits = 0,
                    };
                    rows.Add(entity);
                }
                return rows;
            }
        }
    }
}
