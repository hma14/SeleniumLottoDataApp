using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLottoDataApp.BusinessModels
{
    public class Constants
    {
        public enum LottoNames
        {
            BC49 = 1,
            Lotto649 = 2,
            LottoMax = 3,
            LottoCash4Life = 4,
            Cash4Life_CashBall = 5,


        }
        public enum LottoNumberRange
        {
            BC49 = 49,
            Lotto649 = 49,
            LottoMax = 50,
            LottoCash4Life = 60,
            Cash4Life_CashBall = 4,
        }
    }
}
