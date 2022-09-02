using NFCommon.Services;
using System;

namespace NFHelio.Tasks
{
  internal class MoveMirror : ITask
  {
    private readonly IServiceProvider provider;
    private readonly IAppMessageWriter appMessageWriter;

    /// <inheritdoc />
    string ITask.Command => "move";

    /// <inheritdoc />
    string ITask.Description => "Moves the mirror";

    /// <inheritdoc />
    string ITask.Help => "move <plane> <angle> where plane is a or z,\nangle is the desired angle";

    public MoveMirror(IServiceProvider provider, IAppMessageWriter appMessageWriter)
    {
      this.provider = provider;
      this.appMessageWriter = appMessageWriter;
    }

    /// <inheritdoc />
    public void Execute(string[] args)
    {
      if (args.Length != 2)
      {
        this.appMessageWriter.SendString("To move, provide a or z and angle\n");
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
          this.appMessageWriter.SendString("Unknown plane\n");
          return;
      }

      short angleDesired = short.Parse(args[1]);

      var appMessageWriter = (IAppMessageWriter)provider.GetService(typeof(IAppMessageWriter));

      var motorController = new MotorController(appMessageWriter);
      motorController.MoveMotor(plane, angleDesired);
    }
  }
}
