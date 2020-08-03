using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS_Kommunikation_v1._0
{
    static class PassivBMS
    {
        public static String vorsilbeBMS = "AX";
        static public string BMStoString(int i)
        {
            if (i<9)
            {
                return vorsilbeBMS+"0" +Convert.ToString(i);
            }
            else
            {
                return vorsilbeBMS+Convert.ToString(i);
            }
        }
        #region WhMessung
        static public string WhMessung(int i)
        {
            return BMStoString(i) + ",c,:X";
        }
        static public string WhMessung(string i)
        {
            return BMStoString(Convert.ToInt32(i)) + ",c,:X";
        }
        #endregion
        #region Mosfet An / Aus
        static public string Mosfet(int i,bool OnOff)
        {
            if (OnOff)
            {
                return BMStoString(i) + ",a,:X";
            }
            else
            {
                return BMStoString(i) + ",b,:X";
            }
            
        }
        static public string Mosfet(string i, bool OnOff)
        {
            if (OnOff)
            {
                return BMStoString(Convert.ToInt32(i)) + ",a,:X";
            }
            else
            {
                return BMStoString(Convert.ToInt32(i)) + ",b,:X";
            }
        }
        #endregion
        #region Status
        static public string Status(int i)
        {
            return BMStoString(i) + ",d,:X";
        }
        static public string Status(string i)
        {
            return BMStoString(Convert.ToInt32(i)) + ",d,:X";
        }
        #endregion
        #region Parameter
        static public string Parameter(int i)
        {
            return BMStoString(i) + ",e,:X";
        }
        static public string Parameter(string i)
        {
            return BMStoString(Convert.ToInt32(i)) + ",e,:X";
        }
        #endregion
        #region mvinRef
        static public string mvinRef(string i,string wert)
        {
            return BMStoString(Convert.ToInt32(i)) + ",h,"+ wert.Replace(",", ".") + ":X";
        }
        #endregion
        #region aRef
        static public string aRef(string i, string wert)
        {
            return BMStoString(Convert.ToInt32(i)) + ",i," + wert.Replace(",", ".") + ":X";
        }
        #endregion
        #region rLast
        static public string rLast(string i, string wert)
        {
            return BMStoString(Convert.ToInt32(i)) + ",j," + wert.Replace(",", ".") + ":X";
        }
        #endregion
        #region maxALast
        static public string maxALast(string i, string wert)
        {
            return BMStoString(Convert.ToInt32(i)) + ",k," + wert.Replace(",", ".") + ":X";
        }
        #endregion
        #region vinRef
        static public string vinRef(string i, string wert)
        {
            return BMStoString(Convert.ToInt32(i)) + ",l," + wert.Replace(",", ".") + ":X";
        }
        #endregion
        #region vmin
        static public string vmin(string i, string wert)
        {
            return BMStoString(Convert.ToInt32(i)) + ",m," + wert.Replace(",", ".") + ":X";
        }
        #endregion
        #region vmax
        static public string vmax(string i, string wert)
        {
            return BMStoString(Convert.ToInt32(i)) + ",n," + wert.Replace(",", ".") + ":X";
        }
        #endregion
        
        #region Sync
        static public string Sync(int i)
        {
            return BMStoString(i) + ",g,:X";
        }
        #endregion
    }
}
