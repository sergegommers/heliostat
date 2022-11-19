namespace SmartApp.Client
{
  using Plugin.BLE.Abstractions.Contracts;
  using System;
  using System.Text;
  using System.Text.RegularExpressions;
  using System.Threading;
  using System.Threading.Tasks;
  using Xamarin.Essentials;

  public class SerialDevice
  {
    private readonly ICharacteristic sendCharacteristic;
    private readonly ICharacteristic receiveCharacteristic;

    private string receivedStrings = string.Empty;

    public SerialDevice(ICharacteristic sendCharacteristic, ICharacteristic receiveCharacteristic)
    {
      this.sendCharacteristic = sendCharacteristic;
      this.receiveCharacteristic = receiveCharacteristic;

      this.receiveCharacteristic.ValueUpdated += BytesReceivedEventHandler;
    }

    public async Task StartUpdatesAsync()
    {
      // start listening
      await receiveCharacteristic.StartUpdatesAsync();
    }

    public async Task WriteStringAsync(string value)
    {
      await MainThread.InvokeOnMainThreadAsync(async () =>
      {
        // send the command to set the position
        await sendCharacteristic.WriteAsync(Encoding.ASCII.GetBytes(value));
      });
    }

    private void BytesReceivedEventHandler(object sender, Plugin.BLE.Abstractions.EventArgs.CharacteristicUpdatedEventArgs e)
    {
      byte[] receivedBytes = e.Characteristic.Value;
      this.receivedStrings += Encoding.UTF8.GetString(receivedBytes, 0, receivedBytes.Length);
    }

    protected async Task<bool> WaitForSilenceAsync(TimeSpan timeSpan)
    {
      CancellationTokenSource source = new CancellationTokenSource();
      source.CancelAfter(timeSpan);

      bool result = await WaitForSilenceAsync(source.Token);

      source.Dispose();

      return result;
    }

    protected async Task<bool> WaitForSilenceAsync(CancellationToken cancellationToken)
    {
      this.receivedStrings = string.Empty;
      while (!cancellationToken.IsCancellationRequested)
      {
        await Task.Delay(500);
        if (this.receivedStrings == string.Empty)
        {
          break;
        }
        else
        {
          this.receivedStrings = string.Empty;
        }
      }

      return !cancellationToken.IsCancellationRequested;
    }

    protected async Task<MatchCollection> WaitForRegExAsync(string regex, int matchcount, TimeSpan timeSpan)
    {
      CancellationTokenSource source = new CancellationTokenSource();
      source.CancelAfter(timeSpan);

      var result = await WaitForRegExAsync(regex, matchcount, source.Token);

      source.Dispose();

      return result;
    }

    protected async Task<MatchCollection> WaitForRegExAsync(string regex, int matchcount, CancellationToken cancellationToken)
    {
      while (!cancellationToken.IsCancellationRequested)
      {
        var rg = new Regex(regex);
        var matches = rg.Matches(receivedStrings);
        if (matches.Count == matchcount)
        {
          return matches;
        }

        await Task.Delay(100);
      }

      return null;
    }
  }
}
