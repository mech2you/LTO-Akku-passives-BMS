using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_Kommunikation_v1._0
{
    static class PassivBMS
    {
        static public string BMStoString(int i)
        {
            if (i<9)
            {
                return "0"+Convert.ToString(i);
            }
            else
            {
                return Convert.ToString(i);
            }
        }
    }
}
