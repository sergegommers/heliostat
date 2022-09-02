namespace NFHelio.Tasks
{
  using NFCommon.Services;
  using NFSpa;
  using System;
  using System.Diagnostics;
  using System.Threading;

  /// <summary>
  /// Follows the sun across the sky
  /// </summary>
  internal class FollowSun : ITask
  {
    private readonly IServiceProvider provider;
    private readonly IAppMessageWriter appMessageWriter;

    /// <inheritdoc />
    public string Command => "followsun";

    /// <inheritdoc />
    public string Description => "Follows the sun";

    /// <inheritdoc />
    public string Help => "follow <action>\nwhere action is start or stop";

    public FollowSun(IServiceProvider provider, IAppMessageWriter appMessageWriter)
    {
      this.provider = provider;
      this.appMessageWriter = appMessageWriter;
    }

    /// <inheritdoc />
    public void Execute(string[] args)
    {
      if (args.Length != 1)
      {
        this.appMessageWriter.SendString("Invalid number of arguments\n");
        return;
      }

      bool doStart = false;
      bool doStop = false;
      switch (args[0].ToLower())
      {
        case "start":
          doStart = true;
          break;
        case "stop":
          doStop = true;
          break;
      }

      if (doStart)
      {
        if (Program.context.SunFollowingThread != null)
        {
          this.appMessageWriter.SendString("Already running...\n");
          return;
        }

        var appMessageWriter = (IAppMessageWriter)provider.GetService(typeof(IAppMessageWriter));
        Follower follower = new Follower(appMessageWriter);

        Program.context.SunFollowingThread = new Thread(new ThreadStart(follower.Start));

        // Start the thread.
        Program.context.SunFollowingThread.Start();

        this.appMessageWriter.SendString("Following the sun...\n");

        return;
      }

      if (doStop)
      {
        if (Program.context.SunFollowingThread != null)
        {
          Program.context.SunFollowingThread.Abort();
          Program.context.SunFollowingThread = null;
        }
      }
    }
  }

  public class Follower
  {
    public readonly IAppMessageWriter appMessageWriter;

    public Follower(IAppMessageWriter appMessageWriter)
    {
      this.appMessageWriter = appMessageWriter;
    }

    public void Start()
    {
      while (true)
      {
        Debug.WriteLine($"Follower: getting the time");
        var realTimeClock = Program.context.RealTimeClockFactory.GetRealTimeClock(Context.RtcAddress, 1);
        var dt = realTimeClock.GetTime();

        Debug.WriteLine($"Follower: getting the position");
        Settings settings = Program.context.SettingsStorageFactory.GetSettingsStorage().ReadSettings() as Settings;

        Debug.WriteLine($"Follower: calculating the angles");
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
          function = (int)Calculator.SpaOutputs.SPA_ZA
        };

        var calculator = new Calculator();
        var result = calculator.Spa_calculate(spa);
        if (result == 0)
        {
          short azimuth = (short)spa.azimuth;
          short zenith = (short)spa.zenith;

          Debug.WriteLine($"Follower: moving the mirror to azimuth {azimuth} and zenith {zenith}");

          var motorController = new MotorController(appMessageWriter);
          motorController.MoveMotor(MotorPlane.Azimuth, azimuth);

          Debug.WriteLine($"Follower: mirrors moved");
        }
        else
        {
          Debug.WriteLine($"Follower: calculating the angles failed with error code {result}");
        }

        Thread.Sleep(10 * 1000);
      }
    }
  }
}
