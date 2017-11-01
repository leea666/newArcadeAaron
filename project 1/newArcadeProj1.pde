import processing.serial.*;

Serial myPort1;
Serial myPort2 ;
int[] player1Sensor = {0, 0, 0};
int player1Points ;

int[] player2Sensor = {0, 0, 0};
int player2Points ;

long time = 0 ;
int timeLeft = 63000 ;
int remainingTime ;

void setup() {
  size(500, 500) ;
  println(myPort1.list()) ;
  myPort1 = new Serial(this, myPort1.list()[1], 9600) ;
  myPort2 = new Serial(this, myPort2.list()[2], 9600) ;
}

void draw() {
  background(255) ;
  fill(0) ;
  textSize(32) ;
  text("Time Remaining: " + remainingTime/1000 + " seconds", 10, 50) ;
  text("Player 1 Points: " + player1Points, 10, 150) ;
  text("Player 2 Points: " + player2Points, 10, 300) ;
  time = millis() ;
  remainingTime = timeLeft - millis() ;
  if(remainingTime <= 0) {
    remainingTime = 0 ;
  } 
  
  if(remainingTime == 0) {
     if(player1Points > player2Points) {
       fill(255,0,0) ;
       text("Player 1 Wins!", width/2, 400) ;
     } else if (player2Points > player1Points) {
       fill(0,255,0) ;
       text("Player 2 Wins!", width/2, 400) ;
     } else if (player2Points == player1Points) {
       fill(0,0,255) ;
       text("TIE!", width/2, 400) ; 
     }
  }
  if (remainingTime > 0) {
    if (player1Sensor[0] > 300 && player1Sensor[0] < 500) {
      println("this is running") ;
      player1Points = player1Points + 1 ;
    } else if (player1Sensor[0] >= 500) {
      player1Points = player1Points + 2 ;
    }

    if (player1Sensor[1] > 300 && player1Sensor[1] < 500) {
      println("this is running") ;
      player1Points = player1Points + 1 ;
    } else if (player1Sensor[1] >= 500) {
      player1Points = player1Points + 2 ;
    } 

    if (player1Sensor[2] > 300 && player1Sensor[2] < 500) {
      println("this is running") ;
      player1Points = player1Points + 1 ;
    } else if (player1Sensor[2] >= 500 && player1Sensor[2] < 700) {
      player1Points = player1Points + 2 ;
    } else if (player1Sensor[2] >= 700) {
      player1Points = player1Points + 3 ;
    }
    println(player1Points) ;
    println(player1Sensor[2]) ;


    if(player2Sensor[0] > 300 && player2Sensor[0] < 500) {
      println("this is running") ;
       player2Points = player2Points + 1 ; 
    } else if (player2Sensor[0] >= 500) {
       player2Points = player2Points + 2 ; 
    }

    if(player2Sensor[1] > 300 && player2Sensor[1] < 500) {
      println("this is running") ;
      player2Points = player2Points + 1 ; 
    } else if (player2Sensor[1] >= 500) {
      player2Points = player2Points + 2 ; 
    } 

    if(player2Sensor[2] > 300 && player2Sensor[2] < 500) {
      println("this is running") ;
       player2Points = player2Points + 1 ; 
    } else if (player2Sensor[2] >= 500 && player2Sensor[2] < 700) {
       player2Points = player2Points + 2 ; 
    } else if (player2Sensor[2] >= 700) {
       player2Points = player2Points + 3 ; 
    }
  }
}

void serialEvent(Serial myPort) {
  if (myPort == myPort1) {
    String inString = myPort1.readStringUntil('\n');
    inString = trim(inString);
    if (inString != null) {
      String[] parsedSerial = split(inString, ',');
      for (int x = 0; x < 3; x=x+1) {
        player1Sensor[x] = parseInt(parsedSerial[x]);
      }
    }
  } else if (myPort == myPort2) {
    String inString = myPort2.readStringUntil('\n');
    inString = trim(inString);
    if (inString != null) {
      String[] parsedSerial = split(inString, ',');
      for (int x = 0; x < 3; x=x+1) {
        player2Sensor[x] = parseInt(parsedSerial[x]);
      }
    }
  }
}