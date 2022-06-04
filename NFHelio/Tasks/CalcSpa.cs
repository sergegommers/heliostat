namespace NFHelio.Tasks
{
  using NFSpa;
  using System.Diagnostics;

  /// <summary>
  /// Calculates the position of the sun and some other parameters
  /// </summary>
  internal class CalcSpa : ITask
  {
    /// <inheritdoc />
    string ITask.Command => "calcspa";

    /// <inheritdoc />
    string ITask.Description => "Calculates the position of the sun";

    /// <inheritdoc />
    string ITask.Help => "No further info";

    /// <inheritdoc />
    public void Execute(string[] args)
    {
      var realTimeClock = Program.context.RealTimeClockFactory.GetRealTimeClock(Context.RtcAddress, 1);
      var dt = realTimeClock.GetTime();

      Settings settings = null;

      for (int i = 0; i < 5; i++)
      {
        settings = Program.context.SettingsStorageFactory.GetSettingsStorage().ReadSettings() as Settings;

        if (settings != null)
        {
          break;
        }
        else
        {
          Debug.WriteLine($"SettingsStorage.ReadSettings attempt {i} failed");
        }
      }

      if (settings == null)
      {
        Program.context.BluetoothSpp.SendString("Calculation failed, can't read settings\r\n");

        return;
      }

      Spa_data spa = new()
      {
        year = dt.Year,
        month = dt.Month,
        day = dt.Day,
        hour = dt.Hour,
        minute = dt.Minute,
        second = dt.Second,
        timezone = 0.0f,
        delta_ut1 = 0,
        delta_t = 67,
        longitude = settings.Longitude,
        latitude = settings.Latitude,
        elevation = 0,
        pressure = 1013,
        temperature = 11,
        slope = 0,
        azm_rotation = 0,
        atmos_refract = 0.5667f,
        function = (int)Calculator.SpaOutputs.SPA_ZA_RTS
      };

      double min, sec;

      var calculator = new Calculator();
      var result = calculator.Spa_calculate(spa);

      if (result == 0)  // check for SPA errors
      {
        // display the results inside the SPA structure
        Program.context.BluetoothSpp.SendString(string.Format("Epsilon:       {0,12:F6}\n", spa.epsilon));
        Program.context.BluetoothSpp.SendString(string.Format("Zenith:        {0,12:F6}\n", spa.zenith));
        Program.context.BluetoothSpp.SendString(string.Format("Azimuth:       {0,12:F6}\n", spa.azimuth));
        Program.context.BluetoothSpp.SendString(string.Format("Incidence:     {0,12:F6}\n", spa.incidence));

        min = 60.0 * (spa.sunrise - (int)(spa.sunrise));
        sec = 60.0 * (min - (int)min);
        Program.context.BluetoothSpp.SendString(string.Format("Sunrise:       {0,2:D2}:{1,2:D2}:{2,2:D2}\n", (int)(spa.sunrise), (int)min, (int)sec));

        min = 60.0 * (spa.sunset - (int)(spa.sunset));
        sec = 60.0 * (min - (int)min);
        Program.context.BluetoothSpp.SendString(string.Format("Sunset:        {0,2:D2}:{1,2:D2}:{2,2:D2}\n", (int)(spa.sunset), (int)min, (int)sec));
      }
    }
  }
}
