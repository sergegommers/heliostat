namespace NFHelio
{
  using NFCommon.Storage;

  public class Settings : SettingsBase
  {
    public Settings()
    {
      this.Aci = new short[0];
      this.Acv = new short[0];
      this.Zci = new short[0];
      this.Zcv = new short[0];
    }

    // Observer longitude (negative west of Greenwich)
    // valid range: -180 to 180 degrees
    public double Longitude
    {
      get;
      set;
    }

    // Observer latitude (negative south of equator)
    // valid range: -90 to 90 degrees
    public double Latitude
    {
      get;
      set;
    }

    public short[] Aci
    {
      get;
      set;
    }

    public short[] Acv
    {
      get;
      set;
    }

    public short[] Zci
    {
      get;
      set;
    }

    public short[] Zcv
    {
      get;
      set;
    }
  }
}
