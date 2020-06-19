namespace BMS_Kommunikation_v1._0
{
    partial class BMSKommunikation
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.ButtonTrennen = new System.Windows.Forms.Button();
            this.ButtonVerbinden = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxPortArduino = new System.Windows.Forms.TextBox();
            this.ButtonLaden = new System.Windows.Forms.Button();
            this.ButtonSpeichern = new System.Windows.Forms.Button();
            this.ButtonAutoConnect = new System.Windows.Forms.Button();
            this.TextBoxLog = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.WhMessungTab = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.VinStart = new System.Windows.Forms.Label();
            this.TextBoxWhMessungVinEnde = new System.Windows.Forms.TextBox();
            this.TextBoxWhMessungVinStart = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.Startzeit = new System.Windows.Forms.Label();
            this.TextBoxWhMessungZeitEnde = new System.Windows.Forms.TextBox();
            this.TextBoxWhMessungZeitStart = new System.Windows.Forms.TextBox();
            this.TextBoxWhMessung = new System.Windows.Forms.TextBox();
            this.TextBoxWhMessungAnzahl = new System.Windows.Forms.TextBox();
            this.ButtonWHMessungStop = new System.Windows.Forms.Button();
            this.ButtonWHMessungStart = new System.Windows.Forms.Button();
            this.Einstellungen = new System.Windows.Forms.TabPage();
            this.CheckBoxMosfet = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBoxmaxALast = new System.Windows.Forms.TextBox();
            this.TextBoxvinRef = new System.Windows.Forms.TextBox();
            this.TextBoxvmin = new System.Windows.Forms.TextBox();
            this.TextBoxvmax = new System.Windows.Forms.TextBox();
            this.TextBoxrLast = new System.Windows.Forms.TextBox();
            this.TextBoxaref = new System.Windows.Forms.TextBox();
            this.TextBoxmvinRef = new System.Windows.Forms.TextBox();
            this.Status = new System.Windows.Forms.TabPage();
            this.ChartAkkuzellen = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBoxAuswahl = new System.Windows.Forms.ComboBox();
            this.Sync = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.checkBoxAutoMode = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.WhMessungTab.SuspendLayout();
            this.Einstellungen.SuspendLayout();
            this.Status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartAkkuzellen)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonTrennen
            // 
            this.ButtonTrennen.Location = new System.Drawing.Point(408, 14);
            this.ButtonTrennen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ButtonTrennen.Name = "ButtonTrennen";
            this.ButtonTrennen.Size = new System.Drawing.Size(112, 35);
            this.ButtonTrennen.TabIndex = 23;
            this.ButtonTrennen.Text = "trennen";
            this.ButtonTrennen.UseVisualStyleBackColor = true;
            // 
            // ButtonVerbinden
            // 
            this.ButtonVerbinden.Location = new System.Drawing.Point(287, 14);
            this.ButtonVerbinden.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ButtonVerbinden.Name = "ButtonVerbinden";
            this.ButtonVerbinden.Size = new System.Drawing.Size(112, 35);
            this.ButtonVerbinden.TabIndex = 22;
            this.ButtonVerbinden.Text = "verbinden";
            this.ButtonVerbinden.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 20);
            this.label1.TabIndex = 21;
            this.label1.Text = "Port Arduino";
            // 
            // TextBoxPortArduino
            // 
            this.TextBoxPortArduino.Location = new System.Drawing.Point(132, 17);
            this.TextBoxPortArduino.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TextBoxPortArduino.Name = "TextBoxPortArduino";
            this.TextBoxPortArduino.Size = new System.Drawing.Size(148, 26);
            this.TextBoxPortArduino.TabIndex = 20;
            this.TextBoxPortArduino.Text = "COM5";
            // 
            // ButtonLaden
            // 
            this.ButtonLaden.Location = new System.Drawing.Point(853, 814);
            this.ButtonLaden.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ButtonLaden.Name = "ButtonLaden";
            this.ButtonLaden.Size = new System.Drawing.Size(112, 35);
            this.ButtonLaden.TabIndex = 24;
            this.ButtonLaden.Text = "Laden";
            this.ButtonLaden.UseVisualStyleBackColor = true;
            this.ButtonLaden.Click += new System.EventHandler(this.ButtonLaden_Click);
            // 
            // ButtonSpeichern
            // 
            this.ButtonSpeichern.Location = new System.Drawing.Point(973, 814);
            this.ButtonSpeichern.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ButtonSpeichern.Name = "ButtonSpeichern";
            this.ButtonSpeichern.Size = new System.Drawing.Size(112, 35);
            this.ButtonSpeichern.TabIndex = 25;
            this.ButtonSpeichern.Text = "Speichern";
            this.ButtonSpeichern.UseVisualStyleBackColor = true;
            this.ButtonSpeichern.Click += new System.EventHandler(this.ButtonSpeichern_Click);
            // 
            // ButtonAutoConnect
            // 
            this.ButtonAutoConnect.Location = new System.Drawing.Point(539, 14);
            this.ButtonAutoConnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ButtonAutoConnect.Name = "ButtonAutoConnect";
            this.ButtonAutoConnect.Size = new System.Drawing.Size(124, 35);
            this.ButtonAutoConnect.TabIndex = 26;
            this.ButtonAutoConnect.Text = "Auto Connect";
            this.ButtonAutoConnect.UseVisualStyleBackColor = true;
            // 
            // TextBoxLog
            // 
            this.TextBoxLog.Location = new System.Drawing.Point(1191, 180);
            this.TextBoxLog.Multiline = true;
            this.TextBoxLog.Name = "TextBoxLog";
            this.TextBoxLog.Size = new System.Drawing.Size(891, 898);
            this.TextBoxLog.TabIndex = 27;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.WhMessungTab);
            this.tabControl1.Controls.Add(this.Einstellungen);
            this.tabControl1.Controls.Add(this.Status);
            this.tabControl1.Location = new System.Drawing.Point(30, 151);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1159, 931);
            this.tabControl1.TabIndex = 30;
            // 
            // WhMessungTab
            // 
            this.WhMessungTab.Controls.Add(this.label12);
            this.WhMessungTab.Controls.Add(this.VinStart);
            this.WhMessungTab.Controls.Add(this.TextBoxWhMessungVinEnde);
            this.WhMessungTab.Controls.Add(this.TextBoxWhMessungVinStart);
            this.WhMessungTab.Controls.Add(this.label11);
            this.WhMessungTab.Controls.Add(this.label10);
            this.WhMessungTab.Controls.Add(this.label9);
            this.WhMessungTab.Controls.Add(this.Startzeit);
            this.WhMessungTab.Controls.Add(this.TextBoxWhMessungZeitEnde);
            this.WhMessungTab.Controls.Add(this.TextBoxWhMessungZeitStart);
            this.WhMessungTab.Controls.Add(this.TextBoxWhMessung);
            this.WhMessungTab.Controls.Add(this.TextBoxWhMessungAnzahl);
            this.WhMessungTab.Controls.Add(this.ButtonWHMessungStop);
            this.WhMessungTab.Controls.Add(this.ButtonWHMessungStart);
            this.WhMessungTab.Location = new System.Drawing.Point(4, 29);
            this.WhMessungTab.Name = "WhMessungTab";
            this.WhMessungTab.Padding = new System.Windows.Forms.Padding(3);
            this.WhMessungTab.Size = new System.Drawing.Size(1151, 898);
            this.WhMessungTab.TabIndex = 0;
            this.WhMessungTab.Text = "Wh Messung";
            this.WhMessungTab.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(195, 183);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 20);
            this.label12.TabIndex = 13;
            this.label12.Text = "Vin Ende";
            // 
            // VinStart
            // 
            this.VinStart.AutoSize = true;
            this.VinStart.Location = new System.Drawing.Point(60, 183);
            this.VinStart.Name = "VinStart";
            this.VinStart.Size = new System.Drawing.Size(71, 20);
            this.VinStart.TabIndex = 12;
            this.VinStart.Text = "Vin Start";
            // 
            // TextBoxWhMessungVinEnde
            // 
            this.TextBoxWhMessungVinEnde.Location = new System.Drawing.Point(190, 206);
            this.TextBoxWhMessungVinEnde.Name = "TextBoxWhMessungVinEnde";
            this.TextBoxWhMessungVinEnde.Size = new System.Drawing.Size(100, 26);
            this.TextBoxWhMessungVinEnde.TabIndex = 11;
            // 
            // TextBoxWhMessungVinStart
            // 
            this.TextBoxWhMessungVinStart.Location = new System.Drawing.Point(39, 206);
            this.TextBoxWhMessungVinStart.Name = "TextBoxWhMessungVinStart";
            this.TextBoxWhMessungVinStart.Size = new System.Drawing.Size(100, 26);
            this.TextBoxWhMessungVinStart.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(249, 261);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 20);
            this.label11.TabIndex = 9;
            this.label11.Text = "Berechnete Wh";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(56, 261);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(172, 20);
            this.label10.TabIndex = 8;
            this.label10.Text = "Anzahl der Messungen";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(238, 125);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 20);
            this.label9.TabIndex = 7;
            this.label9.Text = "Endzeit";
            // 
            // Startzeit
            // 
            this.Startzeit.AutoSize = true;
            this.Startzeit.Location = new System.Drawing.Point(60, 125);
            this.Startzeit.Name = "Startzeit";
            this.Startzeit.Size = new System.Drawing.Size(69, 20);
            this.Startzeit.TabIndex = 6;
            this.Startzeit.Text = "Startzeit";
            // 
            // TextBoxWhMessungZeitEnde
            // 
            this.TextBoxWhMessungZeitEnde.Location = new System.Drawing.Point(230, 151);
            this.TextBoxWhMessungZeitEnde.Name = "TextBoxWhMessungZeitEnde";
            this.TextBoxWhMessungZeitEnde.Size = new System.Drawing.Size(231, 26);
            this.TextBoxWhMessungZeitEnde.TabIndex = 5;
            // 
            // TextBoxWhMessungZeitStart
            // 
            this.TextBoxWhMessungZeitStart.Location = new System.Drawing.Point(39, 151);
            this.TextBoxWhMessungZeitStart.Name = "TextBoxWhMessungZeitStart";
            this.TextBoxWhMessungZeitStart.Size = new System.Drawing.Size(170, 26);
            this.TextBoxWhMessungZeitStart.TabIndex = 4;
            // 
            // TextBoxWhMessung
            // 
            this.TextBoxWhMessung.Location = new System.Drawing.Point(253, 284);
            this.TextBoxWhMessung.Name = "TextBoxWhMessung";
            this.TextBoxWhMessung.Size = new System.Drawing.Size(233, 26);
            this.TextBoxWhMessung.TabIndex = 3;
            // 
            // TextBoxWhMessungAnzahl
            // 
            this.TextBoxWhMessungAnzahl.Location = new System.Drawing.Point(39, 284);
            this.TextBoxWhMessungAnzahl.Name = "TextBoxWhMessungAnzahl";
            this.TextBoxWhMessungAnzahl.Size = new System.Drawing.Size(100, 26);
            this.TextBoxWhMessungAnzahl.TabIndex = 2;
            // 
            // ButtonWHMessungStop
            // 
            this.ButtonWHMessungStop.Location = new System.Drawing.Point(230, 70);
            this.ButtonWHMessungStop.Name = "ButtonWHMessungStop";
            this.ButtonWHMessungStop.Size = new System.Drawing.Size(90, 37);
            this.ButtonWHMessungStop.TabIndex = 1;
            this.ButtonWHMessungStop.Text = "stop";
            this.ButtonWHMessungStop.UseVisualStyleBackColor = true;
            this.ButtonWHMessungStop.Click += new System.EventHandler(this.ButtonWHMessungStop_Click);
            // 
            // ButtonWHMessungStart
            // 
            this.ButtonWHMessungStart.Location = new System.Drawing.Point(60, 70);
            this.ButtonWHMessungStart.Name = "ButtonWHMessungStart";
            this.ButtonWHMessungStart.Size = new System.Drawing.Size(90, 37);
            this.ButtonWHMessungStart.TabIndex = 0;
            this.ButtonWHMessungStart.Text = "start";
            this.ButtonWHMessungStart.UseVisualStyleBackColor = true;
            this.ButtonWHMessungStart.Click += new System.EventHandler(this.ButtonWHMessungStart_Click);
            // 
            // Einstellungen
            // 
            this.Einstellungen.Controls.Add(this.CheckBoxMosfet);
            this.Einstellungen.Controls.Add(this.label8);
            this.Einstellungen.Controls.Add(this.label7);
            this.Einstellungen.Controls.Add(this.label6);
            this.Einstellungen.Controls.Add(this.label5);
            this.Einstellungen.Controls.Add(this.label4);
            this.Einstellungen.Controls.Add(this.label3);
            this.Einstellungen.Controls.Add(this.label2);
            this.Einstellungen.Controls.Add(this.TextBoxmaxALast);
            this.Einstellungen.Controls.Add(this.TextBoxvinRef);
            this.Einstellungen.Controls.Add(this.TextBoxvmin);
            this.Einstellungen.Controls.Add(this.TextBoxvmax);
            this.Einstellungen.Controls.Add(this.TextBoxrLast);
            this.Einstellungen.Controls.Add(this.TextBoxaref);
            this.Einstellungen.Controls.Add(this.TextBoxmvinRef);
            this.Einstellungen.Controls.Add(this.ButtonLaden);
            this.Einstellungen.Controls.Add(this.ButtonSpeichern);
            this.Einstellungen.Location = new System.Drawing.Point(4, 29);
            this.Einstellungen.Name = "Einstellungen";
            this.Einstellungen.Padding = new System.Windows.Forms.Padding(3);
            this.Einstellungen.Size = new System.Drawing.Size(1151, 898);
            this.Einstellungen.TabIndex = 1;
            this.Einstellungen.Text = "Einstellungen";
            this.Einstellungen.UseVisualStyleBackColor = true;
            // 
            // CheckBoxMosfet
            // 
            this.CheckBoxMosfet.AutoSize = true;
            this.CheckBoxMosfet.Location = new System.Drawing.Point(505, 99);
            this.CheckBoxMosfet.Name = "CheckBoxMosfet";
            this.CheckBoxMosfet.Size = new System.Drawing.Size(84, 24);
            this.CheckBoxMosfet.TabIndex = 41;
            this.CheckBoxMosfet.Text = "Mosfet";
            this.CheckBoxMosfet.UseVisualStyleBackColor = true;
            this.CheckBoxMosfet.CheckedChanged += new System.EventHandler(this.CheckBoxMosfet_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 291);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(193, 20);
            this.label8.TabIndex = 40;
            this.label8.Text = "Maximale Spannung Akku";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 259);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(189, 20);
            this.label7.TabIndex = 39;
            this.label7.Text = "Minimale Spannung Akku";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 227);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(304, 20);
            this.label6.TabIndex = 38;
            this.label6.Text = "Akku Spannungsanpassung Widerstände";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(265, 20);
            this.label5.TabIndex = 37;
            this.label5.Text = "Maximaler Strom am Lastwiderstand";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(201, 20);
            this.label4.TabIndex = 36;
            this.label4.Text = "Widerstandswert an R Last";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(241, 20);
            this.label3.TabIndex = 35;
            this.label3.Text = "168p Interne Referenzspannung";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(317, 20);
            this.label2.TabIndex = 34;
            this.label2.Text = "Mosfet Spannungsanpassung Widerstände";
            // 
            // TextBoxmaxALast
            // 
            this.TextBoxmaxALast.Location = new System.Drawing.Point(374, 192);
            this.TextBoxmaxALast.Name = "TextBoxmaxALast";
            this.TextBoxmaxALast.Size = new System.Drawing.Size(100, 26);
            this.TextBoxmaxALast.TabIndex = 32;
            // 
            // TextBoxvinRef
            // 
            this.TextBoxvinRef.Location = new System.Drawing.Point(374, 224);
            this.TextBoxvinRef.Name = "TextBoxvinRef";
            this.TextBoxvinRef.Size = new System.Drawing.Size(100, 26);
            this.TextBoxvinRef.TabIndex = 31;
            // 
            // TextBoxvmin
            // 
            this.TextBoxvmin.Location = new System.Drawing.Point(374, 256);
            this.TextBoxvmin.Name = "TextBoxvmin";
            this.TextBoxvmin.Size = new System.Drawing.Size(100, 26);
            this.TextBoxvmin.TabIndex = 30;
            // 
            // TextBoxvmax
            // 
            this.TextBoxvmax.Location = new System.Drawing.Point(374, 288);
            this.TextBoxvmax.Name = "TextBoxvmax";
            this.TextBoxvmax.Size = new System.Drawing.Size(100, 26);
            this.TextBoxvmax.TabIndex = 29;
            // 
            // TextBoxrLast
            // 
            this.TextBoxrLast.Location = new System.Drawing.Point(374, 160);
            this.TextBoxrLast.Name = "TextBoxrLast";
            this.TextBoxrLast.Size = new System.Drawing.Size(100, 26);
            this.TextBoxrLast.TabIndex = 28;
            // 
            // TextBoxaref
            // 
            this.TextBoxaref.Location = new System.Drawing.Point(374, 128);
            this.TextBoxaref.Name = "TextBoxaref";
            this.TextBoxaref.Size = new System.Drawing.Size(100, 26);
            this.TextBoxaref.TabIndex = 27;
            // 
            // TextBoxmvinRef
            // 
            this.TextBoxmvinRef.Location = new System.Drawing.Point(374, 96);
            this.TextBoxmvinRef.Name = "TextBoxmvinRef";
            this.TextBoxmvinRef.Size = new System.Drawing.Size(100, 26);
            this.TextBoxmvinRef.TabIndex = 26;
            // 
            // Status
            // 
            this.Status.Controls.Add(this.ChartAkkuzellen);
            this.Status.Location = new System.Drawing.Point(4, 29);
            this.Status.Name = "Status";
            this.Status.Padding = new System.Windows.Forms.Padding(3);
            this.Status.Size = new System.Drawing.Size(1151, 898);
            this.Status.TabIndex = 2;
            this.Status.Text = "Status";
            this.Status.UseVisualStyleBackColor = true;
            this.Status.Enter += new System.EventHandler(this.tabPage3_Enter);
            this.Status.Leave += new System.EventHandler(this.tabPage3_Leave);
            // 
            // ChartAkkuzellen
            // 
            chartArea1.Name = "ChartArea1";
            this.ChartAkkuzellen.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.ChartAkkuzellen.Legends.Add(legend1);
            this.ChartAkkuzellen.Location = new System.Drawing.Point(-4, 0);
            this.ChartAkkuzellen.Name = "ChartAkkuzellen";
            series1.ChartArea = "ChartArea1";
            series1.IsVisibleInLegend = false;
            series1.IsXValueIndexed = true;
            series1.Legend = "Legend1";
            series1.Name = "Akkuzellen";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.ChartAkkuzellen.Series.Add(series1);
            this.ChartAkkuzellen.Size = new System.Drawing.Size(1159, 889);
            this.ChartAkkuzellen.TabIndex = 2;
            this.ChartAkkuzellen.Text = "chart1";
            // 
            // comboBoxAuswahl
            // 
            this.comboBoxAuswahl.FormattingEnabled = true;
            this.comboBoxAuswahl.Location = new System.Drawing.Point(34, 100);
            this.comboBoxAuswahl.Name = "comboBoxAuswahl";
            this.comboBoxAuswahl.Size = new System.Drawing.Size(145, 28);
            this.comboBoxAuswahl.TabIndex = 33;
            this.comboBoxAuswahl.SelectedIndexChanged += new System.EventHandler(this.comboBoxAuswahl_SelectedIndexChanged);
            // 
            // Sync
            // 
            this.Sync.Location = new System.Drawing.Point(408, 63);
            this.Sync.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Sync.Name = "Sync";
            this.Sync.Size = new System.Drawing.Size(112, 35);
            this.Sync.TabIndex = 31;
            this.Sync.Text = "Sync";
            this.Sync.UseVisualStyleBackColor = true;
            this.Sync.Click += new System.EventHandler(this.Sync_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1195, 151);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 20);
            this.label13.TabIndex = 32;
            this.label13.Text = "Log";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(40, 70);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(109, 20);
            this.label14.TabIndex = 34;
            this.label14.Text = "Akku Auswahl";
            // 
            // checkBoxAutoMode
            // 
            this.checkBoxAutoMode.AutoSize = true;
            this.checkBoxAutoMode.Location = new System.Drawing.Point(684, 21);
            this.checkBoxAutoMode.Name = "checkBoxAutoMode";
            this.checkBoxAutoMode.Size = new System.Drawing.Size(109, 24);
            this.checkBoxAutoMode.TabIndex = 35;
            this.checkBoxAutoMode.Text = "AutoMode";
            this.checkBoxAutoMode.UseVisualStyleBackColor = true;
            this.checkBoxAutoMode.CheckedChanged += new System.EventHandler(this.checkBoxAutoMode_CheckedChanged);
            // 
            // BMSKommunikation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1050);
            this.Controls.Add(this.checkBoxAutoMode);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.Sync);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.TextBoxLog);
            this.Controls.Add(this.ButtonAutoConnect);
            this.Controls.Add(this.ButtonTrennen);
            this.Controls.Add(this.ButtonVerbinden);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxAuswahl);
            this.Controls.Add(this.TextBoxPortArduino);
            this.Name = "BMSKommunikation";
            this.Text = "BMSKommunikation";
            this.tabControl1.ResumeLayout(false);
            this.WhMessungTab.ResumeLayout(false);
            this.WhMessungTab.PerformLayout();
            this.Einstellungen.ResumeLayout(false);
            this.Einstellungen.PerformLayout();
            this.Status.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChartAkkuzellen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonTrennen;
        private System.Windows.Forms.Button ButtonVerbinden;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBoxPortArduino;
        private System.Windows.Forms.Button ButtonLaden;
        private System.Windows.Forms.Button ButtonSpeichern;
        private System.Windows.Forms.Button ButtonAutoConnect;
        private System.Windows.Forms.TextBox TextBoxLog;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage WhMessungTab;
        private System.Windows.Forms.TabPage Einstellungen;
        private System.Windows.Forms.ComboBox comboBoxAuswahl;
        private System.Windows.Forms.TextBox TextBoxmaxALast;
        private System.Windows.Forms.TextBox TextBoxvinRef;
        private System.Windows.Forms.TextBox TextBoxvmin;
        private System.Windows.Forms.TextBox TextBoxvmax;
        private System.Windows.Forms.TextBox TextBoxrLast;
        private System.Windows.Forms.TextBox TextBoxaref;
        private System.Windows.Forms.TextBox TextBoxmvinRef;
        private System.Windows.Forms.TabPage Status;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button ButtonWHMessungStop;
        private System.Windows.Forms.Button ButtonWHMessungStart;
        private System.Windows.Forms.TextBox TextBoxWhMessung;
        private System.Windows.Forms.TextBox TextBoxWhMessungAnzahl;
        private System.Windows.Forms.TextBox TextBoxWhMessungZeitEnde;
        private System.Windows.Forms.TextBox TextBoxWhMessungZeitStart;
        private System.Windows.Forms.TextBox TextBoxWhMessungVinStart;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label Startzeit;
        private System.Windows.Forms.TextBox TextBoxWhMessungVinEnde;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label VinStart;
        private System.Windows.Forms.Button Sync;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartAkkuzellen;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox CheckBoxMosfet;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkBoxAutoMode;
    }
}

