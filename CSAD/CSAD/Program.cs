using System.IO.Ports;
using System.IO;
SerialPort serialPort = new SerialPort("COM3", 9600); // Adjust port name and baud rate
serialPort.DataReceived += (sender, e) =>
{
    if (sender is SerialPort sender1)
    {
        Console.WriteLine($"sender: {sender1.ReadExisting()}");
        Console.WriteLine($"e: {e.EventType}");
    }

};
serialPort.Open();


while(true){

    string expression = Console.ReadLine();
    if(expression=="exit"){
        break;
    }
    string iniContent = $"Key1={expression}";
    File.WriteAllText("data.ini", iniContent);


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

serialPort.Close();