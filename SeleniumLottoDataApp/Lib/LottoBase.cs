using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.Lib
{
    public class LottoBase
    {
        public IWebDriver Driver { get; set; }
        public LottoBase()
        {
            Driver = new ChromeDriver(); // Launches Browser for English version

            //driver.Manage().Window.Maximize(); // Maximizes Browser

            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
        }

        internal virtual void InsertDb() { }
        

        public IDictionary DicDate = new Dictionary<string, string>  {
                    { "January","01" },
                    { "February","02" },
                    { "March","03" },
                    { "April","04" },
                    { "May","05" },
                    { "June","06" },
                    { "July","07" },
                    { "August","08" },
                    { "September","09" },
                    { "October","10" },
                    { "November","11" },
                    { "December","12" }
        };

        public IDictionary DicDateShort = new Dictionary<string, string>  {
                    { "Jan","01" },
                    { "Feb","02" },
                    { "Mar","03" },
                    { "Apr","04" },
                    { "May","05" },
                    { "Jun","06" },
                    { "Jul","07" },
                    { "Aug","08" },
                    { "Sep","09" },
                    { "Oct","10" },
                    { "Nov","11" },
                    { "Dec","12" }
        };

        public IDictionary DicDateShort2 = new Dictionary<string, string> {
                    { "JAN","01" },
                    { "FEB","02" },
                    { "MAR","03" },
                    { "APR","04" },
                    { "MAY","05" },
                    { "JUN","06" },
                    { "JUL","07" },
                    { "AUG","08" },
                    { "SEP","09" },
                    { "OCT","10" },
                    { "NOV","11" },
                    { "DEC","12" }
        };


    }
}
