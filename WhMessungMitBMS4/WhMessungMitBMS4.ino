#define tmp1 A0
#define tmp2 A1
#define tmp3 A2
#define tmp4 A3
#define Vin A4
#define MVin A5


#define gelbLED 3
unsigned long  gruenT = 0;

#define gruenLED 4
bool gruen = false;

#define MosfetPin 9

float vinRef = 5.873; // Wird verwendet um die Abweichung der Widerstände am Vin anzupassen
char Name[3] = "02";//Name vom Gerät

String cmdString; // Wird verwendet um die ankommenden Daten zu verarbeiten
String datenString; // Enthält die ankommenden unveränderten Daten die über Uart eintreffen
String adresse = ""; //Adresse des Controllers
String par1 = ""; //zusäzliche Parameter
String par2 = ""; //zusäzliche Parameter
int int_tmp = 0;
float mvinRef = 11.270;

int sensorValue1 = 0; // Temparatursensoren 1-4
int sensorValue2 = 0;
int sensorValue3 = 0;
int sensorValue4 = 0;

float aref = 1.085;//Interne Referenzspannungsquell
float rLast = 0.24;//Lastwiderstand
float maxALast = 10.0; //maximale Stromlast für den Mosfet
unsigned long zeitstempel;// Zeitstempel für die Synchronisation

float vmin = 2.000; // Minimal zugelassen Spannung.
float vmax = 5.500; //Ab welcher Spannung schalter der Kontroller den Mosfet an und geht auf Störung
bool geantworet = false;
void setup() {
  analogReference(INTERNAL);
  pinMode(MosfetPin, OUTPUT);
  pinMode(gelbLED, OUTPUT);
  pinMode(gruenLED, OUTPUT);
  digitalWrite(gelbLED, LOW);
  digitalWrite(gruenLED, LOW);
  digitalWrite(MosfetPin, LOW);
  delay(100);
  pinMode(tmp1, INPUT);
  pinMode(tmp2, INPUT);
  pinMode(tmp3, INPUT);
  pinMode(tmp4, INPUT);
  pinMode(Vin, INPUT);
  pinMode(MVin, INPUT);
  Serial.begin(19200);
  for (int i = 0; i < 10; i++) {
    analogRead(tmp1);
    analogRead(tmp2);
    analogRead(tmp3);
    analogRead(tmp4);
    analogRead(Vin);
    analogRead(MVin);
  }
  cmdString.reserve(160);
  Serial.setTimeout(40);
  zeitstempel = millis();
}

////////////

void setMosfetOn() {
  digitalWrite(MosfetPin, HIGH);
}

void setMosfetOff() {
  digitalWrite(MosfetPin, LOW);
}

float getalast() {
  return (getVin() - getMvin()) / rLast; //Ermittlung des Spannungsabfalls am Lastwiderstand
}

float getVin() {
  return analogRead(Vin) * (aref / 1023.0) * vinRef;//Ermittlung der Gesamtstammung
}
float getMvin() {
  return analogRead(MVin) * (aref / 1023.0) * mvinRef; //Ermittlung des Spannungsabfalls am Mosfet
}

void loop() {
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
    Serial.print(String(Name) + ",Fehler:Alast:" + String(getalast(), 3) + ":Fehler:X");
    Serial.flush();
  }
  if (getVin() > vmax) {// Zu hohe Spannung am Akku. Der Mosfet wird eingeschaltet
    Serial.print(String(Name) + ",Fehler:vmax:" + String(getVin(), 3) + ":Fehler:X");
    Serial.flush();
    setMosfetOn();
  }
  if (getVin() < vmin) {// Die Spannung ist am Akku zu niedrig. Schaltet den Mosfet aus
    Serial.print(String(Name) + ",Fehler:vmin:" + String(getVin(), 3) + ":Fehler:X");
    Serial.flush();
    setMosfetOff();
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
  cmdString="";
  cmdString  = Serial.readString();
  datenString=cmdString;
  if (datenString.substring(datenString.length() - 2, datenString.length()) == ":X") { // Überprüft ob das Kommando vollständig empfangen wurde
    int_tmp = cmdString.indexOf(",");
    adresse = cmdString.substring(0, int_tmp); // Speichern des Adresse
    if (adresse == Name) {// Gilt die Kommunikation für dieser Kontroller
      cmdString = cmdString.substring(int_tmp + 1);
      int_tmp = cmdString.indexOf(",");
      par1 = cmdString.substring(0, int_tmp); // Speichern der zusäzlichen Parameter
      par2 = cmdString.substring(int_tmp + 1);
      if (par1 == "on") {
        Serial.print(String(Name) + ",on:X");
        Serial.flush();
        setMosfetOn();
        geantworet = true;
      }
      if (par1 == "off") {
        Serial.print(String(Name) + ",off:X");
        Serial.flush();
        setMosfetOff();
        geantworet = true;
      }
      if (par1 == "WhMessung") {
        Serial.print(String(Name) + "," + String(getalast(), 3) + ":" + String(getVin(), 3) + ":" + String(GetMillis()) + ":X");
        Serial.flush();
        geantworet = true;
      }
      if (par1 == "status") {
        Serial.print(String(Name) + "," + analogRead(tmp1) + ":" + analogRead(tmp2) + ":" + analogRead(tmp3) + ":" + analogRead(tmp4) + ":" + String(getalast(), 3) + ":" + String(getVin(), 3) + ":" + String(GetMillis()) + ":X");
        Serial.flush();
        geantworet = true;
      }
      if (par1 == "para") {//gibt alle Parameter aus
        Serial.print(String(Name) + "," + String(mvinRef, 3) + ":" + String(aref, 3) + ":" + String(rLast, 3) + ":" + String(maxALast, 3) + ":" + String(vinRef, 3) + ":" + String(vmin, 3) + ":" + String(vmax, 3) + ":X");
        Serial.flush();
        geantworet = true;
      }
      if (par1 == "sync") {// Synchronisiert die Zeiten an den Kontrollern
        SetMillisToZero();
        Serial.flush();
        geantworet = true;
      }
      if (par1 == "mvinRef") {// Wird verwendet um den Spannungsteiler am Mosfet anzupassen
        mvinRef = par2.toFloat();
        Serial.flush();
        geantworet = true;
      }
      if (par1 == "aRef") {// Wird verwendet um die interne Refernzespannung zu ändern über Uart zu setzen
        aref = par2.toFloat();
        Serial.flush();
        geantworet = true;
      }
      if (par1 == "rLast") {// Wird verwendet um die rLast über Uart zu setzen
        rLast = par2.toFloat();
        Serial.flush();
        geantworet = true;
      }
      if (par1 == "maxALast") {// maximalen Storm am rLast für zu Fehlerfall und einschalten des MosFet
        maxALast = par2.toFloat();
        Serial.flush();
        geantworet = true;
      }
      if (par1 == "vinRef") {// Wird verwendet um die Abweichung der Widerstände am Vin anzupassen
        vinRef = par2.toFloat();
        Serial.flush();
        geantworet = true;
      }
      if (par1 == "vmin") {// Wird verwendet um die Abweichung der Widerstände am Vin anzupassen
        vmin = par2.toFloat();
        Serial.flush();
        geantworet = true;
      }
      if (par1 == "vmax") {// Wird verwendet um die Abweichung der Widerstände am Vin anzupassen
        vmax = par2.toFloat();
        Serial.flush();
        geantworet = true;
      }

      par1 = "";
    }
  }

  if (!geantworet) {// Daten wurden nicht verarbeitet und sind für jemand anders bestimmt
    Serial.print(datenString);
    Serial.flush();
  }
  geantworet = false;
}
