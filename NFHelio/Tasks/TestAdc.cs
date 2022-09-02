namespace NFHelio.Tasks
{
  using NFCommon.Services;
  using System;
  using System.Device.Adc;
  using System.Diagnostics;
  using System.Threading;

  /// <summary>
  /// Tests the ADC
  /// </summary>
  internal class TestAdc : ITask
  {
    private readonly IAppMessageWriter appMessageWriter;

    /// <inheritdoc />
    string ITask.Command => "testadc";

    /// <inheritdoc />
    string ITask.Description => "Tests the adc controller";

    /// <inheritdoc />
    string ITask.Help => "testadc <plane> <samples> where plane is a or z\nand samples is the number of measurements to take";

    /// <summary>
    /// Initializes a new instance of the <see cref="TestAdc"/> class.
    /// </summary>
    /// <param name="appMessageWriter">The application message writer.</param>
    public TestAdc(IAppMessageWriter appMessageWriter)
    {
      this.appMessageWriter = appMessageWriter;
    }

    /// <inheritdoc />
    public void Execute(string[] args)
    {
      var adc = new AdcController();

      if (args.Length < 1)
      {
        this.appMessageWriter.SendString("To test, provide a or z and (optionally) #samples\n");
        return;
      }

      int adcChannelNumber;
      switch (args[0].ToLower())
      {
        case "a":
          adcChannelNumber = Context.AzimuthAdcChannel;
          break;
        case "z":
          adcChannelNumber = Context.ZenithAdcChannel;
          break;
        default:
          this.appMessageWriter.SendString("Unknown plane\n");
          return;
      }

      int setSize = 1;
      if (args.Length > 1)
      {
        setSize = int.Parse(args[1]);
      }

      Debug.WriteLine($"adc min = {adc.MinValue}, max = {adc.MaxValue}, setsize = {setSize}");

      var endTime = DateTime.UtcNow + TimeSpan.FromSeconds(10);

      var adcChannel = adc.OpenChannel(adcChannelNumber);

      double value = 0;
      double percent = 0;
      while (DateTime.UtcNow < endTime)
      {
        for (int i = 0; i < setSize; i++)
        {
          value += adcChannel.ReadValue();
          percent += adcChannel.ReadRatio();
        }

        value /= setSize;
        percent /= setSize;

        this.appMessageWriter.SendString($"value = {(int)value}, ratio = {percent}\n");

        value = 0;
        percent = 0;

        Thread.Sleep(1000);
      }

      this.appMessageWriter.SendString($"Done testing adc\n");
    }
  }
}
