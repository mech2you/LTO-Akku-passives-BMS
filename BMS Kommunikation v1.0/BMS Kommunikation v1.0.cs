using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace BMS_Kommunikation_v1._0
{
    public partial class BMSKommunikation : Form
    {
        ArduinoConnectSerial Arduino;
        ulong Whtime;
        double deltT;
        double WHMessung;
        int WHMessungAnzahl;
        DateTime utcDate;

        public BackgroundWorker WHMessungWorker;
        public BMSKommunikation()
        {

            InitializeComponent();
            Arduino = new ArduinoConnectSerial(ButtonVerbinden, ButtonTrennen, ButtonAutoConnect, TextBoxPortArduino, TextBoxLog);
            comboBoxAuswahl.Items.Add("01");
            comboBoxAuswahl.Items.Add("02");
            Whtime = 0;
            WHMessung = 0;
            deltT = 0;
            WHMessungAnzahl = 0;

            WHMessungWorker = new BackgroundWorker();
            WHMessungWorker.WorkerReportsProgress = true;
            WHMessungWorker.WorkerSupportsCancellation = true;
            WHMessungWorker.DoWork += new DoWorkEventHandler(EventHandlerWHMessungWorker);
            WHMessungWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(EventHandlerWHMessungWorkerFetig);

            utcDate = DateTime.UtcNow;


        }
        public void EventHandlerWHMessungWorkerFetig(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // Fehler
            }
            TextBoxWhMessungAnzahl.Text = Convert.ToString(WHMessungAnzahl - 2);//-2 sind die verworfenen Messungen
            TextBoxWhMessung.Text = Convert.ToString(WHMessung);
            WHMessung = 0;
            WHMessungAnzahl = 0;
        }
        public void EventHandlerWHMessungWorker(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            ArduinoConnectSerial Arduino = (ArduinoConnectSerial)e.Argument;
            while (!worker.CancellationPending)
            {   
                Arduino.Send_Komando("01,WhMessung,:X");
                System.Threading.Thread.Sleep(1000);
                if (WHMessungAnzahl > 2)// Erst ab den dritten Dattensatz beginng die Messung
                {
                    if (Whtime != Arduino.Datensatz168p[0].time)
                    {
                        deltT = Convert.ToDouble(Whtime - Arduino.Datensatz168p[0].time);
                        WHMessung += deltT * Arduino.Datensatz168p[0].vin * Arduino.Datensatz168p[0].alast/1000/60/60;//Umrechung in Wh
                        Whtime = Arduino.Datensatz168p[0].time;
                    }
                }
                else
                {
                    TextBoxWhMessungVinStart.Invoke(new Action(() =>
                    {
                        TextBoxWhMessungVinStart.Clear();
                        TextBoxWhMessungVinStart.AppendText(Convert.ToString(Arduino.Datensatz168p[0].vin));

                    }));
                    Whtime = Arduino.Datensatz168p[0].time;
                }
                WHMessungAnzahl++;
            }
        }
        private void ButtonLaden_Click(object sender, EventArgs e)
        {

            Arduino.Send_Komando("01,para,:X");
            TextBoxmvinRef.Text=Convert.ToString(Arduino.Datensatz168p[0].mvinRef);
            TextBoxaref.Text = Convert.ToString(Arduino.Datensatz168p[0].aref);
            TextBoxrLast.Text = Convert.ToString(Arduino.Datensatz168p[0].rLast);
            TextBoxmaxALast.Text = Convert.ToString(Arduino.Datensatz168p[0].maxALast);
            TextBoxvinRef.Text = Convert.ToString(Arduino.Datensatz168p[0].vinRef);
            TextBoxvmin.Text = Convert.ToString(Arduino.Datensatz168p[0].vmin);
            TextBoxvmax.Text = Convert.ToString(Arduino.Datensatz168p[0].vmax);
        }

        private void ButtonSpeichern_Click(object sender, EventArgs e)
        {
            Arduino.Send_Komando("02,para,:X");
            TextBoxmvinRef.Text = Convert.ToString(Arduino.Datensatz168p[0].mvinRef);
            TextBoxaref.Text = Convert.ToString(Arduino.Datensatz168p[0].aref);
            TextBoxrLast.Text = Convert.ToString(Arduino.Datensatz168p[0].rLast);
            TextBoxmaxALast.Text = Convert.ToString(Arduino.Datensatz168p[0].maxALast);
            TextBoxvinRef.Text = Convert.ToString(Arduino.Datensatz168p[0].vinRef);
            TextBoxvmin.Text = Convert.ToString(Arduino.Datensatz168p[0].vmin);
            TextBoxvmax.Text = Convert.ToString(Arduino.Datensatz168p[0].vmax);
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            Thread.Sleep(200);
            Arduino.Send_Komando("01,para,:X");
            Thread.Sleep(100);
            Arduino.Send_Komando("02,para,:X");
        }

        private void ButtonWHMessungStart_Click(object sender, EventArgs e)
        {
            utcDate = DateTime.UtcNow;
            utcDate = utcDate.AddHours(2.0);
            Arduino.Send_Komando("01,on,:X");
            Thread.Sleep(100);

            if (!WHMessungWorker.IsBusy)
            {
                WHMessungWorker.RunWorkerAsync(Arduino);
                TextBoxWhMessungZeitStart.Text = utcDate.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("de-DE"));

                Thread.Sleep(100);
                TextBoxWhMessungVinStart.Text = "";
                TextBoxWhMessungZeitEnde.Text = "";
                TextBoxWhMessungVinEnde.Text = "";
                TextBoxWhMessungAnzahl.Text = "";
                TextBoxWhMessung.Text = "";
            }
            

        }

        private void ButtonWHMessungStop_Click(object sender, EventArgs e)
        {
            
            WHMessungWorker.CancelAsync();
            Thread.Sleep(100);
            Arduino.Send_Komando("01,off,:X");
            Thread.Sleep(100);
            Arduino.Send_Komando("01,WhMessung,:X");
            Thread.Sleep(100);
            utcDate = DateTime.UtcNow;
            utcDate = utcDate.AddHours(2);
            TextBoxWhMessungZeitEnde.Text = utcDate.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("de-DE"));
            TextBoxWhMessungVinEnde.Text = Convert.ToString(Arduino.Datensatz168p[0].vin);
            
        }

        private void Sync_Click(object sender, EventArgs e)
        {
            Arduino.Send_Komando("01,Sync,:X");
            Thread.Sleep(1800);
            Arduino.Send_Komando("02,Sync,:X");
        }
    }
}
