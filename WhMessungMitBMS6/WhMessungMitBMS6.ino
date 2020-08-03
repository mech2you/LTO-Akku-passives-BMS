//Version 5.0
// Reset-Funktionen: Sprung zu Adresse 0
void (*Reset_C)(void) = 0;
void Reset_A(void) { asm volatile ("jmp 0 \n"); }
#define tmp1 A0
#define tmp2 A1
#define tmp3 A2
#define tmp4 A3
#define MVin A4
#define Vin A5
#define MosfetPin 9
#define gruenLED 2
#define gelbLED 3

char Name[5] = "AX02";//Name vom Gerät
float vinRef = 5.79; // Wird verwendet um die Abweichung der Widerstände am Vin anzupassen
float mvinRef = 11.30;
float aref = 1.080;//Interne Referenzspannungsquell
float rLast = 0.25;//Lastwiderstand

// Temparatursensoren 1-4
float sensorValue1ref = 1;
float sensorValue2ref = 1;
float sensorValue3ref = 1;
float sensorValue4ref = 1;

unsigned long  gruenT = 0;
bool gruen = false;

bool stringComplete = false;
String cmdString = ""; // Wird verwendet um die ankommenden Daten zu verarbeiten
String datenString = ""; // Enthält die ankommenden unveränderten Daten die über Uart eintreffen
String tmpdatenString = ""; // Enthält die ankommenden und verändert diese falls der Befehl anghängte Daten beinhaltet

unsigned int cmdInt = 0; // Anzahl der Fehler die kein Anfang und Endstring im Kommando vorkam




float maxALast = 10.0; //maximale Stromlast für den Mosfet
unsigned long zeitstempel;// Zeitstempel für die Synchronisation

float vmin = 2.000; // Minimal zugelassen Spannung.
float vmax = 7.500; //Ab welcher Spannung schalter der Kontroller den Mosfet an und geht auf Störung
bool geantworet = false;
void setup() {
  analogReference(INTERNAL);
  pinMode(MosfetPin, OUTPUT);
  pinMode(gelbLED, OUTPUT);
  pinMode(gruenLED, OUTPUT);
  digitalWrite(gelbLED, HIGH);
  digitalWrite(gruenLED, HIGH);
  delay(500);
  digitalWrite(gelbLED, LOW);
  digitalWrite(gruenLED, LOW);
  digitalWrite(MosfetPin, LOW);
  pinMode(tmp1, INPUT);
  pinMode(tmp2, INPUT);
  pinMode(tmp3, INPUT);
  pinMode(tmp4, INPUT);
  pinMode(Vin, INPUT);
  pinMode(MVin, INPUT);
  Serial.begin(19200);
  for (int i = 0; i < 10; i++) {//Wird verwendetn um Dummywerte die beim umändern der Aref Quelle entstehen zu entfernen
    analogRead(tmp1);
    analogRead(tmp2);
    analogRead(tmp3);
    analogRead(tmp4);
    analogRead(Vin);
    analogRead(MVin);
  }
  cmdString.reserve(64);
  datenString.reserve(64);
  tmpdatenString.reserve(64);
  Serial.setTimeout(100);
  zeitstempel = millis();
}

////////////

void setMosfetOn() {
  digitalWrite(MosfetPin, HIGH);
}

void setMosfetOff() {
  digitalWrite(MosfetPin, LOW);
}


float getVin() {
  float tmpIn = 0;
  int i = 0;
  while (true) { //Mittelt die Messwerte über 10 Stück um so Abweichungen zu reduzieren
    i++;
    tmpIn += analogRead(Vin) * (aref / 1023.0) * vinRef;
    if (i >= 10) {
      break;
    }
  }
  tmpIn = tmpIn / i;
  return tmpIn;//Ermittlung der Gesamtspannung
}
float getMvin() {
  float tmpIn = 0;
  int i = 0;
  while (true) { //Mittelt die Messwerte über 10 Stück um so Abweichungen zu reduzieren
    i++;
    tmpIn += analogRead(MVin) * (aref / 1023.0) * mvinRef;
    if (i >= 10) {
      break;
    }
  }
  tmpIn = tmpIn / i;
  return tmpIn;//Ermittlung des Spannungsabfalls am Mosfet
}

float getalast() {
  return (getVin() - getMvin()) / rLast; //Ermittlung des Stroms am Lastwiderstand
}

