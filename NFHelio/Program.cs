namespace NFHelio
{
  using System.Diagnostics;
  using nanoFramework.DependencyInjection;
  using nanoFramework.Device.Bluetooth.Spp;
  using nanoFramework.Hardware.Esp32;
  using nanoFramework.Hosting;
  using NFCommon.Services;
  using NFHelio.Devices;
  using NFHelio.Services;
  using NFHelio.Storage;

  public static class Program
  {
    public static Context context = new Context();

    public static void Main()
    {
      Debug.WriteLine($"Starting HelioStat");

      IHost host = CreateHostBuilder().Build();

      SetupPins();

      context.EepromFactory = new AT24C32EepromFactory();
      context.SettingsStorageFactory = new SimpleSettingsStorageFactory();
      context.RealTimeClockFactory = new Ds3231RealTimeClockFactory();

      context.Settings = context.SettingsStorageFactory.GetSettingsStorage().ReadSettings() as Settings;
      // if we can't read the settings then we start from scratch
      if (context.Settings == null)
      {
        context.Settings = new Settings();
      }

      Debug.WriteLine($"HelioStat is started, awaiting commands...");

      // starts application and blocks the main calling thread 
      host.Run();
    }

    public static IHostBuilder CreateHostBuilder() =>
      Host.CreateDefaultBuilder()
        .ConfigureServices(services =>
        {
          services.AddSingleton(typeof(IBluetoothSpp), typeof(NordicSpp));
          services.AddSingleton(typeof(IAppMessageWriter), typeof(AppMessageWriter));
          services.AddTransient(typeof(ICommandHandlerService), typeof(CommandHandlerService));
          services.AddHostedService(typeof(BlueToothReceiver));
        });

    private static void SetupPins()
    {
      // i2c pins
      Configuration.SetPinFunction((int)GPIOPort.I2C_Clock, DeviceFunction.I2C1_CLOCK);
      Configuration.SetPinFunction((int)GPIOPort.I2C_Data, DeviceFunction.I2C1_DATA);

      // onboard led
      Configuration.SetPinFunction((int)GPIOPort.ESP32_Onboard_Led, DeviceFunction.PWM16);

      // azimuth adc channel
      int pin = Configuration.GetFunctionPin(DeviceFunction.ADC1_CH0);
      if (pin != (int)GPIOPort.ADC_Azimuth)
      {
        Configuration.SetPinFunction((int)GPIOPort.ADC_Azimuth, DeviceFunction.ADC1_CH0);
      }

      // zenith adc channnel
      pin = Configuration.GetFunctionPin(DeviceFunction.ADC1_CH3);
      if (pin != (int)GPIOPort.ADC_Zenith)
      {
        Configuration.SetPinFunction((int)GPIOPort.ADC_Zenith, DeviceFunction.ADC1_CH3);
      }

      // azimuth motor control
      Configuration.SetPinFunction((int)GPIOPort.PWM_Azimuth_East_to_West, DeviceFunction.PWM1);
      Configuration.SetPinFunction((int)GPIOPort.PWM_Azimuth_West_to_East, DeviceFunction.PWM2);

      // zenith motor control
      Configuration.SetPinFunction((int)GPIOPort.PWM_Zenith_Up, DeviceFunction.PWM3);
      Configuration.SetPinFunction((int)GPIOPort.PWM_Zenith_Down, DeviceFunction.PWM4);
    }
  }
}