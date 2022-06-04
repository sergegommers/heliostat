namespace NFCommon.Storage
{
  public abstract class SettingsStorageFactory
  {
    /// <summary>
    /// Gets the settings storage.
    /// </summary>
    /// <returns></returns>
    public abstract ISettingsStorage GetSettingsStorage();
  }
}
