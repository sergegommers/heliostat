namespace NFHelio.Tasks
{
  /// <summary>
  /// Returns the time
  /// </summary>
  internal class GetTime : ITask
  {
    /// <inheritdoc />
    string ITask.Command => "gettime";

    /// <inheritdoc />
    string ITask.Description => "Returns the time from the RTC.";

    /// <inheritdoc />
    string ITask.Help => "No further info";

    /// <inheritdoc />
    public void Execute(string[] args)
    {
      var realTimeClock = Program.context.RealTimeClockFactory.GetRealTimeClock(Context.RtcAddress, 1);
      var dt = realTimeClock.GetTime();

      Program.context.BluetoothSpp.SendString($"Time: {dt.ToString("yyyy/MM/dd HH:mm:ss")}\n");
    }
  }
}
