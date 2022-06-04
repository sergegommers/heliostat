namespace NFHelio.Tasks
{
  using NFSpa;
  using System.Diagnostics;
  using System.Threading;

  // see https://jsimoesblog.wordpress.com/2020/06/02/multithreading-in-net-nanoframework/
  internal class Follow : ITask
  {
    public string Command => "follow";

    public string Help => "follow";

    public string Description => "follow";

    public void Execute(string[] args)
    {
      if (args.Length != 1)
      {
        Program.context.BluetoothSpp.SendString("Error\n");
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
          Program.context.BluetoothSpp.SendString("Already running...\n");
          return;
        }

        Follower follower = new Follower();
        Program.context.SunFollowingThread = new Thread(new ThreadStart(follower.Start));

        // Start the thread.
        Program.context.SunFollowingThread.Start();

        Program.context.BluetoothSpp.SendString("Following the sun...\n");

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
          function = (int)Calculator.SpaOutputs.SPA_ZA
        };

        var calculator = new Calculator();
        var result = calculator.Spa_calculate(spa);
        if (result == 0)
        {
          short azimuth = (short)spa.azimuth;
          short zenith = (short)spa.zenith;

          Debug.WriteLine($"Follower: moving the mirror to azimuth {azimuth} and zenith {zenith}");

          var motorController = new MotorController();
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
