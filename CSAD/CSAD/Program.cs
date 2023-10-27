using System.IO.Ports;
var serialPort = new SerialPort("COM3", 9600);
serialPort.DataReceived += (sender, e) =>
{
    if (sender is SerialPort sender1)
    {
        Console.WriteLine($"Result: {sender1.ReadExisting()}");
    }
};

try
{
    serialPort.Open();
    Console.WriteLine("Порт відкрито. Введіть два числа і операцію (+, -, *, /) через пробіл (наприклад, 5 10 +):");

    while (true)
    {
        string input = Console.ReadLine();

        if (input.ToLower() == "exit")
        {
            break;
        }

        string[] parts = input.Split(' ');

        if (parts.Length != 3)
        {
            Console.WriteLine("Не вірний формат. Повторіть спробу.");
            continue;
        }

        if (!double.TryParse(parts[0], out double number1) || !double.TryParse(parts[1], out double number2))
        {
            Console.WriteLine("Некоректні числа. Повторіть спробу.");
            continue;
        }

        string operation = parts[2];

        if (operation != "+" && operation != "-" && operation != "*" && operation != "/")
        {
            Console.WriteLine("Некоректна операція. Повторіть спробу.");
            continue;
        }

        // Очищуємо буфер прийому перед надсиланням запиту
        serialPort.DiscardInBuffer();

        serialPort.WriteLine(number1.ToString());
        serialPort.WriteLine(number2.ToString());
        serialPort.WriteLine(operation);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Помилка: {ex.Message}");
}
finally
{
    serialPort.Close();
}