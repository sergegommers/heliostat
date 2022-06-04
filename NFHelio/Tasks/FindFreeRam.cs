namespace NFHelio.Tasks
{
  using System;

  /// <summary>
  /// Finds the free RAM by allocating as much bytes as possible
  /// </summary>
  internal class FindFreeRam : ITask
  {
    /// <inheritdoc />
    string ITask.Command => "freeram";

    /// <inheritdoc />
    string ITask.Description => "freeram: Finds how much RAM is available";

    /// <inheritdoc />
    string ITask.Help => "No further info";

    /// <inheritdoc />
    public void Execute(string[] args)
    {
      int freeram = 0;

      for (int i = 15; i > 4; i--)
      {
        var relsize = (int)Math.Pow(2, i);
        try
        {
          Program.context.BluetoothSpp.SendString($"FreeRam - Allocating {relsize + freeram} bytes\n");
          Alloc(relsize + freeram);
          freeram += relsize;
          Program.context.BluetoothSpp.SendString($"FreeRam - Allocated {freeram} bytes\n");
        }
        catch
        {
        }
      }

      Program.context.BluetoothSpp.SendString($"FreeRam = {freeram} bytes\n");
    }

    private void Alloc(int size)
    {
      byte[] data = new byte[size];
      data[0] = 1;
    }
  }
}
