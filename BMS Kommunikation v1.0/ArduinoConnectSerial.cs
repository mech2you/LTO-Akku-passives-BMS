using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;
using System.Globalization;

namespace BMS_Kommunikation_v1._0
{
    class ArduinoConnectSerial
    {
        #region Variablen
        public bool arduino_connect; // Gibt den Verbinungszustand wieder
        private SerialPort port_send; // Serial Port für das Senden/Empfangen der Daten
        private Button trennen; // Knopf vom Form für das Verbinden
        private Button verbinden; // Knopf vom Form für das Trennen
        private Button auto_connect; // Knopf vom Form für AutoConnect
        private TextBox portBox; // Textbox die den Portnamen enthält
        private TextBox logBox; // Textbox aus der Form wo die Daten angezeigt werden sollen
        private String port; // Beinhaltet den aktuellen Port vom Arduino z.B. COM5
        private int serial_baud; // Datenübertragungsgeschwindigkeit vom Arduino und der Software müssen gleich sein
        private delegate void SafeCallDelegate(string text); // Funktion für das sichere Übertragen von Daten vom Tread zum Form
        string datenString; //beinhaltet den zusammengesetzen Datenstream der ankommt
        public List<Parameter> Datensatz168p;
        #endregion

        //////////////////////////////////////////////////////////////////////////////////

        #region Konstruktoren
        /// <summary>
        /// DruckkopfHexapod Konstruktor 
        /// <param name="_verbinden">object ->Button zum verbinden</param>
        /// <param name="_trennen">object ->Button zum trennen</param>
        /// <param name="_auto_connect">objekt-> Button automatisch wiederverbinden</param>
        /// <param name="_port">objekt-> TextBox die den COM Port enthält</param>
        /// <param name="_log">object->TextBox zum ausgeben des Loggs und der Kommunikation</param>
        /// </summary>
        public ArduinoConnectSerial(object _verbinden, object _trennen, object _auto_connect, object _port, object _log)
        {
            verbinden = (Button)_verbinden;
            verbinden.Click += new EventHandler(ButtonVerbinden_Click);


            trennen = (Button)_trennen;
            trennen.Click += new EventHandler(ButtonTrennen_Click);

            auto_connect = (Button)_auto_connect;
            auto_connect.Click += new EventHandler(ButtonAutoConnent_Click);

            portBox = (TextBox)_port;
            port = "";
            serial_baud = 19200;

            logBox = (TextBox)_log;

            trennen.Visible = false;
            verbinden.Visible = true;
            arduino_connect = false;
            datenString = "";
            Datensatz168p = new List<Parameter>();//erstellt eine List mit zwei Geräten
            Datensatz168p.Add(new Parameter());
            Datensatz168p.Add(new Parameter());

        }
        #endregion

        //////////////////////////////////////////////////////////////////////////////////
        #region EventHandler
        private void ButtonVerbinden_Click(object sender, EventArgs e)
        {
            PortOpen();
        }
        private void ButtonTrennen_Click(object sender, EventArgs e)
        {
            PortClose();
        }

