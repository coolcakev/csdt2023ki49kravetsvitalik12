using CSAD;

var workSerice=new WorkService();
var serialPort = workSerice.CreatePort();
serialPort.Open();


while(true){

    var expression =Console.ReadLine();
    var isExit = workSerice.isExit(expression);
    if(isExit){
        break;
    }

    workSerice.WriteIni(expression);

    await workSerice.SendData(serialPort);
}

serialPort.Close();