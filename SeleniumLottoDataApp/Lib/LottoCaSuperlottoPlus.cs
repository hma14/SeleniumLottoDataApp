﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoCaSuperlottoPlus : LottoBase
    {
        public LottoCaSuperlottoPlus()
        {
            Driver.Url = "http://www.calottery.com/play/draw-games/superlotto-plus";           
        }

        private string searchDrawDate()
        {
            var dats = Driver.FindElements(By.ClassName("draw-cards--draw-date"));
            var dat = dats.First().FindElement(By.TagName("strong"));
            var arr = dat.Text.Split();
            var mo = arr[0].Split('/')[1];
            var da = arr[1].Trim(',');
            var yr = arr[2];
            var date = yr + "-" + DicDateShort2[mo] + "-" + da;
            return date;
        }

        private List<string> searchDrawNumbers()
        {
            List<string> NList = new List<string>();
            var cls = Driver.FindElements(By.ClassName("draw-cards--winning-numbers"));
            var lis = cls.First().FindElements(By.TagName("li"));
            foreach (var li in lis)
            {
                NList.Add(li.Text);
            }
            NList[5] = NList[5].Split('\r')[0];
            return NList;
        }

        internal override void InsertDb()
        {
            using (var db = new LottoDb())
            {
                var list = db.CaSuperlottoPlus.ToList();
                IList<Tuple<int, string>> dates = list.Select(x => new Tuple<int, string>(x.DrawNumber, x.DrawDate)).ToList();
                var lastDrawDate = dates.LastOrDefault().Item2;
                var currentDrawDate = searchDrawDate();

                if (currentDrawDate != lastDrawDate)
                {
                    var lastDrawNumber = dates.LastOrDefault().Item1;
                    var numbers = searchDrawNumbers();

                    var entity = new CaSuperlottoPlu();
                    entity.DrawNumber = lastDrawNumber + 1;
                    entity.DrawDate = currentDrawDate;
                    entity.Number1 = int.Parse(numbers[0]);
                    entity.Number2 = int.Parse(numbers[1]);
                    entity.Number3 = int.Parse(numbers[2]);
                    entity.Number4 = int.Parse(numbers[3]);
                    entity.Number5 = int.Parse(numbers[4]);
                    
                    // save to db
                    db.CaSuperlottoPlus.Add(entity);
                    db.SaveChanges();

                    var mega = new CaSuperlottoPlus_Mega();
                    mega.DrawNumber = lastDrawNumber + 1;
                    mega.DrawDate = currentDrawDate;
                    mega.Mega = int.Parse(numbers[5]);

                    // save to db for Mega
                    db.CaSuperlottoPlus_Mega.Add(mega);
                    db.SaveChanges();
                }
            }
            Driver.Close();
            Driver.Quit();
        }
    }
}
