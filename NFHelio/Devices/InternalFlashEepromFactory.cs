namespace NFHelio.Devices
{
  public class InternalFlashEepromFactory : IEepromFactory
  {
    public IEeprom Create()
    {
      return new InternalFlash();
    }
  }
}
