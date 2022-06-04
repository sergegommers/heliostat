namespace NFHelio
{
  using NFCommon;
  using System;
  using System.Device.Pwm;
  using System.Diagnostics;

  public enum MotorPlane
  {
    Azimuth,
    Zenith
  }

  public class MotorController
  {
    public void MoveMotor(MotorPlane plane, short angleDesired)
    {
      int adcChannel;
      PwmChannel pwmPin1;
      PwmChannel pwmPin2;

      switch (plane)
      {
        case MotorPlane.Azimuth:
          if (angleDesired < 90 || angleDesired > 270)
          {
            Debug.WriteLine($"Angle is out of range");
            return;
          }

          adcChannel = Context.AzimuthAdcChannel;
          pwmPin1 = PwmChannel.CreateFromPin((int)GPIOPort.PWM_Azimuth_East_to_West, 40000, 0);
          pwmPin2 = PwmChannel.CreateFromPin((int)GPIOPort.PWM_Azimuth_West_to_East, 40000, 0);
          break;
        case MotorPlane.Zenith:
          if (angleDesired < 90 || angleDesired > 270)
          {
            Debug.WriteLine($"Angle is out of range");
            return;
          }
          adcChannel = Context.ZenithAdcChannel;
          pwmPin1 = PwmChannel.CreateFromPin((int)GPIOPort.PWM_Zenith_Up, 40000, 0);
          pwmPin2 = PwmChannel.CreateFromPin((int)GPIOPort.PWM_Zenith_Down, 40000, 0);
          break;
        default:
          Program.context.BluetoothSpp.SendString("Unknown plane\n");
          return;
      }

      // get the current value of the motor position
      short value = (short)AdcReader.GetValue(adcChannel, Context.AdcSampleSize);

      // get the desired value using the calibrated angle/value settings
      var array = new CalibrationArray(Program.context.Settings.Aci, Program.context.Settings.Acv);
      array.GetCalibrationPoint(angleDesired, out short valueDesired);

      PwmChannel pwmPin;
      if (value > valueDesired)
      {
        pwmPin = pwmPin1;
      }
      else
      {
        pwmPin = pwmPin2;
      }

      int originalDiff = Math.Abs(valueDesired - value);
      int lastCheckedDiff = originalDiff;
      float currentDutyCycle = 0f;

      try
      {
        // initialize the PWM output, but note nothing will happen until the dutycycle is set different from 0
        pwmPin1.Start();
        pwmPin2.Start();

        var polarityCheckTime = DateTime.UtcNow + TimeSpan.FromSeconds(3);
        var standstillCheckTime = DateTime.UtcNow + TimeSpan.FromSeconds(3);
        while (true)
        {
          // read the current adc value
          value = (short)AdcReader.GetValue(adcChannel, Context.AdcSampleSize);

          Debug.WriteLine($"ValueDesired: {valueDesired} value: {value}");

          // know when to stop...
          if (Math.Abs(valueDesired - value) < 10)
          {
            Program.context.BluetoothSpp.SendString($"Endpoint reached: valueDesired {valueDesired} value {value}\n");
            break;
          }

          int currentDiff = Math.Abs(valueDesired - value);

          // after some seconds, check if we are farther away from where we are supposed to be
          if (DateTime.UtcNow > polarityCheckTime)
          {
            if (currentDiff > originalDiff)
            {
              pwmPin.DutyCycle = 0f;

              Program.context.BluetoothSpp.SendString($"Motor is moving in wrong direction, reverse the polarity\n");

              Debug.WriteLine($"Original diff: {originalDiff}, current diff {currentDiff}");

              break;
            }

            polarityCheckTime = DateTime.MaxValue;
          }

          // check if the mirror is still moving, if not, stop the motor
          if (DateTime.UtcNow > standstillCheckTime)
          {
            if (Math.Abs(currentDiff - lastCheckedDiff) < 10)
            {
              pwmPin.DutyCycle = 0f;

              Program.context.BluetoothSpp.SendString($"Mirror is not moving, check if the motor is stuck\n");

              Debug.WriteLine($"Current diff: {originalDiff}, last checked diff {lastCheckedDiff}");

              break;
            }
            else
            {
              // keep on checking
              standstillCheckTime = DateTime.UtcNow + TimeSpan.FromSeconds(3);
              lastCheckedDiff = currentDiff;
            }
          }

          // check if we overshot our desired value, and if yes reverse direction
          if (value > valueDesired && pwmPin != pwmPin1)
          {
            pwmPin.DutyCycle = 0f;
            pwmPin = pwmPin1;
            Debug.WriteLine($"Overshoot detected, reversing polarity");
          }

          if (value < valueDesired && pwmPin != pwmPin2)
          {
            pwmPin.DutyCycle = 0f;
            pwmPin = pwmPin2;
            Debug.WriteLine($"Overshoot detected, reversing polarity");
          }

          // adapt the speed of motor depending how far we are from the desired position
          float dutyCycle;
          if (Math.Abs(valueDesired - value) > 100)
          {
            dutyCycle = 1f;
          }
          else
          {
            dutyCycle = Math.Abs(valueDesired - value) / 100f;

            // set a minimum speed at which the motor still moves
            dutyCycle = Math.Max(dutyCycle, 0.1f);
          }

          if (currentDutyCycle != dutyCycle)
          {
            Debug.WriteLine($"DutyCycle: {dutyCycle}");
            currentDutyCycle = dutyCycle;
          }

          pwmPin.DutyCycle = dutyCycle;
        }
      }
      catch (Exception ex)
      {
        Program.context.BluetoothSpp.SendString($"MoveMirror stopped with exception {ex.Message}\n");
      }
      finally
      {
        // full stop
        pwmPin.DutyCycle = 0f;
        pwmPin1.Stop();
        pwmPin2.Stop();
      }
    }
  }
}
