void setup() {
  Serial.begin(9600);
}

void loop() {
  if (Serial.available()) {
    String number1 = Serial.readStringUntil('\n');
    String number2 = Serial.readStringUntil('\n');
    String operation = Serial.readStringUntil('\n');                           
    double result = calculate(number1.toDouble(), number2.toDouble(), operation);
    Serial.println(result);

    Serial.println();
  }
}

double calculate(double num1, double num2, String operation) {
  if (operation == "+") {
    return num1 + num2;
  }
  if (operation == "-") {
    return num1 - num2;
  }
  if (operation == "*") {
    return num1 * num2;
  }
  if (operation == "/" && num2 != 0) {
    return num1 / num2;
  }

  return 0.0;
}