float getTmp1() {
  float tmpIn = 0;
  int i = 0;
  while (true) { //Mittelt die Messwerte über 10 Stück um so Abweichungen zu reduzieren
    i++;
    tmpIn += (analogRead(tmp1) * sensorValue1ref);
    if (i >= 10) {
      break;
    }
  }
  tmpIn = tmpIn / i;
  return tmpIn;//Ermittlung der Temparatur 1
}
float getTmp2() {
  float tmpIn = 0;
  int i = 0;
  while (true) { //Mittelt die Messwerte über 10 Stück um so Abweichungen zu reduzieren
    i++;
    tmpIn += (analogRead(tmp2) * sensorValue2ref);
    if (i >= 10) {
      break;
    }
  }
  tmpIn = tmpIn / i;
  return tmpIn;//Ermittlung der Temparatur 2
}
float getTmp3() {
  float tmpIn = 0;
  int i = 0;
  while (true) { //Mittelt die Messwerte über 10 Stück um so Abweichungen zu reduzieren
    i++;
    tmpIn += (analogRead(tmp3) * sensorValue3ref);
    if (i >= 10) {
      break;
    }
  }
  tmpIn = tmpIn / i;
  return tmpIn;//Ermittlung der Temparatur 3
}
float getTmp4() {
  float tmpIn = 0;
  int i = 0;
  while (true) { //Mittelt die Messwerte über 10 Stück um so Abweichungen zu reduzieren
    i++;
    tmpIn += (analogRead(tmp4) * sensorValue4ref);
    if (i >= 10) {
      break;
    }
  }
  tmpIn = tmpIn / i;
  return tmpIn;//Ermittlung der Temparatur 4
}

