using Framework.SimpleJSON;

namespace Server
{
    public static class JsonExtension
    {
        public static void RequestServer(this JSONNode json)
        {
            WSClient.Instance.Send(json);
        }
    }
}
