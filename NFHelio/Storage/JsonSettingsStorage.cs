namespace NFHelio.Storage
{
  using nanoFramework.Json;
  using NFCommon.Storage;
  using System;
  using System.Diagnostics;
  using System.Text;

  public class JsonSettingsStorage : ISettingsStorage
  {
    public SettingsBase ReadSettings()
    {
      try
      {
        var eeprom = Program.context.EepromFactory.GetEeprom(Context.EepromAddress, 1);

        ushort initAddress = 0x0;
        byte[] initBytes = eeprom.Read(initAddress, 6);
        if (initBytes[0] != 0x55 || initBytes[1] != 0xAA)
        {
          Debug.WriteLine($"Settings can't be read, init bytes don't match");
          return null;
        }

        var lenght = BitConverter.ToInt32(initBytes, 2);

        ushort settingsAddress = 0x6;
        byte[] settingsBytes = eeprom.Read(settingsAddress, lenght);
        string serializedSetttings = Encoding.UTF8.GetString(settingsBytes, 0, lenght);

        Debug.WriteLine($"{serializedSetttings}");

        return (Settings)JsonConvert.DeserializeObject(serializedSetttings, typeof(Settings));
      }
      catch (OutOfMemoryException ex)
      {
        // This can happen when the settings contain new fields and we can't properly read\save them
        // To get around this, we clear all settings...
        Settings settings = new Settings();
        WriteSettings(settings);
        Debug.WriteLine($"Settings can't be read, resetting them.");

        return null;
      }
      catch
      {
        return null;
      }
    }

    public void WriteSettings(SettingsBase settings)
    {
      var serializedSetttings = JsonConvert.SerializeObject(settings);
      var length = serializedSetttings.Length;

      var eeprom = Program.context.EepromFactory.GetEeprom(Context.EepromAddress, 1);

      ushort initAddress = 0x0;
      var initBytes = new byte[] { 0x55, 0xAA };
      eeprom.Write(initAddress, initBytes);

      ushort lenghtAddress = 0x2;
      byte[] lenghtBytes = BitConverter.GetBytes(length);
      eeprom.Write(lenghtAddress, lenghtBytes);

      ushort settingsAddress = 0x6;
      byte[] settingsBytes = Encoding.UTF8.GetBytes(serializedSetttings);
      eeprom.Write(settingsAddress, settingsBytes);
    }
  }
}
