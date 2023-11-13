#include <Arduino.h>

String receivedLine = "";

///
/// funuction to setup the arduino
///
void setup() {
    Serial.begin(9600);
}

///
/// funuction to loop the arduino. It will read the serial port and parse the line
///
void loop() {
    if (Serial.available() > 0) {
        char receivedChar = Serial.read();

        if (receivedChar == '\n') {
            // End of line received, process the line
            parseLine(receivedLine);
            receivedLine = "";  // Reset for the next line
        } else {
            receivedLine += receivedChar;
        }
    }
}

///
/// funuction to parse a line from the ini file and get value and key
///
void parseLine(String line) {
    // Assuming the format is key=value
    int equalPos = line.indexOf('=');
    if (equalPos >= 0) {
        String key = line.substring(0, equalPos);
        String value = line.substring(equalPos + 1);

        // Process the key-value pairs
        if(key=="Key1"){
          char buffer[value.length() + 1];
        value.toCharArray(buffer, sizeof(buffer));
    double result = evaluateExpression(buffer);
    Serial.print("Result: ");
        Serial.println(result);

        }
        }
}

///
/// funuction that takes the operator and return priority
///
int precedence(char op) {
    if (op == '+' || op == '-') {
        return 1;
    } else if (op == '*' || op == '/') {
        return 2;
    }
    return 0;
}

///
/// funuction to apply the operation between two numbers
///
double applyOperation(double a, double b, char op) {
    switch (op) {
        case '+': return a + b;
        case '-': return a - b;
        case '*': return a * b;
        case '/': return a / b;
        default: return 0;
    }
}

///
/// funuction to evaluate the expression and put the value in the stack
///
double evaluateExpression(const char* expression) {
    double values[100];
    char ops[100];
    int valIndex = -1;
    int opIndex = -1;

    for (size_t i = 0; expression[i] != '\0'; ++i) {
        if (expression[i] == ' ')
            continue;

        if (isdigit(expression[i])) {
            char num[10];
            int j = 0;
            while (isdigit(expression[i]) || expression[i] == '.') {
                num[j++] = expression[i++];
            }
            num[j] = '\0';
            values[++valIndex] = atof(num);
            i--; // Move the index back one step as it's incremented in the loop
        } else if (expression[i] == '(') {
            ops[++opIndex] = expression[i];
        } else if (expression[i] == ')') {
            while (opIndex >= 0 && ops[opIndex] != '(') {
                double b = values[valIndex--];
                double a = values[valIndex--];
                char op = ops[opIndex--];
                values[++valIndex] = applyOperation(a, b, op);
            }
            opIndex--; // Remove the '(' from the stack
        } else if (expression[i] == '+' || expression[i] == '-' || expression[i] == '*' || expression[i] == '/') {
            while (opIndex >= 0 && precedence(ops[opIndex]) >= precedence(expression[i])) {
                double b = values[valIndex--];
                double a = values[valIndex--];
                char op = ops[opIndex--];
                values[++valIndex] = applyOperation(a, b, op);
            }
            ops[++opIndex] = expression[i];
        }
    }

    while (opIndex >= 0) {
        double b = values[valIndex--];
        double a = values[valIndex--];
        char op = ops[opIndex--];
        values[++valIndex] = applyOperation(a, b, op);
    }

    return values[valIndex];
}