        private void ButtonAutoConnent_Click(object sender, EventArgs e)
        {
            AutoConnect();
        }
        #endregion
        //////////////////////////////////////////////////////////////////////////////////
        #region Arduino Kommunikation
        /// <summary>
        /// PortOpen Verbindet sich um Arduino
        /// </summary>
        /// <returns>
        /// true bei erfolgreicher Verbinung
        /// false bei einen Fehler
        /// </returns>
        public bool PortOpen()
        {
            bool connect = false;
            port = portBox.Text;
            if (port_send == null)
            {
                port_send = new SerialPort(port, serial_baud);
                port_send.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);

                connect = SerialConnect();

                if (connect)
                {
                    trennen.Visible = true;
                    verbinden.Visible = false;
                    auto_connect.Visible = false;
                }
                arduino_connect = connect;
                ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
                args.Verbunden = true;
                OnThresholdReached(args);
                return connect;
            }
            else if (port_send.IsOpen == false)
            {
                port_send.PortName = port;
                connect = SerialConnect();

                if (connect)
                {
                    trennen.Visible = true;
                    verbinden.Visible = false;
                    auto_connect.Visible = false;
                }
                arduino_connect = connect;
                return connect;
            }
            else
            {
                arduino_connect = false;
                return false;
            }
        }
        /// <summary>
        /// PortOpen Verbindet sich um Arduino über einen bestimmten Port
        /// | String-> <paramref name="_port"/> = Portnummer wie z.B. COM5
        /// </summary>
        /// <returns>
        /// true bei erfolgreicher Verbinung
        /// false bei einen Fehler
        /// </returns>
        private bool PortOpen(String _port)
        {
            bool connect = false;
            port = _port;
            if (port_send == null)
            {
                port_send = new SerialPort(port, serial_baud);
                port_send.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);

                connect = SerialConnect();

                if (connect)
                {
                    trennen.Visible = true;
                    verbinden.Visible = false;
                    auto_connect.Visible = false;
                }
                arduino_connect = connect;
                return connect;
            }
            else if (port_send.IsOpen == false)
            {
                port_send.PortName = port;
                connect = SerialConnect();

                if (connect)
                {
                    trennen.Visible = true;
                    verbinden.Visible = false;
                    auto_connect.Visible = false;
                }
                arduino_connect = connect;
                return connect;
            }
            else
            {
                arduino_connect = false;
                return false;
            }
        }
        /// <summary>
        /// Sucht automatisch nach den Aruduino von COM Port 0 bis 16
        /// </summary>
        /// <returns>
        /// true bei gefundener und erfolgreicher Verbinung
        /// false bei einen Fehler
        /// </returns>
        public bool AutoConnect()
        {
            bool tmp = false;
            for (int i = 0; i <= 16; i++)
            {
                if (PortOpen("COM" + Convert.ToString(i)))
                {
                    tmp = true;
                    logBox.Text += "Port " + port + " gefunden" + Environment.NewLine;
                    portBox.Text = port;
                }
            }
            if (!tmp)
            {
                logBox.Text += "Keinen Arduino am COM Port gefunden" + Environment.NewLine;

            }
            return tmp;
        }
        /// <summary>
        /// Öffnet einen Seriellen Port und gibt im Falle eines Fehler diesen in der Textbox aus
        /// </summary>
        /// <returns>
        /// true bei erfolgreichem versand der Nachricht
        /// false bei einen Fehler + Fehlertext
        /// </returns>
        private bool SerialConnect()
        {
            try
            {
                port_send.Open();
                logBox.Text += "Verbunden mit " + port + Environment.NewLine;

            }
            catch (UnauthorizedAccessException)
            {
                logBox.Text += "COM PORT Belegt" + Environment.NewLine;

                return false;
            }
            catch (System.IO.IOException)
            {
                logBox.Text += "Keine Verbinung... falscher Com PORT?-> " + port + Environment.NewLine;

                return false;
            }
            catch (System.ArgumentException)
            {
                logBox.Text += "COM Port muss als Beispiel so heißen COM5" + Environment.NewLine;

                return false;
            }
            catch (InvalidOperationException)
            {
                logBox.Text += "Serial PORT Verbinung breits geöffnet" + Environment.NewLine;

                return false;
            }
            return true;
        }


        /// <summary>
        /// SerialPort_DataReceived Event Handler für die ankommenen Seriellen Daten
        /// <param name="sender">object -> SerialPort</param> 
        /// </summary>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            String tmp = "";
            string[] split;
            datenString += sp.ReadExisting();
            string iTmp = "";

            if (datenString.Contains(":X"))// Ende des Strings wurde empfangen
            {
                if (datenString != null)
                {
                    if (datenString.IndexOf(PassivBMS.vorsilbeBMS) != -1)//ist AX vorhanden?
                    {
                        
                        if (datenString.IndexOf(PassivBMS.vorsilbeBMS) != 0)//Kontrolle ob vor den Daten etwas ankommt ansonste wird dieses weggeschnitten
                        {
                            Log_Text("!!!Fehler Daten wurden gekürzt!!!->" + datenString + Environment.NewLine);
                            datenString = datenString.Substring(datenString.IndexOf(PassivBMS.vorsilbeBMS));
                            
                        }
                        if (datenString.Length > 2)

                        {
                            tmp = datenString.Substring(5, datenString.Length - 2 - 5);//5 ist für das anfangswert AX02, und 2 ist für das Ende :X
                            split = tmp.Split(new Char[] { ':' });// Zerteilt den String auf die einzelen Daten

                            if (split.Length > 1)
                            {
                                if (split[0] != "")
                                {
                                    try
                                    {
                                        iTmp = split[0];
                                    }
                                    catch
                                    {
                                        iTmp = "";
                                        Log_Text("!!!Fehler in den Daten!!!->" + split[0] + Environment.NewLine);
                                    }
                                    int i = Convert.ToInt32(datenString.Substring(2, 2)) - 1;

                                    switch (iTmp.Substring(0, 1))
                                    {
                                        case "p"://Fehlerfall alast
                                            Datensatz168p[i].fehler = true;
                                            break;
                                        case "q"://Fehlerfall vinmax
                                            Datensatz168p[i].fehler = true;
                                            break;
                                        case "r"://Fehlerfall vinmin
                                            Datensatz168p[i].fehler = true;
                                            break;
                                        case "s"://Fehlerfall fehler beim cmdInt
                                            Datensatz168p[i].fehler = true;
                                            break;
                                        case "u":// Kommando Komma nicht erkannt
                                            Datensatz168p[i].fehler = true;
                                            break;
                                        case "o"://Fehlerfall Daten wurden nicht verstanden
                                            Datensatz168p[i].fehler = true;
                                            break;
                                        case "t"://Fehlerfall Es fehlt das Komma für para1
                                            Datensatz168p[i].fehler = true;
                                            break;


                                        case "a"://Mosfet an
                                                Datensatz168p[i].mFet = true;
                                            break;
                                        case "b"://Mosfet aus
                                                Datensatz168p[i].mFet = false;
                                            break;
                                        case "c"://WhMessung
                                            if (split.Length == 4)
                                            {
                                                try
                                                {
                                                    Datensatz168p[i].alast = Convert.ToDouble(split[1], new CultureInfo("en-US"));
                                                    Datensatz168p[i].vin = Convert.ToDouble(split[2], new CultureInfo("en-US"));
                                                    Datensatz168p[i].time = Convert.ToUInt64(split[3], new CultureInfo("en-US"));
                                                }
                                                catch
                                                {
                                                    Log_Text("!!!System.FormatException!!!" + Environment.NewLine);
                                                }

                                            }
                                            else
                                            {
                                                Log_Text("Fehler bei der Wh Messungsmeldung" + Environment.NewLine);
                                            }
                                            break;
                                        case "d"://Status
                                            try
                                            {
                                                if (split.Length == 9)
                                                {
                                                    Datensatz168p[i].tmp1 = Convert.ToDouble(split[1], new CultureInfo("en-US"));
                                                    Datensatz168p[i].tmp2 = Convert.ToDouble(split[2], new CultureInfo("en-US"));
                                                    Datensatz168p[i].tmp3 = Convert.ToDouble(split[3], new CultureInfo("en-US"));
                                                    Datensatz168p[i].tmp4 = Convert.ToDouble(split[4], new CultureInfo("en-US"));
                                                    Datensatz168p[i].alast = Convert.ToDouble(split[5], new CultureInfo("en-US"));
                                                    Datensatz168p[i].vin = Convert.ToDouble(split[6], new CultureInfo("en-US"));
                                                    Datensatz168p[i].mvin= Convert.ToDouble(split[7], new CultureInfo("en-US"));
                                                    Datensatz168p[i].zeit = Convert.ToUInt64(split[8], new CultureInfo("en-US"));
                                                }
                                                else
                                                {

                                                    Log_Text("Fehler bei der Statusmeldung" + Environment.NewLine);

                                                }

                                            }
                                            catch
                                            {
                                                Log_Text("!!!System.FormatException Status!!!" + Environment.NewLine);
                                            }
                                            break;
                                        case "e"://Parameter
                                            try
                                            {
                                                if (split.Length == 8)
                                                {
                                                    Datensatz168p[i].mvinRef = Convert.ToDouble(split[1], new CultureInfo("en-US"));
                                                    Datensatz168p[i].aref = Convert.ToDouble(split[2], new CultureInfo("en-US"));
                                                    Datensatz168p[i].rLast = Convert.ToDouble(split[3], new CultureInfo("en-US"));
                                                    Datensatz168p[i].maxALast = Convert.ToDouble(split[4], new CultureInfo("en-US"));
                                                    Datensatz168p[i].vinRef = Convert.ToDouble(split[5], new CultureInfo("en-US"));
                                                    Datensatz168p[i].vmin = Convert.ToDouble(split[6], new CultureInfo("en-US"));
                                                    Datensatz168p[i].vmax = Convert.ToDouble(split[7], new CultureInfo("en-US"));
                                                }
                                                else
                                                {
                                                    Log_Text("Fehler bei den Parameter" + Environment.NewLine);
                                                }

                                            }
                                            catch
                                            {
                                                Log_Text("!!!System.FormatException Parameter!!!" + Environment.NewLine);
                                            }
                                            break;
                                    }
                                }
                            }
                            Log_Text("Arduino Data->" + datenString + Environment.NewLine);
                        }
                    }
                    else
                    {
                        Log_Text("!!!Fehler Daten Vorsilbe fehlt!!!->" + datenString + Environment.NewLine);
                    }
                }
                    
                datenString = "";
            }
        }
        /// <summary>
        /// Send_Komando Sendet das Kommando über den offenen Seriellen Port
        /// <param name="kommando">String -> Kommando zum senden</param> 
        /// </summary>
        /// <returns>
        /// true bei erfolgreichen Versand
        /// false bei einen Fehler
        /// </returns>
        public bool Send_Komando(String kommando)
        {
            if (port_send != null && port_send.IsOpen)
            {
                
                port_send.Write(kommando);
                Log_Text("Send Kommando->" + kommando + Environment.NewLine);

                return true;
            }
            else
            {
                Log_Text("Nicht verbunden" + Environment.NewLine);
                return false;
            }

        }



        /// <summary>
        /// Schließt die serielle Verbinung zum Arduino
        /// </summary>
        /// <returns>
        /// true bei erfolgreicher Trennung
        /// false bei einen Fehler
        /// </returns>
        public bool PortClose()
        {
            if (port_send != null && port_send.IsOpen)
            {
                port_send.Close();
                trennen.Visible = false;
                verbinden.Visible = true;
                auto_connect.Visible = true;
                arduino_connect = false;
                return true;
            }
            else
            {
                arduino_connect = false;
                return false;
            }
        }
        #endregion
        //////////////////////////////////////////////////////////////////////////////////

        #region Extrafunktionen 
        /// <summary>
        /// Log_Text Übergibt sicher den Text an logBox 
        /// <param name="text">String -> Text der an die logBox übergeben werden soll </param>
        /// </summary>
        private void Log_Text(String text)
        {
            if (logBox.InvokeRequired)
            {

                logBox.Invoke(new Action(() =>
                {
                    logBox.AppendText(text + Environment.NewLine);

                }));
            }
            else
            {
                logBox.Text += text + Environment.NewLine;
            }
        }

        #endregion
        protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
        {
            EventHandler<ThresholdReachedEventArgs> handler = ThresholdReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<ThresholdReachedEventArgs> ThresholdReached;
    }
    public class ThresholdReachedEventArgs : EventArgs
    {
        public bool Verbunden { get; set; }
    }
}