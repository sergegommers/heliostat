using NFCommon.Services;

namespace NFHelio.Tasks
{
  /// <summary>
  /// Sets the geographical position
  /// </summary>
  internal class SetPosition : ITask
  {
    private readonly IAppMessageWriter appMessageWriter;

    /// <inheritdoc />
    string ITask.Command => "setpos";

    /// <inheritdoc />
    string ITask.Description => "Sets the position as latitude longitude";

    /// <inheritdoc />
    string ITask.Help => "setpos <latitude> <longitude>\nwith both values as doubles";

    /// <summary>
    /// Initializes a new instance of the <see cref="SetPosition"/> class.
    /// </summary>
    /// <param name="appMessageWriter">The application message writer.</param>
    public SetPosition(IAppMessageWriter appMessageWriter)
    {
      this.appMessageWriter = appMessageWriter;
    }

    /// <inheritdoc />
    public void Execute(string[] args)
    {
      if (args.Length != 2)
      {
        this.appMessageWriter.SendString("To set the position, provide latitude longitude\n");
        return;
      }

      Program.context.Settings.Latitude = double.Parse(args[0]);
      Program.context.Settings.Longitude = double.Parse(args[1]);

      Program.context.SettingsStorageFactory.GetSettingsStorage().WriteSettings(Program.context.Settings);

      var settings = Program.context.SettingsStorageFactory.GetSettingsStorage().ReadSettings() as Settings;
      this.appMessageWriter.SendString($"Latitude longitude set to {settings.Latitude}, {settings.Longitude}\n");
    }
  }
}
