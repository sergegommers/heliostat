namespace NFHelio.Storage
{
  using NFCommon.Storage;
  using NFHelio.Devices;
  using System;
  using System.Collections;
  using System.Diagnostics;

  public class SimpleSettingsStorage : ISettingsStorage
  {
    private readonly IServiceProvider serviceProvider;

    public SimpleSettingsStorage(IServiceProvider serviceProvider)
    {
      this.serviceProvider = serviceProvider;
    }

    public SettingsBase ReadSettings()
    {
      try
      {
        Debug.WriteLine($"Reading settings.");

        var factory = (IEepromFactory)this.serviceProvider.GetService(typeof(IEepromFactory));
        var eeprom = factory.Create();

        ushort initAddress = 0x0;
        byte[] initBytes = eeprom.Read(initAddress, 6);
        if (initBytes[0] != 0xAA || initBytes[1] != 0x55)
        {
          Debug.WriteLine($"Settings can't be read, init bytes don't match");
          return null;
        }

        // get the length of the Settings object
        var lenght = BitConverter.ToInt32(initBytes, 2);

        // and read the byte array of that length
        ushort settingsAddress = 0x6;
        byte[] settingsBytes = eeprom.Read(settingsAddress, lenght);

        Settings settings = new Settings();
        int index = 0;

        // longitude
        settings.Longitude = BitConverter.ToDouble(settingsBytes, index);
        index += sizeof(double);

        // latitude
        settings.Latitude = BitConverter.ToDouble(settingsBytes, index);
        index += sizeof(double);

        // number of entries in the Aci array
        int numEntries = BitConverter.ToInt32(settingsBytes, index);
        index += sizeof(int);

        settings.Aci = new short[numEntries];
        for (int i = 0; i < numEntries; i++)
        {
          // new entry in the array
          settings.Aci[i] = BitConverter.ToInt16(settingsBytes, index);
          index += sizeof(short);
        }

        // number of entries in the Acv array
        numEntries = BitConverter.ToInt32(settingsBytes, index);
        index += sizeof(int);

        settings.Acv = new short[numEntries];
        for (int i = 0; i < numEntries; i++)
        {
          // new entry in the array
          settings.Acv[i] = BitConverter.ToInt16(settingsBytes, index);
          index += sizeof(short);
        }

        // number of entries in the Zci array
        numEntries = BitConverter.ToInt32(settingsBytes, index);
        index += sizeof(int);

        settings.Zci = new short[numEntries];
        for (int i = 0; i < numEntries; i++)
        {
          // new entry in the array
          settings.Zci[i] = BitConverter.ToInt16(settingsBytes, index);
          index += sizeof(short);
        }

        // number of entries in the Zcv array
        numEntries = BitConverter.ToInt32(settingsBytes, index);
        index += sizeof(int);

        settings.Zcv = new short[numEntries];
        for (int i = 0; i < numEntries; i++)
        {
          // new entry in the array
          settings.Zcv[i] = BitConverter.ToInt16(settingsBytes, index);
          index += sizeof(short);
        }

        Debug.WriteLine($"Settings are read.");

        return settings;
      }
      catch
      {
        // This can happen when the settings contain new fields and we can't properly read\save them
        // To get around this, we clear all settings...
        Settings settings = new Settings();
        WriteSettings(settings);

        Debug.WriteLine($"Settings can't be read, resetting them.");

        return null;
      }
    }

    public void WriteSettings(SettingsBase settingsBase)
    {
      Debug.WriteLine($"Saving settings.");

      var factory = (IEepromFactory)this.serviceProvider.GetService(typeof(IEepromFactory));
      var eeprom = factory.Create();

      ushort initAddress = 0x0;
      var initBytes = new byte[] { 0xAA, 0x55 };
      eeprom.Write(initAddress, initBytes);

      var settings = settingsBase as Settings;

      var list = new ArrayList();

      // add longitude
      var bytes = BitConverter.GetBytes(settings.Longitude);
      list.Add(bytes);

      // add latitude
      bytes = BitConverter.GetBytes(settings.Latitude);
      list.Add(bytes);

      // add the number of entries in the Aci array
      bytes = BitConverter.GetBytes(settings.Aci.Length);
      list.Add(bytes);
      foreach (var v in settings.Aci)
      {
        // add the entry
        bytes = BitConverter.GetBytes(v);
        list.Add(bytes);
      }

      // add the number of entries in the Acv array
      bytes = BitConverter.GetBytes(settings.Acv.Length);
      list.Add(bytes);
      foreach (var v in settings.Acv)
      {
        // add the entry
        bytes = BitConverter.GetBytes(v);
        list.Add(bytes);
      }

      // add the number of entries in the Zci array
      bytes = BitConverter.GetBytes(settings.Zci.Length);
      list.Add(bytes);
      foreach (var v in settings.Zci)
      {
        // add the entry
        bytes = BitConverter.GetBytes(v);
        list.Add(bytes);
      }

      // add the number of entries in the Zcv array
      bytes = BitConverter.GetBytes(settings.Zcv.Length);
      list.Add(bytes);
      foreach (var v in settings.Zcv)
      {
        // add the entry
        bytes = BitConverter.GetBytes(v);
        list.Add(bytes);
      }

      // calculate the total length
      int l = 0;
      foreach (var entry in list)
      {
        l += ((byte[])entry).Length;
      }

      // create a byte array of that length
      byte[] bytesToWrite = new byte[l];

      // and copy the individual byte arrays in the arraylist to our flat byte array
      l = 0;
      foreach (var entry in list)
      {
        ((byte[])entry).CopyTo(bytesToWrite, l);
        l += ((byte[])entry).Length;
      }

      // write the length of the array to the eeprom
      ushort lenghtAddress = 0x2;
      byte[] lenghtBytes = BitConverter.GetBytes(l);
      eeprom.Write(lenghtAddress, lenghtBytes);

      // and write the byte array
      ushort settingsAddress = 0x6;
      eeprom.Write(settingsAddress, bytesToWrite);

      Debug.WriteLine($"Settings are saved.");
    }
  }
}
