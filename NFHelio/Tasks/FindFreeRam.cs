namespace NFHelio.Tasks
{
  using NFCommon.Services;
  using System;

  /// <summary>
  /// Finds the free RAM by allocating as much bytes as possible
  /// </summary>
  internal class FindFreeRam : ITask
  {
    private readonly IAppMessageWriter appMessageWriter;

    /// <inheritdoc />
    string ITask.Command => "freeram";

    /// <inheritdoc />
    string ITask.Description => "Finds how much RAM is available";

    /// <inheritdoc />
    string ITask.Help => "No further info";

    /// <summary>
    /// Initializes a new instance of the <see cref="FindFreeRam"/> class.
    /// </summary>
    /// <param name="appMessageWriter">The application message writer.</param>
    public FindFreeRam(IAppMessageWriter appMessageWriter)
    {
      this.appMessageWriter = appMessageWriter;
    }

    /// <inheritdoc />
    public void Execute(string[] args)
    {
      int freeram = 0;

      for (int i = 15; i > 4; i--)
      {
        var relsize = (int)Math.Pow(2, i);
        try
        {
          this.appMessageWriter.SendString($"FreeRam - Allocating {relsize + freeram} bytes\n");
          Alloc(relsize + freeram);
          freeram += relsize;
          this.appMessageWriter.SendString($"FreeRam - Allocated {freeram} bytes\n");
        }
        catch
        {
        }
      }

      this.appMessageWriter.SendString($"FreeRam = {freeram} bytes\n");
    }

    private void Alloc(int size)
    {
      byte[] data = new byte[size];
      data[0] = 1;
    }
  }
}
