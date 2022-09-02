﻿namespace NFHelio.Tasks
{
  using NFCommon.Services;
  using NFSpa;
  using System.Diagnostics;

  /// <summary>
  /// Calculates the position of the sun and some other parameters
  /// </summary>
  internal class CalcSpa : ITask
  {
    private readonly IAppMessageWriter appMessageWriter;

    /// <inheritdoc />
    string ITask.Command => "calcspa";

    /// <inheritdoc />
    string ITask.Description => "Calculates the position of the sun";

    /// <inheritdoc />
    string ITask.Help => "No further info";

    /// <summary>
    /// Initializes a new instance of the <see cref="CalcSpa"/> class.
    /// </summary>
    /// <param name="appMessageWriter">The application message writer.</param>
    public CalcSpa(IAppMessageWriter appMessageWriter)
    {
      this.appMessageWriter = appMessageWriter;
    }

    /// <inheritdoc />
    public void Execute(string[] args)
    {
      var realTimeClock = Program.context.RealTimeClockFactory.GetRealTimeClock(Context.RtcAddress, 1);
      var dt = realTimeClock.GetTime();

      var settings = Program.context.SettingsStorageFactory.GetSettingsStorage().ReadSettings() as Settings;
      if (settings == null)
      {
        this.appMessageWriter.SendString("Calculation failed, can't read settings\n");

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
        longitude = settings.Longitude,
        latitude = settings.Latitude,
        elevation = 0,
        function = (int)Calculator.SpaOutputs.SPA_ZA_RTS
      };

      double min, sec;

      var calculator = new Calculator();
      var result = calculator.Spa_calculate(spa);

      // check for SPA errors
      if (result == 0)
      {
        // display the results inside the SPA structure
        this.appMessageWriter.SendString(string.Format("Azimuth:       {0,12:F6}\n", spa.azimuth));
        this.appMessageWriter.SendString(string.Format("Zenith:        {0,12:F6}\n", spa.zenith));

        min = 60.0 * (spa.sunrise - (int)(spa.sunrise));
        sec = 60.0 * (min - (int)min);
        this.appMessageWriter.SendString(string.Format("Sunrise:       {0,2:D2}:{1,2:D2}:{2,2:D2}\n", (int)(spa.sunrise), (int)min, (int)sec));

        min = 60.0 * (spa.sunset - (int)(spa.sunset));
        sec = 60.0 * (min - (int)min);
        this.appMessageWriter.SendString(string.Format("Sunset:        {0,2:D2}:{1,2:D2}:{2,2:D2}\n", (int)(spa.sunset), (int)min, (int)sec));
      }
      else
      {
        Debug.WriteLine($"Calculating the sun's position failed with error code {result}");
      }
    }
  }
}
