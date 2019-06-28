#include <SerialCommands.h>

const unsigned long BAUD_RATE = 115200;
const int NUM_BUTTONS = 5;
const uint8_t MAX_SERIAL_BUFFER_LENGTH = 24;
const unsigned int SMOOTHING_DURATION_IN_CYCLES = 255;

struct Button {
	byte pin;
	byte id;
	bool inverted;
	bool isDown;
	unsigned int smoothTime;
};

Button buttons[] = {
	{.pin = 9, .id = 0, .inverted = true, .isDown = false, .smoothTime = 0 },
	{.pin = 8, .id = 1, .inverted = true, .isDown = false, .smoothTime = 0 },
	{.pin = 10, .id = 2, .inverted = true, .isDown = false, .smoothTime = 0 },
	{.pin = 11, .id = 3, .inverted = true, .isDown = false, .smoothTime = 0 },
	{.pin = 4, .id = 4, .inverted = true, .isDown = false, .smoothTime = 0 }
};

char _serial_command_buffer[MAX_SERIAL_BUFFER_LENGTH];
SerialCommands serial_commands(&Serial, _serial_command_buffer, sizeof(_serial_command_buffer), "\n", " ");

void CheckButton(Button& thisButton) {
	if( thisButton.smoothTime > 0 ) {
		thisButton.smoothTime -= 1;
	} else if( digitalRead(thisButton.pin) ) {
			if(thisButton.inverted) {
				if(thisButton.isDown) {
					ButtonUp(thisButton);
				}
			} else {
				if(!thisButton.isDown) {
					ButtonDown(thisButton);
				}
			}
		} else {
			if(thisButton.inverted) {
				if(!thisButton.isDown) {
					ButtonDown(thisButton);
				}
			} else {
				if(thisButton.isDown) {
					ButtonUp(thisButton);
				}
			}
		}
}

void ButtonUp(Button& thisButton) {
	thisButton.isDown = false;
	// Serial.println("Bu " + theButton.id);
	thisButton.smoothTime = SMOOTHING_DURATION_IN_CYCLES;
	Serial.println("Bu " + String(thisButton.id));
}

void ButtonDown(Button& thisButton) {
	thisButton.isDown = true;
	// Serial.println("Bd " + theButton.id);
	thisButton.smoothTime = SMOOTHING_DURATION_IN_CYCLES;
	Serial.println("Bd " + String(thisButton.id));
}

void setup() {
	Serial.begin(BAUD_RATE);
	for (const Button& thisButton : buttons) {
		pinMode(thisButton.pin, INPUT_PULLUP);
	}
}

void loop() {
	serial_commands.ReadSerial();
	for (Button& thisButton : buttons) {
		CheckButton(thisButton);
	}
}