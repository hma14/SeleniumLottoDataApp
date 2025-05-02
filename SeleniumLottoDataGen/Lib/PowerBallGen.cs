using SeleniumLottoDataApp;
using System;
using System.Collections.Generic;
using System.IO;

namespace SeleniumLottoDataGen.Lib
{
    public class PowerBallGen : LottoGenBase
    {
        public PowerBallGen()
        {

        }


        public override void ParseData()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var Path = parent + @"\Lotto.Data\Lottery_Powerball_Winning_Numbers.txt";
            using (StreamReader reader = new StreamReader(Path))
            {
                string line = string.Empty;
                List<PowerBall> records = new List<PowerBall>() ; 
                List<PowerBall_PowerBall> records2 = new List<PowerBall_PowerBall>();
                List<List<string>> rows = new List<List<string>>();
                char[] separator = new[] { ' ', '\t' };
                int drawNumber = 1;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line == string.Empty || line == "\t") break;
                   
                    string[] arr = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                    var entity1 = new PowerBall()
                    {
                        DrawNumber = drawNumber,
                        DrawDate = arr[0],
                        Number1 = int.Parse(arr[1]),
                        Number2 = int.Parse(arr[2]),
                        Number3 = int.Parse(arr[3]),
                        Number4 = int.Parse(arr[4]),
                        Number5 = int.Parse(arr[5]),
                    };

                    var entity2 = new PowerBall_PowerBall()
                    {
                        DrawNumber = drawNumber,
                        DrawDate = arr[0],
                        PowerBall = int.Parse(arr[6])
                    };
                    drawNumber++;

                    records.Add(entity1);
                    records2.Add(entity2);
                }
                
                InsertDb(records);
                InsertDb_PowerBall(records2);

            }
        }

        public void InsertDb(List<PowerBall> rows)
        {
            using (var db = new LottoDb())
            {
                db.PowerBalls.AddRange(rows);
                db.SaveChanges();
            }
        }
        public void InsertDb_PowerBall(List<PowerBall_PowerBall> rows)
        {
            using (var db = new LottoDb())
            {
                db.PowerBall_PowerBall.AddRange(rows);
                db.SaveChanges();
            }
        }
    }
}
