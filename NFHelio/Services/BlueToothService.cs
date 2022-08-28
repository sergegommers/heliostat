namespace NFHelio.Services
{
  using nanoFramework.Device.Bluetooth.Spp;
  using nanoFramework.Hosting;
  using System;
  using System.Diagnostics;
  using System.Threading;

  internal class BlueToothService : BackgroundService
  {
    private NordicSpp bluetoothSpp;

    private readonly ICommandHandlerService commandHandlerService;

    public BlueToothService(ICommandHandlerService commandHandlerService)
    {
      this.commandHandlerService = commandHandlerService;

      Program.context.BluetoothSpp = bluetoothSpp;
    }

    protected override void ExecuteAsync()
    {
      bluetoothSpp = new NordicSpp();

      // Add event handles for received data and Connections 
      bluetoothSpp.ReceivedData += Spp_ReceivedData;
      bluetoothSpp.ConnectedEvent += Spp_ConnectedEvent;

      // Start Advertising SPP service
      bluetoothSpp.Start("HelioStat");

      while (!CancellationRequested)
      {
        Thread.Sleep(1000);
      }

      bluetoothSpp.Stop();
    }

    private void Spp_ConnectedEvent(IBluetoothSpp sender, EventArgs e)
    {
      if (bluetoothSpp.IsConnected)
      {
        bluetoothSpp.SendString($"Welcome to HelioStat\n");
        bluetoothSpp.SendString($"Send 'help' for options\n");
      }

      Debug.WriteLine($"BlueTooth client connected:{sender.IsConnected}");
    }

    private void Spp_ReceivedData(IBluetoothSpp sender, SppReceivedDataEventArgs receivedDataEventArgs)
    {
      this.commandHandlerService.HandleMessage(receivedDataEventArgs.DataString);
    }
  }
}
