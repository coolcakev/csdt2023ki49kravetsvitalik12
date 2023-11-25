using CSAD;

namespace TestProject1;

public class UnitTest1
{
    private WorkService _workService = new();
    [Fact]
    public void IsCreatePortSuccess()
    {
        // act
        var port = _workService.CreatePort();

        // assert
        Assert.NotNull(port);
        Assert.Same("COM3", port.PortName);
        Assert.Equal(9600, port.BaudRate);
    }

    [Fact]
    public void IsReturnFalse()
    {
        // act
        var isExit = _workService.isExit("");

        // assert
        Assert.False(isExit);
    }
    [Fact]
    public void IsReturnTrue()
    {
        // act
        var isExit = _workService.isExit("exit");

        // assert
        Assert.True(isExit);
    }
    [Fact]
    public void IsSuccessWriteIni()
    {
        // arrange
        var expression = "exit";
        var path = "data.ini";

        // act
        _workService.WriteIni(expression, path);

        // assert
        var allTExt = File.ReadAllText(path);
        Assert.Contains($"Key1={expression}", allTExt);
    }
}