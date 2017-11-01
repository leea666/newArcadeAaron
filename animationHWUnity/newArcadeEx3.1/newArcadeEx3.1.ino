//9.27

void setup() {
  Serial.begin(115200);
  pinMode(A2, INPUT_PULLUP);
}

void loop() {
  Serial.print(analogRead(A0));
  Serial.print(",");
  Serial.print(analogRead(A1));
  Serial.print(",");
  Serial.println(digitalRead(A2));
}
