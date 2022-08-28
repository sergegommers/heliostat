namespace NFHelio.Services
{
  using System.Collections;
  using NFHelio.Tasks;
  using System.Diagnostics;
  using NFHelio;

  /// <summary>
  /// Here we check the incoming commands and start the matching tasks.
  /// </summary>
  public class CommandHandlerService : ICommandHandlerService
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CommandHandlerService"/> class.
    /// </summary>
    public CommandHandlerService()
    {
    }

    /// <summary>
    /// Handles the message.
    /// </summary>
    /// <param name="message">The message.</param>
    public void HandleMessage(string message)
    {
      if (string.IsNullOrEmpty(message))
      {
        return;
      }

      message = message.Trim();

      Debug.WriteLine($"Received=>{message}");

      string[] args = message.Split(' ');
      if (args.Length != 0)
      {
        var command = args[0].ToLower();
        message = message.Substring(args[0].Length).Trim();
        if (string.IsNullOrEmpty(message))
        {
          args = new string[0];
        }
        else
        {
          args = message.Split(' ');
        }

        var tasks = new ArrayList
        {
          new TestOnboardLed(),
          new FollowSun(),
          new Calibrate(),
          new MoveMirror(),
          new CalcSpa(),
          new SetTime(),
          new SetPosition(),
          new GetTime(),
          new FindFreeRam(),
          new FreeMem(),
          new TestAdc(),
          new TestMotors(),
          new Reboot(),
        };

        switch (command)
        {
          case "help":
            if (args.Length == 0)
            {
              Program.context.BluetoothSpp.SendString($"help <command> for more info.\n");
              Program.context.BluetoothSpp.SendString($"\n");
            }
            foreach (ITask task in tasks)
            {
              if (args.Length > 0)
              {
                if (args[0].ToLower() == task.Command.ToLower())
                {
                  Program.context.BluetoothSpp.SendString($"{task.Help}\n");
                  break;
                }
              }
              else
              {
                Program.context.BluetoothSpp.SendString($"{task.Command}: {task.Description}\n");
              }
            }
            break;
          default:
            foreach (ITask task in tasks)
            {
              if (command == task.Command)
              {
                task.Execute(args);

                return;
              }
            }

            Program.context.BluetoothSpp.SendString($"Unknown command\n");

            break;
        }
      }
    }
  }
}
