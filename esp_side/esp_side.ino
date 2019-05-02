#include <string.h>
#include <MPU9250.h>

#include <ESP8266WiFi.h>          //https://github.com/esp8266/Arduino
#include <DNSServer.h>
#include <ESP8266WebServer.h>
#include <WiFiManager.h>          //https://github.com/tzapu/WiFiManager
#include <WiFiClient.h>

MPU9250 IMU(Wire,0x68);
int status;
WiFiServer server(26);
WiFiClient client;


String message_na;


//gets called when WiFiManager enters configuration mode
void configModeCallback (WiFiManager *myWiFiManager) {
  //Serial.println("Entered config mode");
  //Serial.println(WiFi.softAPIP());
  //if you used auto generated SSID, print it
  //Serial.println(myWiFiManager->getConfigPortalSSID());
  //entered config mode, make led toggle faster
}

void setup(void) {
  //Serial.begin(115200);
  //pinMode(BUILTIN_LED, OUTPUT);
  pinMode(3, INPUT_PULLUP); 
  
    //WiFiManager
  //Local intialization. Once its business is done, there is no need to keep it around
  WiFiManager wifiManager;
  //reset settings - for testing
  //wifiManager.resetSettings();

  //set callback that gets called when connecting to previous WiFi fails, and enters Access Point mode
  wifiManager.setAPCallback(configModeCallback);

  if (!wifiManager.autoConnect(">>I'm HERE!!<<", "hahahahaha")) {
    //Serial.println("failed to connect and hit timeout");
    //reset and try again, or maybe put it to deep sleep
    ESP.reset();
    delay(1000);
  }

  //if you get here you have connected to the WiFi
  //Serial.println("connected...yeey :)");
  server.begin();
  //keep LED off
  //digitalWrite(BUILTIN_LED, HIGH);

  Wire.begin();
  // start communication with IMU 
  status = IMU.begin();
  if (status < 0) {
    //Serial.println("Couldnt start1");
    while(1);
  }
  //Serial.println("MPU9250 found!");
  

}

void loop() {
  client = server.available();
  if (client){
    Serial.println("Client connected");
    while (client.connected()){
      IMU.readSensor();
      /* Display the results (acceleration is measured in m/s^2) */
      // Send the distance to the client, along with a break to separate our messages
      message_na = String("");
      message_na += String(IMU.getAccelX_mss());
      message_na += String(",");
      message_na += String(IMU.getAccelY_mss());
      message_na += String(",");
      message_na += String(IMU.getAccelZ_mss());
      message_na += String(",");
      message_na += String(IMU.getGyroX_rads());
      message_na += String(",");
      message_na += String(IMU.getGyroY_rads());
      message_na += String(",");
      message_na += String(IMU.getGyroZ_rads());
      message_na += String(",");
      message_na += String(IMU.getMagX_uT());
      message_na += String(",");
      message_na += String(IMU.getMagY_uT());
      message_na += String(",");
      message_na += String(IMU.getMagZ_uT());
      message_na += String(",");
      message_na += String(IMU.getTemperature_C());
      message_na += String(",");
      message_na += String(digitalRead(3));
      client.print(message_na);
      client.print("\r");
      
      // Delay before the next reading
      delay(10);
    }
  }
}
