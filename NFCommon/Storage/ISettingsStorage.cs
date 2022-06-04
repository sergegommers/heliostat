namespace NFCommon.Storage
{
  public interface ISettingsStorage
  {
    public SettingsBase ReadSettings();

    public void WriteSettings(SettingsBase settings);
  }
}
