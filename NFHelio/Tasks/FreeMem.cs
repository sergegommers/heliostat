namespace NFHelio.Tasks
{
  using nanoFramework.Hardware.Esp32;
  using NFCommon.Services;

  /// <summary>
  /// Shows details about the memory
  /// </summary>
  internal class FreeMem : ITask
  {
    private readonly IAppMessageWriter appMessageWriter;

    /// <inheritdoc />
    string ITask.Command => "mem";

    /// <inheritdoc />
    string ITask.Description => "Shows details about the memory";

    /// <inheritdoc />
    string ITask.Help => "No further info";

    /// <summary>
    /// Initializes a new instance of the <see cref="FreeMem"/> class.
    /// </summary>
    /// <param name="appMessageWriter">The application message writer.</param>
    public FreeMem(IAppMessageWriter appMessageWriter)
    {
      this.appMessageWriter = appMessageWriter;
    }

    /// <inheritdoc />
    public void Execute(string[] args)
    {
      NativeMemory.GetMemoryInfo(
        NativeMemory.MemoryType.All,
        out uint totalSize,
        out uint totalFreeSize,
        out uint largestBlock);

      this.appMessageWriter.SendString($"Native memory - Total:{totalSize}\n");
      this.appMessageWriter.SendString($"Native memory - Free:{totalFreeSize}\n");
      this.appMessageWriter.SendString($"Native memory - Largest:{largestBlock}\n");
    }
  }
}
