namespace NFHelio.Tasks
{
  /// <summary>
  /// Sets the geographical position
  /// </summary>
  internal class SetPosition : ITask
  {
    /// <inheritdoc />
    string ITask.Command => "setpos";

    /// <inheritdoc />
    string ITask.Description => "Sets the position as latitude longitude";

    /// <inheritdoc />
    string ITask.Help => "setpos <latitude> <longitude> with both values as doubles";

    /// <inheritdoc />
    public void Execute(string[] args)
    {
      if (args.Length != 2)
      {
        Program.context.BluetoothSpp.SendString("To set the position, provide latitude longitude\n");
        return;
      }

      Program.context.Settings.Latitude = double.Parse(args[0]);
      Program.context.Settings.Longitude = double.Parse(args[1]);

      Program.context.SettingsStorageFactory.GetSettingsStorage().WriteSettings(Program.context.Settings);

      var settings = Program.context.SettingsStorageFactory.GetSettingsStorage().ReadSettings() as Settings;
      Program.context.BluetoothSpp.SendString($"Latitude longitude set to {settings.Latitude}, {settings.Longitude}\n");
    }
  }
}
