int sensor1 = A0 ;
int sensor2 = A1 ;
int sensor3 = A2 ;

int value1 = 0 ;
int value2 = 0 ;
int value3 = 0 ;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600) ;
}

void loop() {
  // put your main code here, to run repeatedly:
  value1 = analogRead(sensor1) ;
  value2 = analogRead(sensor2) ;
  value3 = analogRead(sensor3) ;

  Serial.print(value1) ;
  Serial.print(",") ;
  Serial.print(value2) ;
  Serial.print(",") ;
  Serial.println(value3) ;
  delay(100) ;
}