void loop() {
  bool kommandoOK = false;
  //LED Steuerung
  if (gruen) {
    gruenT = millis() + 100;// Wie lange soll die LED leuchten wenn Daten ankommen
    gruen = false;
    digitalWrite(gruenLED, HIGH);
  }
  if (millis() > gruenT) {
    digitalWrite(gruenLED, LOW);
  }
  if (GetMillis() % 3000 < 75) { // Alive Signal alle 3 Sekunden 75 ms an
    digitalWrite(gelbLED, HIGH);
  } else {
    digitalWrite(gelbLED, LOW);
  }
  if (getalast() > maxALast) {// zu hoher Strom am Mosfet
    Serial.print(String(Name) + ",p,:" + String(getalast(), 1) + ":X");
    Serial.flush();
  }
  if (getVin() > vmax) {// Zu hohe Spannung am Akku. Der Mosfet wird eingeschaltet
    Serial.print(String(Name) + ",q,:" + String(getVin(), 2) + ":X");
    Serial.flush();
    setMosfetOn();
  }
  if (getVin() < vmin) {// Die Spannung ist am Akku zu niedrig. Schaltet den Mosfet aus
    Serial.print(String(Name) + ",r,:" + String(getVin(), 2) + ":X");
    Serial.flush();
    setMosfetOff();
  }
  if (stringComplete) {
    kommandoOK = false;
    int8_t intAnfang = 0; // Anfang des Kommandos
    int8_t intEnde = 0; //Ende des Kommandos
    intAnfang = cmdString.indexOf("AX");
    intEnde = cmdString.indexOf(":X");
    if (intAnfang != -1 && intEnde != -1) { // es gibt ein Anfang und Ende vom Befehl

      if (intAnfang != 0) {//Die Anfangsequnez befindet sich nicht am Anfang
        cmdString = cmdString.substring(intAnfang); // Kürzt den String von vorne da die Daten nicht zugeordent werden können
      }
      if (cmdString.substring(cmdString.length() - 2) == ":X") { // Überprüft ob das Kommando vollständig empfangen wurde und keine andere Daten angehänbt sind

        tmpdatenString = cmdString;
      } else { // Daten sind angehänbt
        tmpdatenString = String(cmdString.substring(0, intEnde + 2)); //Speichert das Kommando im tmpdatenString
        cmdString = cmdString.substring(intEnde + 2); //Daten für das nächste serial Event werden gelassen und nur das Kommando wird herausgeschnitten
      }
      kommandoOK = true; //Daten sind bereit zumm verarbeiten
      cmdInt = 0;// Setzt den Fehlerspeicher zurück da das Kommando verarbeitet wurde
    } else {
      cmdInt++;
      if (cmdInt > 62) {
        Serial.print(String(Name) + ",s,:X");
        Serial.flush();
        cmdString = "";
      }
    }

    if (kommandoOK) {//Kommando wurde erkannt
      String adresse = ""; //Adresse des Controllers
      String par1 = ""; //zusäzliche Parameter
      String par2 = ""; //zusäzliche Parameter
      int int_tmp = 0;
      int_tmp = tmpdatenString.indexOf(",");
      if (int_tmp != -1) {
        adresse = tmpdatenString.substring(0, int_tmp); // Speichern des Adresse
      } else {
        Serial.print(String(Name) + ",t,:X");
        Serial.flush();
        cmdString = "";
      }
      if (adresse == Name) {// Gilt die Kommunikation für dieser Kontroller
        geantworet = false;
        tmpdatenString = tmpdatenString.substring(int_tmp + 1);
        int_tmp = tmpdatenString.indexOf(",");
        if (int_tmp != -1) { //Komma wurde gefunden
          par1 = tmpdatenString.substring(0, int_tmp); // Speichern der zusäzlichen Parameter
          par2 = tmpdatenString.substring(int_tmp + 1, tmpdatenString.indexOf(":"));
          int_tmp = tmpdatenString.indexOf(":");
        } else {
          par1 = "";
          par2 = "";
          Serial.print(String(Name) + ",u,:X");//Komma wurde nicht gefunden
        }
        if (par1 == "a") {
          Serial.print(String(Name) + ",a,:X");
          Serial.flush();
          setMosfetOn();
          geantworet = true;
        }
        if (par1 == "b") {
          Serial.print(String(Name) + ",b,:X");
          Serial.flush();
          setMosfetOff();
          geantworet = true;
        }
        if (par1 == "c") {
          Serial.print(String(Name) + ",c,:" + String(getalast(), 2) + ":");
          Serial.print(String(getVin(), 2) + ":" + String(GetMillis()) + ":X");
          Serial.flush();
          geantworet = true;
        }
        if (par1 == "d") {
          Serial.print(String(Name) + ",d,:" + String(getTmp1(), 1) + ":" );//print muss gesplittet werden da sonst der Speicher nicht ausreicht
          Serial.print(String(getTmp2(), 1) + ":" + String(getTmp3(), 1) + ":");
          Serial.print(String(getTmp4(), 1) + ":" + String(getalast(), 2) + ":" );
          Serial.print(String(getVin(), 2) + ":" + String(getMvin(), 2) );
          Serial.print(":" + String(GetMillis()) + ":X");
          Serial.flush();
          geantworet = true;
        }
        if (par1 == "e") {//gibt alle Parameter aus
          Serial.print(String(Name) + ",e,:" + String(mvinRef, 3) + ":" );//print muss gesplittet werden da sonst der Speicher nicht ausreicht
          Serial.print(String(aref, 3) + ":" +String(rLast, 3) + ":");
          Serial.print(String(maxALast, 1) +":"+String(vinRef, 3) + ":"); 
          Serial.print(String(vmin, 3) + ":" + String(vmax, 3) + ":X");
          Serial.flush();
          geantworet = true;
        }
        if (par1 == "f") {//gibt die Spannung am MosFet aus
          Serial.print(String(Name) + ",f,:" + String(getMvin(), 2) + ":X");
          Serial.flush();
          geantworet = true;
        }
        if (par1 == "g") {// Synchronisiert die Zeiten an den Kontrollern
          SetMillisToZero();
          Serial.print(String(Name) + ",g,:X");
          Serial.flush();
          geantworet = true;
        }
        if (par1 == "h") {// Wird verwendet um den Spannungsteiler am Mosfet anzupassen
          mvinRef = par2.toFloat();
          Serial.print(String(Name) + ",h,:X");
          Serial.flush();
          geantworet = true;
        }
        if (par1 == "i") {// Wird verwendet um die interne Refernzespannung zu ändern über Uart zu setzen
          aref = par2.toFloat();
          Serial.print(String(Name) + ",i,:X");
          Serial.flush();
          geantworet = true;
        }
        if (par1 == "j") {// Wird verwendet um die rLast über Uart zu setzen
          rLast = par2.toFloat();
          Serial.print(String(Name) + ",j,:X");
          Serial.flush();
          geantworet = true;
        }
        if (par1 == "k") {// maximalen Storm am rLast für zu Fehlerfall und einschalten des MosFet
          maxALast = par2.toFloat();
          Serial.print(String(Name) + ",k,:X");
          Serial.flush();
          geantworet = true;
        }
        if (par1 == "l") {// Wird verwendet um die Abweichung der Widerstände am vinRef anzupassen
          vinRef = par2.toFloat();
          Serial.print(String(Name) + ",l,:X");
          Serial.flush();
          geantworet = true;
        }
        if (par1 == "m") {// Wird verwendet um die Abweichung der Widerstände am Vin anzupassen
          vmin = par2.toFloat();
          Serial.print(String(Name) + ",m,:X");
          Serial.flush();
          geantworet = true;
        }
        if (par1 == "n") {// Wird verwendet um die Abweichung der Widerstände am vmax anzupassen
          vmax = par2.toFloat();
          Serial.print(String(Name) + ",n,:X");
          Serial.flush();
          geantworet = true;
        }
        if (par1 == "x") {// Wird verwendet um den Kontroller neu zu starten
          Serial.print(String(Name) + ",x,:X");
          Serial.flush();
          Reset_A();
          geantworet = true;
        }
        if (!geantworet) { //Daten wurden nicht verstanden
          Serial.print(String(Name) + ",o,:X");
          Serial.flush();
          cmdString = "";
        }
        par1 = "";
        par2 = "";
      } else {
        Serial.print(cmdString);// Daten wurden nicht verarbeitet und sind für jemand anders bestimmt
        Serial.flush();
      }
    }else{
      Serial.print(cmdString);
      Serial.flush();
    }
    stringComplete = false;
    cmdString = "";
  }
}
void SetMillisToZero() {
  zeitstempel = millis();
}
unsigned long GetMillis() {
  return (zeitstempel - millis());
}
void serialEvent() {
  gruen = true;
  while (Serial.available()) {
    char inChar = (char)Serial.read();
    cmdString += inChar;
    if (cmdString.indexOf(":X") != -1) {
      stringComplete = true;
    }
  }
}
