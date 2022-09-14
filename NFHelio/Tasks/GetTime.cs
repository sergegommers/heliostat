namespace NFHelio.Tasks
{
  using NFHelio.Devices;
  using System;

  /// <summary>
  /// Returns the time
  /// </summary>
  internal class GetTime : BaseTask, ITask
  {
    /// <inheritdoc />
    string ITask.Command => "gettime";

    /// <inheritdoc />
    string ITask.Description => "Returns the time from the RTC.";

    /// <inheritdoc />
    string ITask.Help => "No further info";

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTime" /> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    public GetTime(IServiceProvider serviceProvider)
    : base(serviceProvider)
    {
    }

    /// <inheritdoc />
    public void Execute(string[] args)
    {
      var realTimeClockFactory = (IRealTimeClockFactory)this.GetServiceProvider().GetService(typeof(IRealTimeClockFactory));
      var realTimeClock = realTimeClockFactory.Create();

      var dt = realTimeClock.GetTime();

      this.SendString($"Time: {dt.ToString("yyyy/MM/dd HH:mm:ss")}\n");
    }
  }
}
