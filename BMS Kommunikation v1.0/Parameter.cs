using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS_Kommunikation_v1._0
{
    class Parameter
    {
        public double tmp1, tmp2, tmp3, tmp4;
        public double alast;
        public double vin;
        public double zeit;// Zeitstempel der aktuell auf den Kontroller ist
        public double mvinRef;
        public double aref;
        public double rLast;
        public double maxALast;
        public double vinRef;
        public double vmin;
        public double vmax;
        public bool mFet;
        public bool fehler;
        public bool datenOK;
        public ulong time;// Akutelle Zeit auf den passiven BMS
        public ulong Whtime;// Wird benötigt für den Background Worker um so die Zeitunterschied zu berechnen
        public bool WhMessungAn;
        public List<String> WhMessung;
        public DateTime WhMessungStartzeit;
        public DateTime WhMessungEndezeit;
        public double WhMessungMessungVinStart;
        public double WhMessungMessungVinEnde;
        public double WhMessungErgebnis;
        public Int64 WHMessungAnzahl;
        public Parameter()
        {
            tmp1 = tmp2 = tmp3 = tmp4 = 0;
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
            fehler = false;
            datenOK = false;
            WhMessungAn = false;
            WhMessung = new List<string>();
            WhMessungStartzeit = DateTime.UtcNow;
            WhMessungStartzeit = WhMessungStartzeit.AddHours(2.0);
            WhMessungEndezeit = DateTime.UtcNow;
            WhMessungEndezeit = WhMessungEndezeit.AddHours(2.0);
            WhMessungMessungVinStart=0;
            WhMessungMessungVinEnde=0;
            WhMessungErgebnis=0;
            WHMessungAnzahl = 0;
        }  
    }
}
