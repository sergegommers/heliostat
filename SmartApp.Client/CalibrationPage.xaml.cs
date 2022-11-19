namespace SmartApp.Client
{
  using System;
  using Xamarin.Forms;
  using Xamarin.Forms.Xaml;

  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class CalibrationPage : ContentPage
  {
    private readonly HelioStat helioStat;

    public object Plane
    {
      get;
      set;
    }

    public CalibrationPage(HelioStat helioStat)
    {
      this.helioStat = helioStat;

      InitializeComponent();
    }

    private char GetAngle()
    {
      if (Plane.ToString() == "Azimuth")
      {
        return 'a';
      }
      else
      {
        return 'z';
      }
    }

    private void OnAngleRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
      RadioButton radioButton = sender as RadioButton;
      if (radioButton != null)
      {
        if (radioButton.IsChecked)
        {
          Plane = radioButton.Value.ToString();
        }
      }
    }

    private async void MoveDown_Pressed(object sender, EventArgs e)
    {
      var result = await this.helioStat.MoveMotorAsync(GetAngle(), -0.5);
    }

    private async void MoveDown_Released(object sender, EventArgs e)
    {
      var result = await this.helioStat.MoveMotorAsync('a', 0.0);
      result += await this.helioStat.MoveMotorAsync('z', 0.0);
    }

    private async void MoveUp_Pressed(object sender, EventArgs e)
    {
      var result = await this.helioStat.MoveMotorAsync(GetAngle(), 0.5);
    }

    private async void MoveUp_Released(object sender, EventArgs e)
    {
      var result = await this.helioStat.MoveMotorAsync('a', 0.0);
      result += await this.helioStat.MoveMotorAsync('z', 0.0);
    }
  }
}