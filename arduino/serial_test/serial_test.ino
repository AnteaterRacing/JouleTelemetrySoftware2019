#define BAUD_RATE 9600

#define SYN1      '_'
#define SYN0      'c'
#define SYN_SIZE  2

#define DAT_SIZE  88

#define PKT_SIZE  SYN_SIZE + DAT_SIZE

typedef struct packet_t
{
  byte header[SYN_SIZE] = {SYN1, SYN0};
  byte data[DAT_SIZE];
} packet;

static packet pkt;

void setup() {
  Serial.begin(BAUD_RATE);
  // Seed random generator
  randomSeed(analogRead(0));
}

// Fill byte array with data from another
void fill(byte *dst, byte *src, unsigned int dst_offset, unsigned int count)
{
  for (unsigned int i = 0; i < count; i++) {
    dst[dst_offset+i] = src[i];
  }
}

// Fill byte array with random bytes
void fill_random(byte *dst, unsigned int dst_offset, unsigned int count) {
  for (unsigned int i = 0; i < count; i++) {
    dst[dst_offset+i] = random(0, 2);
  }
}

// Continously write random data packets
void loop() {
  //while (Serial.read() != SYN1);
//  Serial.write('K');
  //if (Serial.read() == SYN0)
  //{
    fill_random(pkt.data, SYN_SIZE, PKT_SIZE);
    byte pkt_write[PKT_SIZE];
    fill(pkt_write, pkt.header, 0, SYN_SIZE);
    fill(pkt_write, pkt.data, 0, DAT_SIZE);
    Serial.write(pkt_write, PKT_SIZE);
    //Serial.write('\n');
  //}
}
