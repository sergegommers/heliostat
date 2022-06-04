namespace NFHelio.Devices
{
  public abstract class RealTimeClockFactory
  {
    public abstract IRealTimeClock GetRealTimeClock(int address, int i2cBus);
  }
}
