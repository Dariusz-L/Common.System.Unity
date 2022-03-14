namespace Common.Basic.DDD
{
    public abstract class Entity : IEntity
    {
        public string ID { get; }

        public uint Version { get; private set; }
        uint IEntity.Version { get => Version; set { Version = value; } }

        public Entity(string id, uint version)
        {
            ID = id;
            Version = version;
        }

        public Entity(string id)
        {
            ID = id;
        }
    }
}
