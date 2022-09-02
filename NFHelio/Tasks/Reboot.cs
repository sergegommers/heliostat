namespace NFHelio.Tasks
{
  using nanoFramework.Runtime.Native;
  using NFCommon.Services;
  using System.Threading;

  /// <summary>
  /// Reboots the microcontroller
  /// </summary>
  internal class Reboot : ITask
  {
    private readonly IAppMessageWriter appMessageWriter;

    /// <inheritdoc />
    string ITask.Command => "reboot";

    /// <inheritdoc />
    string ITask.Description => "Reboots the microcontroller";

    /// <inheritdoc />
    string ITask.Help => "No further info";

    /// <summary>
    /// Initializes a new instance of the <see cref="Reboot"/> class.
    /// </summary>
    /// <param name="appMessageWriter">The application message writer.</param>
    public Reboot(IAppMessageWriter appMessageWriter)
    {
      this.appMessageWriter = appMessageWriter;
    }

    /// <inheritdoc />
    public void Execute(string[] args)
    {
      this.appMessageWriter.SendString("Rebooting now\n");
      Thread.Sleep(100);
      Power.RebootDevice();
    }
  }
}
