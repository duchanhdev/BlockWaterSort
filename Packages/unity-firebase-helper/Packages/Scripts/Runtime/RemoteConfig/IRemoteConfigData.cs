namespace FirebaseHelper.Runtime.RemoteConfig
{
    public interface IRemoteConfigData
    {
        string ValueName { get; }

        string Serialize();

        void Deserialized(string json);
    }
}