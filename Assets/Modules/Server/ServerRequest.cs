using Framework.SimpleJSON;

namespace Server
{
    public enum ServerRequest
    {
        Ping,
        Pong
    }
    public static class ServerRequestExtension
    {
        public static JSONNode ToJson(this ServerRequest request)
        {
            return new JSONData((int)request);
        }
    }
}
