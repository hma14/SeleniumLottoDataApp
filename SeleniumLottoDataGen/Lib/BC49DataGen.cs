using SeleniumLottoDataApp;
using SeleniumLottoDataApp.BusinessModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using static SeleniumLottoDataApp.BusinessModels.Constants;

namespace SeleniumLottoDataGen.Lib
{
    public class BC49DataGen
    {
        public BC49DataGen()
        {

        }

        public void ParseData()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var Path = parent + @"\Lotto.Data\BC49.csv";
            using (StreamReader reader = new StreamReader(Path))
            {
                string line = string.Empty;
                List<BC49> rows = new List<BC49>();
                List<List<LottoNumber>> rows2 = new List<List<LottoNumber>>();

                char[] separator = new[] { ',' };
                while ((line = reader.ReadLine()) != null)
                {
                    string[] arr = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    DateTime dat = DateTime.Parse(arr[2].Trim('"'));
                    int pastDays = int.Parse(ConfigurationManager.AppSettings["HistoryDays"]);
                    if (dat < DateTime.Now.AddDays(-pastDays)) continue;

                    var entity = new BC49()
                    {
                        DrawNumber = int.Parse(arr[1]),
                        DrawDate = dat,
                        Number1 = int.Parse(arr[3]),
                        Number2 = int.Parse(arr[4]),
                        Number3 = int.Parse(arr[5]),
                        Number4 = int.Parse(arr[6]),
                        Number5 = int.Parse(arr[7]),
                        Number6 = int.Parse(arr[8]),
                        Bonus = int.Parse(arr[9])
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

        public void InsertDb(List<BC49> rows)
        {
            using (var db = new LottoDb())
            {
                if (db.BC49.ToList().LastOrDefault()?.DrawNumber >= rows.FirstOrDefault().DrawNumber) return;
                db.BC49.AddRange(rows);
                db.SaveChanges();
            }
        }

        public void InsertLottoNumberDb(List<List<LottoNumber>> rows)
        {
            using (var db = new LottoDb())
            {
                foreach (var rs in rows)
                {
                    db.LottoNumber.AddRange(rs);                   
                }
                db.SaveChanges();
            }
        }

        private List<LottoNumber> GetLottoNumberRecord(BC49 lotto)
        {
            using (var db = new LottoDb())
            {
                List<LottoNumber> rows = new List<LottoNumber>();
                for (int i = 1; i <= (int)LottoNumberRange.BC49; i++)
                {
                    LottoNumber entity = new LottoNumber
                    {
                        LottoName = LottoNames.BC49,
                        DrawNumber = lotto.DrawNumber,
                        DrawDate = lotto.DrawDate,
                        Number = i,
                        Distance = 0,
                        IsHit = (lotto.Number1 == i ||
                                    lotto.Number2 == i ||
                                    lotto.Number3 == i ||
                                    lotto.Number4 == i ||
                                    lotto.Number5 == i ||
                                    lotto.Number6 == i ||
                                    lotto.Bonus == i) ? true : false,
                        NumberRange = LottoNumberRange.BC49,
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
