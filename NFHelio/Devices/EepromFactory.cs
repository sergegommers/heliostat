namespace NFHelio.Devices
{
  public abstract class EepromFactory
  {
    /// <summary>
    /// Gets the eeprom.
    /// </summary>
    /// <param name="address">The address.</param>
    /// <param name="i2cBus">The i2c bus.</param>
    /// <returns></returns>
    public abstract IEeprom GetEeprom(int address, int i2cBus);
  }
}
