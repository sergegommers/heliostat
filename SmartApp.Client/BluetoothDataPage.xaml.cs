using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinEssentials = Xamarin.Essentials;

namespace SmartApp.Client
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class BluetoothDataPage : ContentPage
  {
    private readonly IDevice _connectedDevice;


    public BluetoothDataPage(IDevice connectedDevice)
    {
      InitializeComponent();
      _connectedDevice = connectedDevice;
      InitButton.IsEnabled = !(ScanButton.IsEnabled = false);
    }

    private ICharacteristic sendCharacteristic;
    private ICharacteristic receiveCharacteristic;

    private async void InitalizeCommandButton_Clicked(object sender, EventArgs e)
    {
      try
      {
        // bluetooth chunk size is 20 by default, here we increase it to the max we need
        await _connectedDevice.RequestMtuAsync(64);

        var service = await _connectedDevice.GetServiceAsync(GattIdentifiers.UartGattServiceId);

        if (service != null)
        {
          sendCharacteristic = await service.GetCharacteristicAsync(GattIdentifiers.UartGattCharacteristicSendId);

          receiveCharacteristic = await service.GetCharacteristicAsync(GattIdentifiers.UartGattCharacteristicReceiveId);
          if (receiveCharacteristic != null)
          {
            var descriptors = await receiveCharacteristic.GetDescriptorsAsync();

            receiveCharacteristic.ValueUpdated += (o, args) =>
            {
              var receivedBytes = args.Characteristic.Value;
              MainThread.BeginInvokeOnMainThread(() =>
                          {
                            var newtext = Encoding.UTF8.GetString(receivedBytes, 0, receivedBytes.Length) + Environment.NewLine;
                            Output.Text += newtext;
                          });
            };

            await receiveCharacteristic.StartUpdatesAsync();
            InitButton.IsEnabled = !(ScanButton.IsEnabled = true);

            // store the current device so we can connect to it later without rescanning
            Application.Current.Properties["devicename"] = _connectedDevice.Name;
            Application.Current.Properties["deviceid"] = _connectedDevice.Id;

            await Application.Current.SavePropertiesAsync();
          }
        }
        else
        {
          Output.Text += "UART GATT service not found." + Environment.NewLine;
        }
      }
      catch (Exception ex)
      {
        Output.Text += "Error initializing UART GATT service." + Environment.NewLine;
      }
    }


    private async void SetTimeAndLocationCommandButton_Clicked(object sender, EventArgs e)
    {
      var location = await Geolocation.GetLastKnownLocationAsync();
      if (location != null)
      {
        Output.Text += $"Latitude = {location.Latitude}, Longitude = {location.Longitude}, Accuracy = {location.Accuracy}" + Environment.NewLine;
      }

      try
      {
        if (sendCharacteristic != null)
        {
          var result = await sendCharacteristic.WriteAsync(Encoding.ASCII.GetBytes($"setpos {location.Latitude} {location.Longitude}\r\n"));
        }
      }
      catch (Exception ex)
      {
        Output.Text += "Error sending comand to UART." + Environment.NewLine;
      }
    }

    private async void SendCommandButton_Clicked(object sender, EventArgs e)
    {
      try
      {
        if (sendCharacteristic != null)
        {
          var bytes = await sendCharacteristic.WriteAsync(Encoding.ASCII.GetBytes($"{CommandTxt.Text}\r\n"));
        }
      }
      catch (Exception ex)
      {
        Output.Text += "Error sending comand to UART." + Environment.NewLine;
      }
    }
  }
}