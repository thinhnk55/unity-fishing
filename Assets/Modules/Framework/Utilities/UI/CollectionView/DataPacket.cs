using Framework.SimpleJSON;

namespace Framework
{
    public interface IDataUnit<T>
    {
        public int Id { get; set; }
        public T FromJson(JSONNode json);
    }
    public interface IEntity<T>
    {
        public JSONNode ToJson(IEntity<T> obj);
        public IEntity<T> FromJson(JSONNode data);
    }
}

