using System.IO.Ports;

namespace CSAD
{
    public class WorkService
    {
        /// <summary>
        /// Create Port for arduino
        /// </summary>
       public SerialPort CreatePort()
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
        public bool isExit(string expression)
        {
            return expression == "exit";
        }

        /// <summary>
        /// write in ini file
        /// </summary>
       public void WriteIni(string expression,string path="data.ini")
        {
            var iniContent = $"Key1={expression}";
            File.WriteAllText(path, iniContent);
        }

        /// <summary>
        /// send our ini file to arduino
        /// </summary>
       public async Task SendData(SerialPort serialPort)
        {
            using (StreamReader sr = new StreamReader("data.ini"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // You can parse or manipulate the line here
                    await Task.Delay(2000);
                    serialPort.WriteLine(line);// Sending content line by line

                }
            }
        }
    }
}