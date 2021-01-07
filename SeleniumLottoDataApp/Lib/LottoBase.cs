using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SeleniumLottoDataApp.Lib
{

    public class LottoBase
    {
        //public RemoteWebDriver Driver { get; set; }
        public ChromeDriver Driver { get; set; }

        public LottoBase()
        {
            //PhantomJSDriverService service = PhantomJSDriverService.CreateDefaultService();
            //service.IgnoreSslErrors = true;
            //service.LoadImages = false;
            //service.ProxyType = "none";
            //service.SuppressInitialDiagnosticInformation = true;
            //service.AddArgument("--webdriver-loglevel=NONE");

            //Driver = new PhantomJSDriver(service);
            //Driver.Manage().Window.Size = new Size(1024, 768);

            

            var chromeOptions = new ChromeOptions
            {
                BinaryLocation = @"C:\Program Files (x86)\google\chrome\Application\chrome.exe", 
                //DebuggerAddress = "127.0.0.1:9222"
            };

            chromeOptions.AddArguments(new List<string>()
            {
                "--silent-launch",
                "--no-startup-window",
                "no-sandbox",
                "--window-size=1920,1080",
                "--disable-gpu",
                "--disable-extensions",
                "--proxy-server='direct://'",
                "--proxy-bypass-list=*",
                "--start-maximized",
                "--headless",
            });

            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;    // This is to hidden the console.
            Driver = new ChromeDriver(chromeDriverService, chromeOptions);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);

        }
        
        public void CloseDriver()
        {
            Driver.Quit();
        }

        internal virtual void InsertDb() { }
        public virtual void InsertDb(int drawNumber, string drawDate, string[] numbers) { }



        public IDictionary DicDate = new Dictionary<string, string>  {
                    { "January","1" },
                    { "February","2" },
                    { "March","3" },
                    { "April","4" },
                    { "May","5" },
                    { "June","6" },
                    { "July","7" },
                    { "August","8" },
                    { "September","9" },
                    { "October","10" },
                    { "November","11" },
                    { "December","12" }
        };

        public IDictionary DicDateShort = new Dictionary<string, string>  {
                    { "Jan","1" },
                    { "Feb","2" },
                    { "Mar","3" },
                    { "Apr","4" },
                    { "May","5" },
                    { "Jun","6" },
                    { "Jul","7" },
                    { "Aug","8" },
                    { "Sep","9" },
                    { "Oct","10" },
                    { "Nov","11" },
                    { "Dec","12" }
        };

        public IDictionary DicDateShort2 = new Dictionary<string, string> {
                    { "JAN","1" },
                    { "FEB","2" },
                    { "MAR","3" },
                    { "APR","4" },
                    { "MAY","5" },
                    { "JUN","6" },
                    { "JUL","7" },
                    { "AUG","8" },
                    { "SEP","9" },
                    { "OCT","10" },
                    { "NOV","11" },
                    { "DEC","12" }
        };

        public IDictionary DicDateShort3 = new Dictionary<string, string> {
                    { "JAN","1" },
                    { "FEB","2" },
                    { "MAR","3" },
                    { "APR","4" },
                    { "MAY","5" },
                    { "JUN","6" },
                    { "JUL","7" },
                    { "AUG","8" },
                    { "SEPT","9" },
                    { "OCT","10" },
                    { "NOV","11" },
                    { "DEC","12" }
        };
    }
}
