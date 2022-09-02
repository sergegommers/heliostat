using NFCommon.Services;

namespace NFHelio.Tasks
{
  /// <summary>
  /// Sets the time
  /// </summary>
  internal class SetTime : ITask
  {
    private readonly IAppMessageWriter appMessageWriter;

    /// <inheritdoc />
    string ITask.Command => "settime";

    /// <inheritdoc />
    string ITask.Description => "Sets the time of the real time clock";

    /// <inheritdoc />
    string ITask.Help => "settime <yyyy> <mm> <dd> <hh> <mm> <ss>\nwith the time in UTC";

    /// <summary>
    /// Initializes a new instance of the <see cref="SetTime"/> class.
    /// </summary>
    /// <param name="appMessageWriter">The application message writer.</param>
    public SetTime(IAppMessageWriter appMessageWriter)
    {
      this.appMessageWriter = appMessageWriter;
    }

    /// <inheritdoc />
    public void Execute(string[] args)
    {
      if (args.Length != 6)
      {
        this.appMessageWriter.SendString("To set the time provide year month day hour minute second\n");
        return;
      }

      var realTimeClock = Program.context.RealTimeClockFactory.GetRealTimeClock(Context.RtcAddress, 1);
      realTimeClock.SetTime(
        int.Parse(args[0]),
        int.Parse(args[1]),
        int.Parse(args[2]),
        int.Parse(args[3]),
        int.Parse(args[4]),
        int.Parse(args[5]));

      var dt = realTimeClock.GetTime();
      this.appMessageWriter.SendString($"Time set to: {dt.ToString("yyyy/MM/dd HH:mm:ss")}\n");
    }
  }
}
