namespace NFHelio.Tasks
{
  using System.Device.Pwm;
  using System.Threading;

  /// <summary>
  /// Tests the engines
  /// </summary>
  internal class TestMotors : ITask
  {
    /// <inheritdoc />
    string ITask.Command => "testmotor";

    /// <inheritdoc />
    string ITask.Description => "Tests the motor";

    /// <inheritdoc />
    string ITask.Help => "Briefly activates the motors";

    /// <inheritdoc />
    public void Execute(string[] args)
    {
      PwmChannel pwmPin1;
      PwmChannel pwmPin2;

      switch (args[0].ToLower())
      {
        case "a":
          pwmPin1 = PwmChannel.CreateFromPin((int)GPIOPort.PWM_Azimuth_East_to_West, 40000, 0);
          pwmPin2 = PwmChannel.CreateFromPin((int)GPIOPort.PWM_Azimuth_West_to_East, 40000, 0);
          break;
        case "z":
          pwmPin1 = PwmChannel.CreateFromPin((int)GPIOPort.PWM_Zenith_Up, 40000, 0);
          pwmPin2 = PwmChannel.CreateFromPin((int)GPIOPort.PWM_Zenith_Down, 40000, 0);
          break;
        default:
          Program.context.BluetoothSpp.SendString("Unknown plane\n");
          return;
      }

      // Start the PWM
      pwmPin1.Start();
      pwmPin2.Start();

      TestChannel(pwmPin1);
      TestChannel(pwmPin2);

      // Stop the PWM:
      pwmPin1.Stop();
      pwmPin2.Stop();

      Program.context.BluetoothSpp.SendString($"Done testing\n");
    }

    private void TestChannel(PwmChannel channel)
    {
      bool dutyCycleGoingUp = true;
      var dutyCycle = .00f;

      while (true)
      {
        if (dutyCycleGoingUp)
        {
          // slowly increase light intensity
          dutyCycle += 0.05f;

          // change direction if reaching maximum duty cycle (100%)
          if (dutyCycle > .95)
          {
            dutyCycleGoingUp = !dutyCycleGoingUp;
          }
        }
        else
        {
          // slowly decrease light intensity
          dutyCycle -= 0.05f;

          // change direction if reaching minimum duty cycle (0%)
          if (dutyCycle < 0.10)
          {
            break;
          }
        }

        // update duty cycle
        channel.DutyCycle = dutyCycle;

        Thread.Sleep(50);
      }

      channel.DutyCycle = .00f;
    }
  }
}
