using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartApp.Client
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class BluetoothDataPage : ContentPage
  {
    private readonly IDevice _connectedDevice;

    private HelioStat helioStat;


    public BluetoothDataPage(IDevice connectedDevice)
    {
      InitializeComponent();
      _connectedDevice = connectedDevice;
      InitButton.IsEnabled = !(ScanButton.IsEnabled = false);
    }

    private ICharacteristic sendCharacteristic;
    private ICharacteristic receiveCharacteristic;

    private async void CalibrateCommandButton_Clicked(object sender, EventArgs e)
    {
      await Navigation.PushAsync(new CalibrationPage(this.helioStat));
    }

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

            helioStat = new HelioStat(sendCharacteristic, receiveCharacteristic);
            await helioStat.StartUpdatesAsync();

            InitButton.IsEnabled = !(ScanButton.IsEnabled = true);

            // store the current device so we can connect to it later without rescanning
            Application.Current.Properties["devicename"] = _connectedDevice.Name;
            Application.Current.Properties["deviceid"] = _connectedDevice.Id;

            await Application.Current.SavePropertiesAsync();
          }
        }
        else
        {
          AddToLog("UART GATT service not found.");
        }
      }
      catch (Exception ex)
      {
        AddToLog($"Error initializing UART GATT service: {ex.Message}");
      }
    }

    private void AddToLog(string text)
    {
      if (string.IsNullOrWhiteSpace(text))
      {
        return;
      }

      MainThread.BeginInvokeOnMainThread(() =>
      {
        Output.Text += text + Environment.NewLine;
      });
    }

    private async void SetTimeAndLocationCommandButton_Clicked(object sender, EventArgs e)
    {
      var result = await helioStat.SetTimeAndLocationAsync();
      AddToLog(result);
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
        AddToLog($"Error sending command to device: {ex.Message}");
      }
    }
  }
}