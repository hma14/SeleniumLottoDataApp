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
    public class LottoMaxDataGen : LottoGenBase
    {
        public LottoMaxDataGen()
        {

        }

        public override void ParseData()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var Path = parent + @"\Lotto.Data\LottoMax.csv";
            using (StreamReader reader = new StreamReader(Path))
            {
                string line = string.Empty;
                List<LottoMax> rows = new List<LottoMax>();
                List<LottoType> lottoTypes = new List<LottoType>();
                List<Number> numbers = new List<Number>();

                char[] separator = new[] { ',' };
                List<Number> prevDraw = null;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] arr = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                    if (int.Parse(arr[11]) == 0) continue;

                    DateTime dat = DateTime.Parse(arr[3].Trim('"'));
                    int pastDays = int.Parse(ConfigurationManager.AppSettings["HistoryDays"]);
                    if (dat < DateTime.Now.AddDays(-pastDays)) continue;

                    var entity = new LottoMax()
                    {
                        DrawNumber = int.Parse(arr[1]),
                        DrawDate = dat,
                        Number1 = int.Parse(arr[4]),
                        Number2 = int.Parse(arr[5]),
                        Number3 = int.Parse(arr[6]),
                        Number4 = int.Parse(arr[7]),
                        Number5 = int.Parse(arr[8]),
                        Number6 = int.Parse(arr[9]),
                        Number7 = int.Parse(arr[10]),
                        Bonus = int.Parse(arr[11])
                    };
                    rows.Add(entity);

                    // Create data list for LottoType table
                    var lottoType = new LottoType
                    {
                        Id = Guid.NewGuid(),
                        LottoName = (int)LottoNames.LottoMax,
                        DrawDate = entity.DrawDate,
                        DrawNumber = entity.DrawNumber,
                        NumberRange = (int)LottoNumberRange.LottoMax,
                    };
                    lottoTypes.Add(lottoType);

                    // Create data list for Number table
                    List<Number> lottoNumbers = new List<Number>();
                    for (int i = 1; i <= lottoType.NumberRange; i++)
                    {
                        var number = new Number
                        {
                            Id = Guid.NewGuid(),
                            Value = i,
                            LottoTypeId = lottoType.Id,
                        };

                        if (number.Value == entity.Number1 ||
                            number.Value == entity.Number2 ||
                            number.Value == entity.Number3 ||
                            number.Value == entity.Number4 ||
                            number.Value == entity.Number5 ||
                            number.Value == entity.Number6 ||
                            number.Value == entity.Number7 ||
                            number.Value == entity.Bonus)
                        {
                            number.Distance = 0;
                            number.IsHit = true;
                            number.TotalHits = prevDraw != null ? prevDraw[number.Value - 1].TotalHits + 1 : 1;
                            number.NumberofDrawsWhenHit = prevDraw != null ? prevDraw[number.Value - 1].Distance + 1 : 1;
                        }
                        else
                        {
                            number.IsHit = false;
                            number.Distance = prevDraw != null ? prevDraw[number.Value - 1].Distance + 1 : 1;
                            number.TotalHits = prevDraw != null ? prevDraw[number.Value - 1].TotalHits : 0;
                        }
                        numbers.Add(number);
                        lottoNumbers.Add(number);
                    }
                    prevDraw = lottoNumbers;
                }
                InsertDb(rows, lottoTypes, numbers);
            }
        }

        public void InsertDb(List<LottoMax> rows, List<LottoType> lottoTypes, List<Number> numbers)
        {
            using (var db = new LottoDb())
            {
                db.LottoMax.AddRange(rows);
                db.LottoTypes.AddRange(lottoTypes);
                db.Numbers.AddRange(numbers);
                db.SaveChanges();
            }
        }

    }
}

