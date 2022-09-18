namespace NFHelio.Tasks
{
  using NFCommon.Services;
  using System;

  /// <summary>
  /// A base class for all Tasks.
  /// </summary>
  internal class BaseTask
  {
    private readonly IServiceProvider serviceProvider;
    private readonly IAppMessageWriter appMessageWriter;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseTask"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    public BaseTask(IServiceProvider serviceProvider)
    {
      this.serviceProvider = serviceProvider;
      this.appMessageWriter = (IAppMessageWriter)serviceProvider.GetService(typeof(IAppMessageWriter));
    }

    protected IServiceProvider GetServiceProvider()
    {
      return serviceProvider;
    }

    protected void SendString(string message)
    {
      this.appMessageWriter.SendString(message);
    }
  }
}
