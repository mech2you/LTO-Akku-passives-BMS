using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_Kommunikation_v1._0
{
    class Parameter
    {
        public double t1,t2,t3,t4;
        public double alast;
        public double vin;
        public double zeit;
        public double mvinRef;
        public double aref;
        public double rLast;
        public double maxALast;
        public double vinRef;
        public double vmin;
        public double vmax;
        public bool mFet;
        public bool datenOK;
        public ulong time;
        public Parameter()
        {
            t1 = t2 = t3 = t4 = 0;
            alast = 0; ;
            vin = 0;
            zeit = 0;
            mvinRef = 0;
            aref = 0;
            rLast = 0;
            maxALast = 0;
            vinRef = 0;
            vmin = 0;
            vmax = 0;
            mFet = false;
            datenOK = false;
        }
    }
}
