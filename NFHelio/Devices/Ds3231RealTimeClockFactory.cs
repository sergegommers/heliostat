namespace NFHelio.Devices
{
  internal class Ds3231RealTimeClockFactory : RealTimeClockFactory
  {
    public override IRealTimeClock GetRealTimeClock(int address, int i2cBus)
    {
      return new Ds3231RealTimeClock(address, i2cBus);
    }
  }
}
