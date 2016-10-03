const int pin = 13;
int delaytime = 100;

void setup() {
  Serial.begin(9600);
  pinMode(pin, OUTPUT);
}

void loop() {
  if (Serial.available() > 0) {
    int data = (Serial.read());
    switch (data) {
      case 1:
        digitalWrite(pin, HIGH);
        break;

      case 2:
        digitalWrite(pin, LOW);
        break;

      CACE3:
      case 3:
        while (Serial.available() == 0)
          highlow();
        break;

      case 4:
        if (Serial.available() == 2) {
          int high, low;
          high = Serial.read();
          low  = Serial.read();
          high = high << 8;
          delaytime = high + low;
          goto CACE3;
          break;
        }
    }
  }
}

void highlow() {
  digitalWrite(pin, HIGH);
  delay(delaytime);
  digitalWrite(pin, LOW);
  delay(delaytime);
}
