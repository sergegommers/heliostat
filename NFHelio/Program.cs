namespace NFHelio
{
  using System;
  using System.Diagnostics;
  using System.Threading;
  using nanoFramework.Device.Bluetooth.Spp;
  using nanoFramework.Hardware.Esp32;
  using NFHelio.Devices;
  using NFHelio.Storage;

  public static class Program
  {
    public static Context context = new Context();

    public static void Main()
    {
      Debug.WriteLine($"Starting NFHelio");

      SetupPins();

      context.BluetoothSpp = SetUpBlueTooth();
      context.EepromFactory = new AT24C32EepromFactory();
      context.SettingsStorageFactory = new SimpleSettingsStorageFactory();
      context.RealTimeClockFactory = new Ds3231RealTimeClockFactory();

      context.Settings = context.SettingsStorageFactory.GetSettingsStorage().ReadSettings() as Settings;
      // if we can't read the settings then we start from scratch
      if (context.Settings == null)
      {
        context.Settings = new Settings();
      }

      Debug.WriteLine($"NFHelio is started, awaiting commands...");

      while (true)
      {
        Thread.Sleep(10000);
      }
    }

    private static void SetupPins()
    {
      Configuration.SetPinFunction((int)GPIOPort.I2C_Clock, DeviceFunction.I2C1_CLOCK);
      Configuration.SetPinFunction((int)GPIOPort.I2C_Data, DeviceFunction.I2C1_DATA);

      // azimuth adc channel
      Configuration.SetPinFunction((int)GPIOPort.ADC_Azimuth, DeviceFunction.ADC1_CH0);

      // zenith adc channnel
      Configuration.SetPinFunction((int)GPIOPort.ADC_Zenith, DeviceFunction.ADC1_CH3);

      // azimuth motor control
      Configuration.SetPinFunction((int)GPIOPort.PWM_Azimuth_East_to_West, DeviceFunction.PWM1);
      Configuration.SetPinFunction((int)GPIOPort.PWM_Azimuth_West_to_East, DeviceFunction.PWM2);

      // zenith motor control
      Configuration.SetPinFunction((int)GPIOPort.PWM_Zenith_Up, DeviceFunction.PWM3);
      Configuration.SetPinFunction((int)GPIOPort.PWM_Zenith_Down, DeviceFunction.PWM4);
    }

    private static IBluetoothSpp SetUpBlueTooth()
    {
      // Create Instance of Bluetooth Serial profile
      var bluetoothSpp = new NordicSpp();

      // Add event handles for received data and Connections 
      bluetoothSpp.ReceivedData += Spp_ReceivedData;
      bluetoothSpp.ConnectedEvent += Spp_ConnectedEvent;

      // Start Advertising SPP service
      bluetoothSpp.Start("SunTracker");

      return bluetoothSpp;
    }

    private static void Spp_ConnectedEvent(IBluetoothSpp sender, EventArgs e)
    {
      if (context.BluetoothSpp.IsConnected)
      {
        context.BluetoothSpp.SendString($"Welcome to SunTracker\n");
        context.BluetoothSpp.SendString($"Send 'help' for options\n");
      }

      Debug.WriteLine($"BlueTooth client connected:{sender.IsConnected}");
    }

    private static void Spp_ReceivedData(IBluetoothSpp sender, SppReceivedDataEventArgs receivedDataEventArgs)
    {
      var commandHandler = new CommandHandler();
      commandHandler.HandleMessage(receivedDataEventArgs.DataString);
    }
  }
}