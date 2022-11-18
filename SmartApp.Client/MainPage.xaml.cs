using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinEssentials = Xamarin.Essentials;

namespace SmartApp.Client
{
  [DesignTimeVisible(false)]
  public partial class MainPage : ContentPage
  {
    private readonly IAdapter _bluetoothAdapter;
    private List<IDevice> _gattDevices = new List<IDevice>();

    public MainPage()
    {
      knownDevice = GetKnownDevice();

      InitializeComponent();

      _bluetoothAdapter = CrossBluetoothLE.Current.Adapter;
      _bluetoothAdapter.DeviceDiscovered += (sender, foundBleDevice) =>
      {
        if (foundBleDevice.Device != null && !string.IsNullOrEmpty(foundBleDevice.Device.Name))
          _gattDevices.Add(foundBleDevice.Device);
      };

      // this is somehow needed to get all bluetooth permission setup correctly
      _ = Geolocation.GetLastKnownLocationAsync();
    }

    public string knownDevice
    {
      get;
      set;
    }

    private async Task<bool> PermissionsGrantedAsync()
    {
      var locationPermissionStatus = await Permissions.CheckStatusAsync<XamarinEssentials.Permissions.LocationAlways>();

      if (locationPermissionStatus != PermissionStatus.Granted)
      {
        var status = await Permissions.RequestAsync<Permissions.LocationAlways>();
        return status == PermissionStatus.Granted;
      }
      return true;
    }

    private async void ScanButton_Clicked(object sender, EventArgs e)
    {
      IsBusyIndicator.IsVisible = IsBusyIndicator.IsRunning = !(ScanButton.IsEnabled = false);
      foundBleDevicesListView.ItemsSource = null;

      if (!await PermissionsGrantedAsync())
      {
        await DisplayAlert("Permission required", "Application needs location permission", "OK");
        IsBusyIndicator.IsVisible = IsBusyIndicator.IsRunning = !(ScanButton.IsEnabled = true);
        return;
      }

      _gattDevices.Clear();

      foreach (var device in _bluetoothAdapter.ConnectedDevices)
        _gattDevices.Add(device);

      await _bluetoothAdapter.StartScanningForDevicesAsync();

      foundBleDevicesListView.ItemsSource = _gattDevices.ToArray();
      IsBusyIndicator.IsVisible = IsBusyIndicator.IsRunning = !(ScanButton.IsEnabled = true);
    }

    private async void FoundBluetoothDevicesListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
      await ConnectToDeviceAsync(e.Item as IDevice, null);
    }

    private async Task ConnectToDeviceAsync(IDevice device, Guid? deviceId)
    {
      IsBusyIndicator.IsVisible = IsBusyIndicator.IsRunning = !(ScanButton.IsEnabled = false);

      if (device != null && device.State == DeviceState.Connected)
      {
        await Navigation.PushAsync(new BluetoothDataPage(device));
      }
      else
      {
        try
        {
          if (deviceId != null)
          {
            // todo: use a cancellation token, as on IOS the wait can be indefinitely
            device = await _bluetoothAdapter.ConnectToKnownDeviceAsync(deviceId.Value);
          }
          else
          {
            var connectParameters = new ConnectParameters(false, true);
            await _bluetoothAdapter.ConnectToDeviceAsync(device, connectParameters);
          }

          await Navigation.PushAsync(new BluetoothDataPage(device));
        }
        catch
        {
          await DisplayAlert("Error connecting", $"Error connecting to BLE device: {device.Name ?? "N/A"}", "Retry");
        }
      }

      IsBusyIndicator.IsVisible = IsBusyIndicator.IsRunning = !(ScanButton.IsEnabled = true);
    }

    private async void ConnectToKnownDeviceButton_Clicked(object sender, EventArgs e)
    {
      if (Application.Current.Properties.ContainsKey("deviceid"))
      {
        var id = new Guid(Application.Current.Properties["deviceid"].ToString());

        await ConnectToDeviceAsync(null, id);
      }
    }

    private string GetKnownDevice()
    {
      if (Application.Current.Properties.ContainsKey("devicename"))
      {
        return Application.Current.Properties["devicename"].ToString();
      }

      return null;
    }
  }
}