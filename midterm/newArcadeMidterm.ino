

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200) ;
  pinMode(6, INPUT_PULLUP) ;
  pinMode(7, INPUT_PULLUP) ;
  pinMode(8, INPUT_PULLUP) ;
  pinMode(9, INPUT_PULLUP) ;
  pinMode(10, INPUT_PULLUP) ;
  pinMode(11, INPUT_PULLUP) ;
  pinMode(12, INPUT_PULLUP) ;
  pinMode(13, INPUT_PULLUP) ;


}

void loop() {
  // put your main code here, to run repeatedly:
  int myData = digitalRead(6) ;
  int myData2 = digitalRead(7) ;
  int myData3 = digitalRead(8) ;
  int myData4 = digitalRead(9) ;
  int myData5 = digitalRead(10) ;
  int myData6 = digitalRead(11) ;
  int myData7 = digitalRead(12) ;
  int myData8 = digitalRead(13) ;

  Serial.print(myData);
  Serial.print(",");
  Serial.print(myData2);
  Serial.print(",");
  Serial.print(myData3);
  Serial.print(",");
  Serial.print(myData4);
  Serial.print(",");
  Serial.print(myData5);
  Serial.print(",");
  Serial.print(myData6);
  Serial.print(",");
  Serial.print(myData7);
  Serial.print(",");
  Serial.println(myData8);
  

}
