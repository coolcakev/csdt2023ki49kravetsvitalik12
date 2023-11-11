using System.IO.Ports;
using System.IO;
using System.Security.Cryptography;

var serialPort = CreatePort();
serialPort.Open();


while(true){

    string expression = GetExpression();
    if(expression=="exit"){
        break;
    }

    WriteIni(expression);

    await SendData();
}

serialPort.Close();

/// <summary>
/// Create Port for arduino
/// </summary>
SerialPort CreatePort()
{
    SerialPort serialPort = new SerialPort("COM3", 9600); // Adjust port name and baud rate
    serialPort.DataReceived += (sender, e) =>
    {
        if (sender is SerialPort sender1)
        {
            Console.WriteLine($"sender: {sender1.ReadExisting()}");
            Console.WriteLine($"e: {e.EventType}");
        }

    };
    return serialPort;
}

/// <summary>
/// Get expression from console like (10-5)*5
/// </summary>
string GetExpression()
{
    return Console.ReadLine();
}

/// <summary>
/// write in ini file
/// </summary>
void WriteIni(string expression)
{
    string iniContent = $"Key1={expression}";
    File.WriteAllText("data.ini", iniContent);
}

/// <summary>
/// send our ini file to arduino
/// </summary>
async Task SendData()
{
    using (StreamReader sr = new StreamReader("data.ini"))
    {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            // You can parse or manipulate the line here
            await Task.Delay(2000);
            serialPort.WriteLine(line);  // Sending content line by line

        }
    }
}