namespace NFHelio
{
  using System.Collections;
  using NFHelio.Tasks;
  using System.Diagnostics;

  /// <summary>
  /// Here we check the incoming commands and start the matching tasks.
  /// </summary>
  public class CommandHandler
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CommandHandler"/> class.
    /// </summary>
    public CommandHandler()
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
          new Follow(),
          new Calibrate(),
          new MoveMirror(),
          new CalcSpa(),
          new SetTime(),
          new SetPosition(),
          new GetTime(),
          new FindFreeRam(),
          new FreeMem(),
          new TestAdc(),
          new TestEngine(),
          new Reboot(),
        };

        switch (command)
        {
          case "help":
            foreach (ITask task in tasks)
            {
              Program.context.BluetoothSpp.SendString($"{task.Description}\r\n");
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

            Program.context.BluetoothSpp.SendString($"Unknown command\r\n");

            break;
        }
      }
    }
  }
}
