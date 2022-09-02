namespace NFHelio.Services
{
  using nanoFramework.Device.Bluetooth.Spp;
  using NFCommon.Services;

  internal class AppMessageWriter : IAppMessageWriter
  {
    private readonly IBluetoothSpp bluetoothSpp;

    public AppMessageWriter(IBluetoothSpp bluetoothSpp)
    {
      this.bluetoothSpp = bluetoothSpp;
    }

    public void SendString(string message)
    {
      this.bluetoothSpp.SendString(message);
    }
  }
}
