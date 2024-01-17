using Framework.SimpleJSON;

namespace Framework
{
    public interface IDataUnit<T>
    {
        public int Index { get; set; }
    }
    public interface IEntity<T>
    {
        public JSONNode ToJson(IEntity<T> obj);
        public IEntity<T> FromJson(JSONNode data);
    }
}

