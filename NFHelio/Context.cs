namespace NFHelio
{
  using NFCommon.Storage;
  using NFHelio.Devices;
  using System.Threading;

  public class Context
  {
    public const int EepromAddress = 0x57;
    public const int RtcAddress = 104;

    public const int AzimuthAdcChannel = 0;
    public const int ZenithAdcChannel = 3;
    public const int AdcSampleSize = 100;

    public EepromFactory EepromFactory
    {
      get;
      set;
    }

    public Settings Settings
    {
      get;
      set;
    }

    public RealTimeClockFactory RealTimeClockFactory
    {
      get;
      set;
    }

    public SettingsStorageFactory SettingsStorageFactory
    {
      get;
      set;
    }

    public Thread SunFollowingThread
    {
      get;
      set;
    }
  }
}
