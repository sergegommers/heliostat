namespace NFHelio.Tasks
{
  internal class MoveMirror : ITask
  {
    /// <inheritdoc />
    string ITask.Command => "move";

    /// <inheritdoc />
    string ITask.Description => "Moves the mirror in the given plane over the given angle";

    /// <inheritdoc />
    string ITask.Help => "move <plane> <angle> where plane is a or z, angle is the desired angle";

    /// <inheritdoc />
    public void Execute(string[] args)
    {
      if (args.Length != 2)
      {
        Program.context.BluetoothSpp.SendString("To move, provide a or z and angle\n");
        return;
      }

      MotorPlane plane;
      switch (args[0].ToLower())
      {
        case "a":
          plane = MotorPlane.Azimuth;
          break;
        case "z":
          plane = MotorPlane.Zenith;
          break;
        default:
          Program.context.BluetoothSpp.SendString("Unknown plane\n");
          return;
      }

      short angleDesired = short.Parse(args[1]);

      var motorController = new MotorController();
      motorController.MoveMotor(plane, angleDesired);
    }
  }
}
