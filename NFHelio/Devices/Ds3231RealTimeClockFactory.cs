namespace NFHelio.Devices
{
  public class Ds3231RealTimeClockFactory : IRealTimeClockFactory
  {
    public IRealTimeClock Create()
    {
      return new Ds3231RealTimeClock(Context.RtcAddress, 1);
    }
  }
}
