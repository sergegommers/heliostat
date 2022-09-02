namespace NFHelio.Tasks
{
  using NFCommon;
  using NFCommon.Services;

  /// <summary>
  /// Calibrates the potentiometers that read out the azimuth and zenith values
  /// </summary>
  internal class Calibrate : ITask
  {
    private readonly IAppMessageWriter appMessageWriter;

    /// <inheritdoc />
    string ITask.Command => "cal";

    /// <inheritdoc />
    string ITask.Description => "Calibrates the azimuth and zenith angles";

    /// <inheritdoc />
    string ITask.Help => "cal <plane> <angle> where plane is a or z\nor r to reset existing values.\nAngle is the current angle.";

    public Calibrate(IAppMessageWriter appMessageWriter)
    {
      this.appMessageWriter = appMessageWriter;
    }

    /// <inheritdoc />
    public void Execute(string[] args)
    {
      if (args.Length != 2)
      {
        this.appMessageWriter.SendString("Invalid number of arguments\n");
        return;
      }

      int channelNumber;
      switch (args[0].ToLower())
      {
        case "a":
          channelNumber = Context.AzimuthAdcChannel;
          break;
        case "z":
          channelNumber = Context.ZenithAdcChannel;
          break;
        case "r":
          Program.context.Settings.Aci = new short[0];
          Program.context.Settings.Acv = new short[0];
          Program.context.Settings.Zci = new short[0];
          Program.context.Settings.Zcv = new short[0];
          Program.context.SettingsStorageFactory.GetSettingsStorage().WriteSettings(Program.context.Settings);

          this.appMessageWriter.SendString("Mirror calibration is cleared\n");
          return;
        default:
          this.appMessageWriter.SendString("Unknown plane to calibrate\n");
          return;
      }

      bool result = int.TryParse(args[1], out int angle);
      if (!result)
      {
        this.appMessageWriter.SendString("Can't convert given angle to an integer\n");
        return;
      }

      double value = AdcReader.GetValue(channelNumber, Context.AdcSampleSize);

      switch (args[0])
      {
        case "a":
          {
            var array = new CalibrationArray(Program.context.Settings.Aci, Program.context.Settings.Acv);
            array.AddCalibrationPoint((short)angle, (short)value);
            Program.context.Settings.Aci = array.GetIndexes();
            Program.context.Settings.Acv = array.GetValues();
            break;
          }
        case "z":
          {
            var array = new CalibrationArray(Program.context.Settings.Zci, Program.context.Settings.Zcv);
            array.AddCalibrationPoint((short)angle, (short)value);
            Program.context.Settings.Zci = array.GetIndexes();
            Program.context.Settings.Zcv = array.GetValues();
            break;
          }
      }

      Program.context.SettingsStorageFactory.GetSettingsStorage().WriteSettings(Program.context.Settings);
      this.appMessageWriter.SendString($"Adc value for plane {args[0]}\nset to angle {(short)angle} and value {(short)value}\n");
    }
  }
}
