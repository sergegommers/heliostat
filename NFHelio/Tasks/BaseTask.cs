namespace NFHelio.Tasks
{
  using NFCommon.Services;
  using System;

  internal class BaseTask
  {
    private readonly IServiceProvider serviceProvider;
    private readonly IAppMessageWriter appMessageWriter;

    public BaseTask(IServiceProvider serviceProvider)
    {
      this.serviceProvider = serviceProvider;
      this.appMessageWriter = (IAppMessageWriter)serviceProvider.GetService(typeof(IAppMessageWriter));
    }

    protected void SendString(string message)
    {
      this.appMessageWriter.SendString(message);
    }
  }
}
