namespace NFHelio.Storage
{
  using NFCommon.Storage;

  internal class JsonSettingsStorageFactory : SettingsStorageFactory
  {
    public override ISettingsStorage GetSettingsStorage()
    {
      return new JsonSettingsStorage();
    }
  }
}
