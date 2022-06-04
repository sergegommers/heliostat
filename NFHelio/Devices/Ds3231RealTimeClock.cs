namespace NFHelio.Devices
{
  using Iot.Device.Rtc;
  using System;
  using System.Device.I2c;

  public class Ds3231RealTimeClock : IRealTimeClock
  {
    private readonly Ds3231 ds3231;

    public Ds3231RealTimeClock(int address, int i2cBus)
    {
      var settings = new I2cConnectionSettings(i2cBus, address);
      var device = I2cDevice.Create(settings);

      ds3231 = new Ds3231(device);
    }

    /// <inheritdoc />
    public void SetTime(int year, int month, int day, int hour, int minute, int second)
    {
      var dt = new DateTime(year, month, day, hour, minute, second);

      ds3231.DateTime = dt;
    }

    /// <inheritdoc />
    public DateTime GetTime()
    {
      var dt = ds3231.DateTime;

      return dt;
    }
  }
}
