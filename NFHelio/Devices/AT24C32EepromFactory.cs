namespace NFHelio.Devices
{
  class AT24C32EepromFactory : EepromFactory
  {
    public override IEeprom GetEeprom(int address, int i2cBus)
    {
      return new AT24C32(address, i2cBus);
    }
  }
}
