using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using SeleniumLottoDataApp;
using SeleniumLottoDataApp.BusinessModels;
using SeleniumLottoDataApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataGen.Lib
{
    public abstract class LottoGenBase
    {
        public void InsertLottoNumberDb(List<List<LottoNumber>> rows)
        {
            using (var db = new LottoDb())
            {
                List<LottoType> lottoTypes = new List<LottoType>();
                List<Number> numbers = new List<Number>();
                foreach (var rs in rows)
                {
                    // LottoType
                    var row = rs.First();
                    LottoType lottoType = new LottoType
                    {
                        Id = Guid.NewGuid(),
                        LottoName = (int)row.LottoName,
                        DrawDate = row.DrawDate,
                        DrawNumber = row.DrawNumber,
                        NumberRange = (int)row.NumberRange,
                    };
                    lottoTypes.Add(lottoType);

                    // Numbers
                    foreach (var r in rs)
                    {
                        Number num = new Number
                        {
                            Id = Guid.NewGuid(),
                            Value = r.Number,
                            LottoTypeId = lottoType.Id,
                            Distance = r.Distance,
                            IsHit = r.IsHit,
                            NumberofDrawsWhenHit = r.NumberofDrawsWhenHit,
                            IsBonusNumber = r.IsBonusNumber,
                            TotalHits = r.TotalHits,
                        };
                        numbers.Add(num);
                    }
                }
                try
                {
                    db.LottoTypes.AddRange(lottoTypes);
                    db.Numbers.AddRange(numbers);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }
            }
        }

        public string ConvertPdfToText(string pdfPath, string txtPath)
        {
            using (PdfReader pdfReader = new PdfReader(pdfPath))
            using (PdfDocument pdfDoc = new PdfDocument(pdfReader))
            using (StreamWriter sw = new StreamWriter(txtPath))
            {
                for (int page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
                {
                    // Extract text from the current page
                    string pageText = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page));
                    sw.WriteLine(pageText);
                }
            }
            return txtPath;
        }

        public  virtual void ParseData()
        {

        }
    }


}
