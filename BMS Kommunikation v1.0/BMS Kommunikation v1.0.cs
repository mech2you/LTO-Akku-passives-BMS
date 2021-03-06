﻿using System;
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
        double deltT;
        DateTime utcDate;
        bool BWStop;
        bool BWeinmal;//Wird verwerdenet um die Grafik einmal zu initialisieren
        private BackgroundWorker WHMessungWorker;
        private BackgroundWorker StatusWorker;
        private BackgroundWorker AutoModeWorker;
        private BackgroundWorker AliveWorker;
        bool BWStopAliveWorker;//übertragung an BW Worker ob Alive an oder aus ist

        String AuswahlBMS = "";

        public BMSKommunikation()
        {

            InitializeComponent();
            BWStop = false;
            BWStopAliveWorker = false;
            BWeinmal = true;

            Arduino = new ArduinoConnectSerial(ButtonVerbinden, ButtonTrennen, ButtonAutoConnect, TextBoxPortArduino, TextBoxLog);
            Arduino.ThresholdReached += EventHandlerConnect;
            comboBoxAuswahl.Items.Add("01");
            comboBoxAuswahl.Items.Add("02");
            deltT = 0;
            WHMessungWorker = new BackgroundWorker();
            WHMessungWorker.WorkerReportsProgress = true;
            WHMessungWorker.WorkerSupportsCancellation = true;
            WHMessungWorker.DoWork += new DoWorkEventHandler(EventHandlerWHMessungWorker);
            WHMessungWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(EventHandlerWHMessungWorkerFetig);


            StatusWorker = new BackgroundWorker();
            StatusWorker.WorkerReportsProgress = true;
            StatusWorker.WorkerSupportsCancellation = true;
            StatusWorker.DoWork += new DoWorkEventHandler(EventHandlerStatusWorker);
            StatusWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(EventHandlerStatusWorkerFetig);


            AutoModeWorker = new BackgroundWorker();
            AutoModeWorker.WorkerReportsProgress = true;
            AutoModeWorker.WorkerSupportsCancellation = true;
            AutoModeWorker.DoWork += new DoWorkEventHandler(EventHandlerAutoModeWorker);
            AutoModeWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(EventHandlerAutoModeWorkerFetig);

            AliveWorker = new BackgroundWorker();
            AliveWorker.WorkerReportsProgress = true;
            AliveWorker.WorkerSupportsCancellation = true;
            AliveWorker.DoWork += new DoWorkEventHandler(EventHandlerAliveWorker);
            AliveWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(EventHandlerAliveWorkerFertig);


        }
        void EventHandlerConnect(object sender, ThresholdReachedEventArgs e)
        {
            if (e.Verbunden)
            {
                for (int i = 1; i <= Arduino.Datensatz168p.Count(); i++)
                {
                    Thread.Sleep(300);
                    Arduino.Send_Komando(PassivBMS.Parameter(i));
                }
                Thread.Sleep(200);
            }
        }
        public void EventHandlerAliveWorkerFertig(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // Fehler
            }
            if (BWStopAliveWorker)//Bricht den Background Worker ab wenn keine Messungen meher an sind
            {
                AliveWorker.RunWorkerAsync();
            }
        }
        public void EventHandlerAliveWorker(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= Arduino.Datensatz168p.Count(); i++)
            {
                Arduino.Send_Komando(PassivBMS.BMStoString(i) + ",alive,:X");
                System.Threading.Thread.Sleep(2000);
            }
        }


        public void EventHandlerAutoModeWorkerFetig(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // Fehler
            }
            for (int i = 0; i < Arduino.Datensatz168p.Count(); i++)
            {
                if (Arduino.Datensatz168p[i].mFet)// Deaktiviert alle Mosfets damit diese nicht an bleiben
                {
                    Arduino.Datensatz168p[i].mFet = false;
                    Arduino.Send_Komando(PassivBMS.Mosfet(i+1,false));
                    System.Threading.Thread.Sleep(50);
                }
            }
        }
        public void EventHandlerAutoModeWorker(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int k = 0;
            double summe = 0;
            double ergebnis = 0;
            double abweichung = 0.01;
            while (!worker.CancellationPending)
            {
                summe = 0;
                k = 0;
                ergebnis = 0;
                System.Threading.Thread.Sleep(1000);
                for (int i = 0; i < Arduino.Datensatz168p.Count(); i++)
                {
                    summe += Arduino.Datensatz168p[i].vin;
                    k++;
                }
                ergebnis = summe / k;

                for (int i = 0; i < Arduino.Datensatz168p.Count(); i++)
                {
                    if (Arduino.Datensatz168p[i].vin - ergebnis> abweichung)
                    {
                        if (!Arduino.Datensatz168p[i].mFet)
                        {
                            Arduino.Datensatz168p[i].mFet = true;
                            Arduino.Send_Komando(PassivBMS.Mosfet(i + 1, true));
                            System.Threading.Thread.Sleep(100);
                        }
                       
                    }
                    else
                    {
                        if (Arduino.Datensatz168p[i].mFet)
                        {
                            Arduino.Datensatz168p[i].mFet = false;
                            Arduino.Send_Komando(PassivBMS.Mosfet(i + 1, false));
                            System.Threading.Thread.Sleep(100);
                        }
                        
                    }
                }
            }
        }
            public void EventHandlerWHMessungWorkerFetig(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // Fehler
            }
            int abbrechen = 0;

            TextBoxWhMessungAnzahl.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(AuswahlBMS) - 1].WhMessung.Count());
            TextBoxWhMessung.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(AuswahlBMS) - 1].WhMessungErgebnis);
            Arduino.Datensatz168p[Convert.ToInt32(AuswahlBMS) - 1].WhMessungEndezeit= DateTime.UtcNow;
            Arduino.Datensatz168p[Convert.ToInt32(AuswahlBMS) - 1].WhMessungEndezeit.AddHours(2.0);
            TextBoxWhMessungZeitEnde.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(AuswahlBMS) - 1].WhMessungEndezeit);
            Arduino.Datensatz168p[Convert.ToInt32(AuswahlBMS) - 1].WhMessungMessungVinEnde=Arduino.Datensatz168p[Convert.ToInt32(AuswahlBMS) - 1].vin; 
            TextBoxWhMessungVinEnde.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(AuswahlBMS) - 1].vin);
                                            
            TextBoxWhMessungVinStart.Text= Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(AuswahlBMS) - 1].WhMessungMessungVinStart);
            for (int i = 0; i < Arduino.Datensatz168p.Count(); i++)
            {
                if (Arduino.Datensatz168p[Convert.ToInt32(i)].WhMessungAn)
                {
                    abbrechen++;
                }
                else
                {
                    if (Arduino.Datensatz168p[Convert.ToInt32(i)].mFet)
                    {
                        Arduino.Send_Komando(PassivBMS.Mosfet(i + 1, false));
                        Arduino.Datensatz168p[i].mFet = false;
                    }
                }
            }
            if (abbrechen!=0)//Bricht den Background Worker ab wenn keine Messungen meher an sind
            {
                WHMessungWorker.RunWorkerAsync();
            }
        }
        public void EventHandlerWHMessungWorker(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < Arduino.Datensatz168p.Count(); i++)
            {
                if (Arduino.Datensatz168p[i].WhMessungAn)//Messung für den Datensatz ist nicht an
                {
                    Arduino.Send_Komando(PassivBMS.WhMessung(i + 1));
                    System.Threading.Thread.Sleep(1000);
                    if (Arduino.Datensatz168p[i].WHMessungAnzahl > 2)// Erst ab den dritten Dattensatz beginng die Messung
                    {
                        if (Arduino.Datensatz168p[i].Whtime != Arduino.Datensatz168p[i].time)
                        {
                            deltT = Convert.ToDouble(Arduino.Datensatz168p[i].Whtime - Arduino.Datensatz168p[i].time);
                            Arduino.Datensatz168p[i].WhMessungErgebnis += deltT * Arduino.Datensatz168p[i].vin * Arduino.Datensatz168p[i].alast / 1000 / 60 / 60;//Umrechung in Wh
                            Arduino.Datensatz168p[i].Whtime = Arduino.Datensatz168p[i].time;
                            Arduino.Datensatz168p[i].WhMessung.Add(Convert.ToString(deltT) + "," + Convert.ToString(Arduino.Datensatz168p[i].vin)+","+ Convert.ToString(Arduino.Datensatz168p[i].alast));
                            Arduino.Datensatz168p[i].Whtime = Arduino.Datensatz168p[i].time;
                        }
                    }
                    else
                    {
                        Arduino.Datensatz168p[i].WhMessungMessungVinStart = Arduino.Datensatz168p[i].vin;
                        Arduino.Datensatz168p[i].Whtime = Arduino.Datensatz168p[i].time;

                    }
                    Arduino.Datensatz168p[i].WHMessungAnzahl++;

                }
                else
                {
                    if(Arduino.Datensatz168p[i].mFet)
                    {
                        Arduino.Send_Komando(PassivBMS.Mosfet(i + 1, false));
                        Arduino.Datensatz168p[i].mFet = false;
                    }
                }
                if (Arduino.Datensatz168p[i].fehler)//Bricht die Messung ab da ein Fehler aufgetreten ist
                {
                    Thread.Sleep(1000);
                    Arduino.Send_Komando(PassivBMS.Mosfet(i + 1, false));
                    Arduino.Datensatz168p[i].mFet = false;
                    break;
                }
            }
        }
        

        public void EventHandlerStatusWorkerFetig(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // Fehler
            }
            Color rot = Color.FromName("Red");
            Color blau = Color.FromName("Blue");
            int j = Arduino.Datensatz168p.Count;// Anzahl von Geräten
            for (int i = 1; i <= j; i++)
            {
                ChartAkkuzellen.Series["Akkuzellen"].Points[i - 1].SetValueY(Arduino.Datensatz168p[i - 1].vin);
                ChartAkkuzellen.Series["Akkuzellen"].Points[i - 1].IsValueShownAsLabel = true;
                ChartAkkuzellen.Series["Akkuzellen"].Points[i - 1].Font = new Font(FontFamily.GenericSansSerif, 20.0F, FontStyle.Regular, GraphicsUnit.Pixel);
                if (Arduino.Datensatz168p[i - 1].vin < Arduino.Datensatz168p[i - 1].vmax)//Wenn die Spannung zu hoch ist wird das Diagramm rot statt blau
                {
                    ChartAkkuzellen.Series["Akkuzellen"].Points[i - 1].Color = blau;
                }
                else
                {
                    ChartAkkuzellen.Series["Akkuzellen"].Points[i - 1].Color = rot;
                }
            }
            if (BWStop)
            {
                StatusWorker.RunWorkerAsync(Arduino);
            }
        }

        public void EventHandlerStatusWorker(object sender, DoWorkEventArgs e)
        {

            int j = Arduino.Datensatz168p.Count;// Anzahl von Geräten

            for (int i = 1; i <= j; i++)
            {
                Arduino.Send_Komando(PassivBMS.Status(i));
                Thread.Sleep(200);
            }
        }
        private void ButtonLaden_Click(object sender, EventArgs e)
        {
            int j = Arduino.Datensatz168p.Count;// Anzahl von Geräten
            for (int i = 1; i <= j; i++)
            {
                Arduino.Send_Komando(PassivBMS.Parameter(i));
                Thread.Sleep(500);
            }
        }

        private void ButtonSpeichern_Click(object sender, EventArgs e)//Nur die Änderungen werden übertragen an den jeweiligen Kontroller
        {
            if (TextBoxmvinRef.Text != Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].mvinRef))
            {
                Arduino.Send_Komando(PassivBMS.mvinRef(comboBoxAuswahl.Text, TextBoxmvinRef.Text));
                Thread.Sleep(300);
            }
            if (TextBoxaref.Text != Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].aref))
            {
                Arduino.Send_Komando(PassivBMS.aRef(comboBoxAuswahl.Text, TextBoxaref.Text));
                Thread.Sleep(300);
            }
            if (TextBoxrLast.Text != Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].rLast))
            {
                Arduino.Send_Komando(PassivBMS.rLast(comboBoxAuswahl.Text, TextBoxrLast.Text));
                Thread.Sleep(300);
            }
            if (TextBoxmaxALast.Text != Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].maxALast))
            {
                Arduino.Send_Komando(PassivBMS.maxALast(comboBoxAuswahl.Text, TextBoxmaxALast.Text));
                Thread.Sleep(300);
            }
            if (TextBoxvinRef.Text != Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].vinRef))
            {
                Arduino.Send_Komando(PassivBMS.vinRef(comboBoxAuswahl.Text, TextBoxvinRef.Text));
                Thread.Sleep(300);
            }
            if (TextBoxvmin.Text != Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].vmin))
            {
                Arduino.Send_Komando(PassivBMS.vmin(comboBoxAuswahl.Text, TextBoxvmin.Text));
                Thread.Sleep(300);
            }
            if (TextBoxvmax.Text != Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].vmax))
            {
                Arduino.Send_Komando(PassivBMS.vmax(comboBoxAuswahl.Text, TextBoxvmax.Text));
                Thread.Sleep(300);
            }
            
            Arduino.Send_Komando(PassivBMS.Parameter(comboBoxAuswahl.Text.Replace(",", ".")));
            Thread.Sleep(300);//Läde die Daten neu
            TextBoxmvinRef.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].mvinRef);
            TextBoxaref.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].aref);
            TextBoxrLast.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].rLast);
            TextBoxmaxALast.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].maxALast);
            TextBoxvinRef.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].vinRef);
            TextBoxvmin.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].vmin);
            TextBoxvmax.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].vmax); 
        }


        private void ButtonWHMessungStart_Click(object sender, EventArgs e)
        {
            utcDate = DateTime.UtcNow;
            utcDate = utcDate.AddHours(2.0);
            Arduino.Datensatz168p[Convert.ToInt32( AuswahlBMS)-1].mFet = true;
            Arduino.Send_Komando(PassivBMS.Mosfet(AuswahlBMS, true));
            Arduino.Datensatz168p[Convert.ToInt32(AuswahlBMS) - 1].WhMessungAn = true;
            Arduino.Send_Komando(PassivBMS.WhMessung(AuswahlBMS));
            Thread.Sleep(300);
            Arduino.Datensatz168p[Convert.ToInt32(AuswahlBMS) - 1].WhMessungStartzeit = utcDate;
            TextBoxWhMessungZeitStart.Text = Arduino.Datensatz168p[Convert.ToInt32(AuswahlBMS) - 1].WhMessungStartzeit.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("de-DE"));
            Thread.Sleep(100);
            TextBoxWhMessungVinStart.Text = "";
            TextBoxWhMessungZeitEnde.Text = "";
            TextBoxWhMessungVinEnde.Text = "";
            TextBoxWhMessungAnzahl.Text = "";
            TextBoxWhMessung.Text = "";
            Arduino.Datensatz168p[Convert.ToInt32(AuswahlBMS)-1].WHMessungAnzahl = 0;
            if (!WHMessungWorker.IsBusy)// WH messung läuft bereits und kann nicht zweimal gestartet werden Alle Messungen müssen vorher beenden werden
            {
                WHMessungWorker.RunWorkerAsync();             
            }
        }


        private void ButtonWHMessungStop_Click(object sender, EventArgs e)
        {
            Arduino.Datensatz168p[Convert.ToInt32(AuswahlBMS) - 1].WhMessungAn = false;
        }

        private void Sync_Click(object sender, EventArgs e)
        {
            int j = Arduino.Datensatz168p.Count;// Anzahl von Geräten
            for (int i = 1; i <= j; i++)
            {
                Arduino.Send_Komando(PassivBMS.Sync(i));
                Thread.Sleep(300);
            }
        }
        private void comboBoxAuswahl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox CB =  (ComboBox)sender;
            TextBoxmvinRef.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text)-1].mvinRef);
            TextBoxaref.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].aref);
            TextBoxrLast.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].rLast);
            TextBoxmaxALast.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].maxALast);
            TextBoxvinRef.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].vinRef);
            TextBoxvmin.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].vmin);
            TextBoxvmax.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(comboBoxAuswahl.Text) - 1].vmax);
            AuswahlBMS = CB.Text;

            TextBoxWhMessungVinStart.Text =Convert.ToString( Arduino.Datensatz168p[Convert.ToInt32(CB.Text)-1].WhMessungMessungVinStart);
            TextBoxWhMessungVinEnde.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(CB.Text) - 1].WhMessungMessungVinEnde);

            TextBoxWhMessungZeitEnde.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(CB.Text) - 1].WhMessungEndezeit.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("de-DE")));
            TextBoxWhMessungZeitStart.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(CB.Text) - 1].WhMessungStartzeit.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("de-DE")));
            TextBoxWhMessungAnzahl.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(CB.Text) - 1].WHMessungAnzahl);
            TextBoxWhMessung.Text = Convert.ToString(Arduino.Datensatz168p[Convert.ToInt32(CB.Text) - 1].WhMessungErgebnis);
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            BWStop = true;
            if (BWeinmal)
            {
                for (int i = 1; i <= Arduino.Datensatz168p.Count(); i++)
                {
                    Thread.Sleep(300);
                    Arduino.Send_Komando(PassivBMS.Parameter(i));
                }
                Thread.Sleep(200);
                BWeinmal = false;
                int j = Arduino.Datensatz168p.Count;// Anzahl von Geräten
                for (int i = 1; i <= j; i++)
                {
                    ChartAkkuzellen.Series["Akkuzellen"].Points.AddXY(i, 3.0);
                    ChartAkkuzellen.Series["Akkuzellen"].Points[i - 1].IsValueShownAsLabel = true;
                    ChartAkkuzellen.Series["Akkuzellen"].Points[i - 1].Font = new Font(FontFamily.GenericSansSerif, 20.0F, FontStyle.Regular, GraphicsUnit.Pixel);
                }
            }
            if (!StatusWorker.IsBusy)
            {

                StatusWorker.RunWorkerAsync();
            }
            
        }

        private void tabPage3_Leave(object sender, EventArgs e)
        {
            BWStop = false;
        }

        private void CheckBoxMosfet_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox isONorOFF=  (CheckBox)sender;
            if (isONorOFF.Checked)
            {
                Arduino.Send_Komando(PassivBMS.Mosfet(comboBoxAuswahl.Text,true));
            }
            else
            {
                Arduino.Send_Komando(PassivBMS.Mosfet(comboBoxAuswahl.Text, false));
            }
        }

        private void checkBoxAutoMode_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CB = (CheckBox)sender;
            if (CB.Checked)
            {
                ButtonWHMessungStart.Enabled = false;//WH Messung ist nicht erlaubt
                ButtonWHMessungStop.Enabled = false;
                for (int i = 0; i < Arduino.Datensatz168p.Count(); i++)//Beendet alle Wh Messungen
                {
                    Arduino.Datensatz168p[i].WhMessungAn = false;
                }
                for (int i = 1; i <= Arduino.Datensatz168p.Count(); i++)
                {
                    Thread.Sleep(300);
                    Arduino.Send_Komando(PassivBMS.Parameter(i));
                }
                Thread.Sleep(200);
                BWStop = true;

                if (!StatusWorker.IsBusy)
                {
                    StatusWorker.RunWorkerAsync();
                }
                Thread.Sleep(500);
                AutoModeWorker.RunWorkerAsync();
            }
            else
            {
                ButtonWHMessungStart.Enabled = true;
                ButtonWHMessungStop.Enabled = true;
                AutoModeWorker.CancelAsync();
            }
        }

        private void CheckBoxAlive_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CB = (CheckBox)sender;
            if (CB.Checked)
            {
                BWStopAliveWorker = true;
                if (!AliveWorker.IsBusy)
                {

                    AliveWorker.RunWorkerAsync();
                }
                else
                {
                    CB.Checked = false;
                }
            }
            else
            {
                BWStopAliveWorker = false;
            }
        }
    }
}
