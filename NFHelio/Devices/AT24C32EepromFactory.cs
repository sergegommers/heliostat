namespace NFHelio.Devices
{
  internal class AT24C32EepromFactory : IEepromFactory
  {
    public IEeprom Create()
    {
      return new AT24C32(Context.EepromAddress, 1);
    }
  }
}
