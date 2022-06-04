namespace NFHelio.Storage
{
  using NFCommon.Storage;

  internal class SimpleSettingsStorageFactory : SettingsStorageFactory
  {
    public override ISettingsStorage GetSettingsStorage()
    {
      return new SimpleSettingsStorage();
    }
  }
}
