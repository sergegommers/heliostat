using NFCommon.Services;

namespace NFHelio.Tasks
{
  /// <summary>
  /// Returns the time
  /// </summary>
  internal class GetTime : ITask
  {
    private readonly IAppMessageWriter appMessageWriter;

    /// <inheritdoc />
    string ITask.Command => "gettime";

    /// <inheritdoc />
    string ITask.Description => "Returns the time from the RTC.";

    /// <inheritdoc />
    string ITask.Help => "No further info";

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTime"/> class.
    /// </summary>
    /// <param name="appMessageWriter">The application message writer.</param>
    public GetTime(IAppMessageWriter appMessageWriter)
    {
      this.appMessageWriter = appMessageWriter;
    }

    /// <inheritdoc />
    public void Execute(string[] args)
    {
      var realTimeClock = Program.context.RealTimeClockFactory.GetRealTimeClock(Context.RtcAddress, 1);
      var dt = realTimeClock.GetTime();

      this.appMessageWriter.SendString($"Time: {dt.ToString("yyyy/MM/dd HH:mm:ss")}\n");
    }
  }
}
