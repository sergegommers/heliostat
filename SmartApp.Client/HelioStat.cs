namespace SmartApp.Client
{
  using Plugin.BLE.Abstractions.Contracts;
  using System;
  using Xamarin.Essentials;
  using System.Threading.Tasks;

  public class HelioStat: SerialDevice
  {
    public HelioStat(ICharacteristic sendCharacteristic, ICharacteristic receiveCharacteristic)
      : base(sendCharacteristic, receiveCharacteristic)
    {
    }

    public async Task<string> MoveMotorAsync(char plane, double speed)
    {
      try
      {
        await this.WriteStringAsync($"movemotor {plane} {speed}");
        return null;
      }
      catch (Exception ex)
      {
        return "Error sending command to HelioStat: " + ex.Message;
      }
    }



    public async Task<string> SetTimeAndLocationAsync()
    {
      await WaitForSilenceAsync(TimeSpan.FromSeconds(2));

      var location = await Geolocation.GetLastKnownLocationAsync();
      if (location == null)
      {
        return "Could not read location";
      }
      if (location.Accuracy > 10000)
      {
        return "Location accuracy is bigger then 10 kilometers. Not setting the location";
      }

      try
      {
        await this.WriteStringAsync($"setpos {location.Latitude} {location.Longitude}");

        var matches = await WaitForRegExAsync("[-+]?[0-9]*\\.?[0-9]+", 2, TimeSpan.FromSeconds(2));
      }
      catch (Exception ex)
      {
        return "Error sending command to HelioStat: " +ex.Message;
      }

      return null;
    }
  }
}
