static byte READY  = 'R';
static char DATA[] = "Hello World!\n";

void setup() {
  Serial.begin(9600);
}

void loop() {
  // Wait until handshake init received to send data
  while (Serial.read() != READY);
  Serial.print(DATA);
}
